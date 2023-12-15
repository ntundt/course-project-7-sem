﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using diploma.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace diploma.Features.SchemaDescriptions.Queries;

public class GetSchemaDescriptionsQuery : IRequest <GetSchemaDescriptionsQueryResult>
{
    public SieveModel? SieveModel { get; set; }
}

public class GetSchemaDescriptionsQueryResult
{
    public List<SchemaDescriptionDto> SchemaDescriptions { get; set; } = null!;
}

public class GetSchemaDescriptionsQueryHandler : IRequestHandler<GetSchemaDescriptionsQuery, GetSchemaDescriptionsQueryResult>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly SieveProcessor _sieveProcessor;
    
    public GetSchemaDescriptionsQueryHandler(ApplicationDbContext context, IMapper mapper, SieveProcessor sieveProcessor)
    {
        _context = context;
        _mapper = mapper;
        _sieveProcessor = sieveProcessor;
    }
    
    public async Task<GetSchemaDescriptionsQueryResult> Handle(GetSchemaDescriptionsQuery request, CancellationToken cancellationToken)
    {
        var schemaDescriptions = _context.SchemaDescriptions.AsNoTracking()
            .Include(s => s.Files)
            .ProjectTo<SchemaDescriptionDto>(_mapper.ConfigurationProvider);
        
        schemaDescriptions = _sieveProcessor.Apply(request.SieveModel, schemaDescriptions, applyPagination: false);

        return new GetSchemaDescriptionsQueryResult
        {
            SchemaDescriptions = _mapper.Map<List<SchemaDescriptionDto>>(await schemaDescriptions.ToListAsync(cancellationToken))
        };
    }
}
