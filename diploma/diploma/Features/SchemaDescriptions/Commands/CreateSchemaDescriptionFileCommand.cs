﻿using System.Data.Common;
using System.Data.SqlClient;
using AutoMapper;
using diploma.Application;
using diploma.Application.Transpiler;
using diploma.Data;
using diploma.Features.Authentication.Exceptions;
using diploma.Features.Authentication.Services;
using diploma.Features.SchemaDescriptions.Exceptions;
using diploma.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace diploma.Features.SchemaDescriptions.Commands;

public class CreateSchemaDescriptionFileCommand : IRequest<SchemaDescriptionFileDto>
{
    public Guid CallerId { get; set; }
    public Guid SchemaDescriptionId { get; set; }
    public string Dbms { get; set; } = null!;
    public string? Description { get; set; }
    public string? SourceDbms { get; set; }
}

public class CreateSchemaDescriptionFileCommandHandler : IRequestHandler<CreateSchemaDescriptionFileCommand, SchemaDescriptionFileDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IDirectoryService _directoryService;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;
    private readonly IConfiguration _configuration;
    private readonly ISqlTranspilerService _sqlTranspilerService;
    
    public CreateSchemaDescriptionFileCommandHandler(ApplicationDbContext context, IDirectoryService directoryService, 
        IMapper mapper, IClaimService claimService, IConfiguration configuration, ISqlTranspilerService sqlTranspilerService)
    {
        _context = context;
        _directoryService = directoryService;
        _mapper = mapper;
        _claimService = claimService;
        _configuration = configuration;
        _sqlTranspilerService = sqlTranspilerService;
    }
    
    private async Task<string> TranspileAsync(SchemaDescription sd, string sourceDbms, string targetDbms, CancellationToken cancellationToken)
    {
        var schemaDescriptionFile = sd.Files.FirstOrDefault(f => f.Dbms == sourceDbms);
        if (schemaDescriptionFile is null) throw new SchemaDescriptionFileNotFoundException();
        if (schemaDescriptionFile.HasProblems) throw new Exception("Source schema description for transpilation has problems");
        
        var sql = await File.ReadAllTextAsync(schemaDescriptionFile.FilePath, cancellationToken);
        return await _sqlTranspilerService.TranspileAsync(sql, sourceDbms, targetDbms, cancellationToken);
    }

    public async Task<SchemaDescriptionFileDto> Handle(CreateSchemaDescriptionFileCommand request, CancellationToken cancellationToken)
    {
        if (!await _claimService.UserHasClaimAsync(request.CallerId, "ManageSchemaDescriptions", cancellationToken))
        {
            throw new UserDoesNotHaveClaimException(request.CallerId, "ManageSchemaDescriptions");
        }
        
        var schemaDescription = await _context.SchemaDescriptions.AsNoTracking()
            .Include(s => s.Files)
            .FirstOrDefaultAsync(s => s.Id == request.SchemaDescriptionId, cancellationToken);
        if (schemaDescription is null) throw new SchemaDescriptionNotFoundException();
        
        var description = request.Description ?? await TranspileAsync(schemaDescription, request.SourceDbms, request.Dbms, cancellationToken);

        bool hasProblems = false;
        string problems = null!;
        
        var dbmsAdapter = new DbmsAdapterFactory(_configuration).Create(request.Dbms);
        try
        {
            await dbmsAdapter.GetLockAsync(cancellationToken);
            await dbmsAdapter.DropCurrentSchemaAsync(cancellationToken);
            await dbmsAdapter.CreateSchemaAsync(description, cancellationToken);
        }
        catch (DbException e)
        {
            hasProblems = true;
            problems = e.InnerException?.Message ?? e.Message;
        }
        finally
        {
            dbmsAdapter.ReleaseLock();
        }
        
        await _directoryService.SaveSchemaDescriptionToFileAsync(schemaDescription.Id, request.Dbms, description, cancellationToken);

        var schemaDescriptionFile = new SchemaDescriptionFile
        {
            Id = Guid.NewGuid(),
            FilePath = _directoryService.GetSchemaDescriptionPath(schemaDescription.Id, request.Dbms),
            Dbms = request.Dbms,
            SchemaDescriptionId = schemaDescription.Id,
            HasProblems = hasProblems,
            Problems = problems,
        };
        
        await _context.SchemaDescriptionFiles.AddAsync(schemaDescriptionFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<SchemaDescriptionFileDto>(schemaDescriptionFile);
    }
}

