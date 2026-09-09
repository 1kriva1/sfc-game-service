using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Infrastructure.Extensions;

namespace SFC.Game.Infrastructure.Authorization.OwnTeam;
public class OwnTeamHandler(ITeamRepository teamRepository, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<OwnTeamRequirement>
{
    private readonly ITeamRepository _teamRepository = teamRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnTeamRequirement requirement)
    {
        string? teamIdValue = _httpContextAccessor.HttpContext?.GetRouteValue("teamId")?.ToString();

        if (!long.TryParse(teamIdValue, out long teamId))
        {
            context.Fail(new AuthorizationFailureReason(this, "Route does not have \"id\" parameter value."));
            return;
        }

        Guid? userId = _httpContextAccessor.GetUserId();

        if (!userId.HasValue)
        {
            context.Fail(new AuthorizationFailureReason(this, "User does not have NameIdentifier claim value."));
            return;
        }

        if (!await _teamRepository.AnyAsync(teamId, userId.Value).ConfigureAwait(true))
        {
            context.Fail(new AuthorizationFailureReason(this, $"User - {userId} does not related to this resource - {teamId}."));
            return;
        }

        context.Succeed(requirement);
    }
}