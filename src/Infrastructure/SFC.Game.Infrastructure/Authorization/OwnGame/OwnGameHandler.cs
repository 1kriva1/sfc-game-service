using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Infrastructure.Extensions;

namespace SFC.Game.Infrastructure.Authorization.OwnGame;
public class OwnGameHandler(IGameRepository gameRepository, IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<OwnGameRequirement>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnGameRequirement requirement)
    {
        string? gameIdValue = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (!long.TryParse(gameIdValue, out long gameId))
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

        if (!await _gameRepository.AnyAsync(gameId, userId.Value).ConfigureAwait(true))
        {
            context.Fail(new AuthorizationFailureReason(this, $"User - {userId} does not related to this resource - {gameId}."));
            return;
        }

        context.Succeed(requirement);
    }
}