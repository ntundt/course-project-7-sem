﻿using diploma.Data;
using diploma.Features.Authentication.Exceptions;
using diploma.Features.Authentication.Services;
using diploma.Features.SchemaDescriptions.Exceptions;
using diploma.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace diploma.Features.SchemaDescriptions.Commands;

public class DeleteSchemaDescriptionFileCommand : IRequest<Unit>
{
    public Guid CallerId { get; set; }
    public Guid SchemaDescriptionId { get; set; }
    public string Dbms { get; set; } = null!;
}

public class DeleteSchemaDescriptionFileCommandHandler : IRequestHandler<DeleteSchemaDescriptionFileCommand, Unit>
{
    private readonly ApplicationDbContext _context;
    private readonly IDirectoryService _directoryService;
    private readonly IClaimService _claimService;
    
    public DeleteSchemaDescriptionFileCommandHandler(ApplicationDbContext context, IDirectoryService directoryService, IClaimService claimService)
    {
        _context = context;
        _directoryService = directoryService;
        _claimService = claimService;
    }
    
    public async Task<Unit> Handle(DeleteSchemaDescriptionFileCommand request, CancellationToken cancellationToken)
    {
        if (!await _claimService.UserHasClaimAsync(request.CallerId, "ManageSchemaDescriptions", cancellationToken))
        {
            throw new UserDoesNotHaveClaimException(request.CallerId, "ManageSchemaDescriptions");
        }
        
        var schemaDescriptionFile = await _context.SchemaDescriptionFiles.FirstOrDefaultAsync(s => s.SchemaDescriptionId == request.SchemaDescriptionId && s.Dbms == request.Dbms, cancellationToken);
        if (schemaDescriptionFile == null)
        {
            throw new SchemaDescriptionFileNotFoundException();
        }

        _context.SchemaDescriptionFiles.Remove(schemaDescriptionFile);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}