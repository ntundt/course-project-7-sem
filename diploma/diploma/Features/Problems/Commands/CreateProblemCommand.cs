﻿using AutoMapper;
using diploma.Data;
using diploma.Features.Authentication.Exceptions;
using diploma.Features.Authentication.Services;
using diploma.Services;
using MediatR;

namespace diploma.Features.Problems.Commands;

public class CreateProblemCommand : IRequest<ProblemDto>
{
    public Guid CallerId { get; set; }
    public string Name { get; set; } = null!;
    public string Statement { get; set; } = null!;
    public bool OrderMatters { get; set; }
    public decimal FloatMaxDelta { get; set; }
    public bool CaseSensitive { get; set; }
    public TimeSpan TimeLimit { get; set; }
    public Guid ContestId { get; set; }
    public Guid SchemaDescriptionId { get; set; }
    public string Solution { get; set; } = null!;
    public string SolutionDbms { get; set; } = null!;
}

public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, ProblemDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IDirectoryService _directoryService;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;
    
    public CreateProblemCommandHandler(ApplicationDbContext context, IDirectoryService directoryService, IMapper mapper, IClaimService claimService)
    {
        _context = context;
        _directoryService = directoryService;
        _mapper = mapper;
        _claimService = claimService;
    }
    
    public async Task<ProblemDto> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
    {
        if (!await _claimService.UserHasClaimAsync(request.CallerId, "ManageProblems", cancellationToken))
        {
            throw new UserDoesNotHaveClaimException(request.CallerId, "ManageProblems");
        }
        
        var problem = new Problem
        {
            Name = request.Name,
            OrderMatters = request.OrderMatters,
            FloatMaxDelta = request.FloatMaxDelta,
            CaseSensitive = request.CaseSensitive,
            TimeLimit = request.TimeLimit,
            ContestId = request.ContestId,
            SchemaDescriptionId = request.SchemaDescriptionId,
            SolutionDbms = request.SolutionDbms,
        };
        problem.Id = Guid.NewGuid();
        problem.StatementPath = _directoryService.GetProblemStatementPath(problem.Id);
        problem.SolutionPath = _directoryService.GetProblemSolutionPath(problem.Id, request.SolutionDbms);
        await _directoryService.SaveProblemStatementToFileAsync(problem.Id, request.Statement, cancellationToken);
        await _directoryService.SaveProblemSolutionToFileAsync(problem.Id, request.SolutionDbms, request.Solution, cancellationToken);
        await _context.Problems.AddAsync(problem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ProblemDto>(problem);
    }
}