﻿using System.Security.Claims;
using diploma.Features.Users.Commands;
using diploma.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diploma.Features.Users;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpGet("my-claims")]
    [ResponseCache(Duration = 60 * 60 * 24)]
    public async Task<GetClaimsQueryResult> GetMyClaims([FromQuery] GetClaimsQuery query)
    {
        query.UserId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpGet]
    public async Task<UserDto> GetMyInfo([FromQuery] GetUserInfoQuery query)
    {
        query.Id = query.CallerId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<UserDto> GetUserInfo([FromRoute] GetUserInfoQuery query)
    {
        query.CallerId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(query);
        return result;
    }
    
    [HttpPut("my-info")]
    public async Task<IActionResult> UpdateMyInfo([FromBody] UpdateUserInfoCommand command)
    {
        command.CallerId = command.CallerId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _mediator.Send(command);
        return new OkResult();
    }
}