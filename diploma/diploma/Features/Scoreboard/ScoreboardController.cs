﻿using System.Security.Claims;
using diploma.Features.Scoreboard.Commands;
using diploma.Features.Scoreboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diploma.Features.Scoreboard;

[ApiController]
[Route("api/scoreboard")]
public class ScoreboardController
{
    private readonly IMediator _mediator;
    private readonly Authentication.Services.IAuthorizationService _authorizationService;
    
    public ScoreboardController(IMediator mediator, Authentication.Services.IAuthorizationService authorizationService)
    {
        _mediator = mediator;
        _authorizationService = authorizationService;
    }
    
    [HttpGet]
    public async Task<GetScoreboardQueryResult> GetScoreboard([FromQuery] GetScoreboardQuery query)
    {
        try 
        {
            query.CallerId = _authorizationService.GetUserId();
        }
        catch (Exception)
        {
            query.CallerId = Guid.Empty;
        }
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("approve")]
    [Authorize]
    public async Task ApproveScoreboard([FromQuery] Guid contestId)
    {
        var command = new ApproveScoreboardCommand
        {
            ContestId = contestId,
            CallerId = _authorizationService.GetUserId(),
        };
        await _mediator.Send(command);
    }

    [HttpGet("approval-status")]
    public async Task<GetScoreboardApprovalStatusQueryResult> GetScoreboardApprovalStatus([FromQuery] Guid contestId)
    {
        var query = new GetScoreboardApprovalStatusQuery
        {
            ContestId = contestId,
        };
        var result = await _mediator.Send(query);
        return result;
    }
}