using diploma.Data;
using diploma.Exceptions;
using diploma.Features.Authentication.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace diploma.Features.Authentication.Queries;

public class GetEmailConfirmationLinkByCodeQuery : IRequest<GetEmailConfirmationLinkByCodeQueryResult>
{
    public string Code { get; set; } = null!;
    public Guid UserId { get; set; }
}

public class GetEmailConfirmationLinkByCodeQueryResult
{
    public string Link { get; set; } = null!;
}

public class GetEmailConfirmationLinkByCodeQueryHandler : IRequestHandler<GetEmailConfirmationLinkByCodeQuery,
    GetEmailConfirmationLinkByCodeQueryResult>
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authenticationService;
    public GetEmailConfirmationLinkByCodeQueryHandler(ApplicationDbContext context, IAuthenticationService authenticationService)
    {
        _context = context;
        _authenticationService = authenticationService;
    }

    public async Task<GetEmailConfirmationLinkByCodeQueryResult> Handle(GetEmailConfirmationLinkByCodeQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.EmailConfirmationCode == request.Code && u.Id == request.UserId);
        if (user is null)
        {
            throw new NotifyUserException("Code is invalid");
        }

        if (user.EmailConfirmationCodeExpiresAt < DateTime.UtcNow)
        {
            throw new NotifyUserException("Code is expired. Please request it once more by signing up with the same email.");
        }

        return new GetEmailConfirmationLinkByCodeQueryResult
        {
            Link = _authenticationService.GetEmailConfirmationUrl(user.EmailConfirmationToken)
        };
    }
}