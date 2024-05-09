using diploma.Data;
using diploma.Exceptions;
using diploma.Features.Authentication.Services;
using diploma.Features.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace diploma.Features.Users.Commands;

public class SetUserRoleCommand : IRequest<Unit>
{
    public string Role { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid CallerId { get; set; }
}

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
{
    private readonly ApplicationDbContext _context;
    private readonly IPermissionService _permissionService;

    public SetUserRoleCommandHandler(ApplicationDbContext context, IPermissionService permissionService)
    {
        _context = context;
        _permissionService = permissionService;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _permissionService.UserHasPermissionAsync(request.CallerId, Constants.Permission.ManageContestParticipants))
        {
            throw new NotifyUserException("You don't have permission to perform this action");
        }

        var user = await _context.Users.FindAsync(request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var role = await _context.UserRoles.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == request.Role, cancellationToken);
        if (role is null)
        {
            throw new NotifyUserException("Role not found");
        }

        user.UserRoleId = role.Id;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}