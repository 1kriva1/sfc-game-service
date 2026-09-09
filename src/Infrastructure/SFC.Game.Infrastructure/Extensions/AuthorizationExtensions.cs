using Microsoft.AspNetCore.Authorization;

using SFC.Game.Infrastructure.Authorization;

namespace SFC.Game.Infrastructure.Extensions;
public static class AuthorizationExtensions
{
    public static void AddGeneralPolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        ArgumentNullException.ThrowIfNull(options);

        PolicyModel general = AuthorizationPolicies.General(claims);
        options.AddPolicy(general.Name, general.Policy);
    }

    public static void AddOwnGamePolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        PolicyModel ownGame = AuthorizationPolicies.OwnGame(claims);
        options.AddPolicy(ownGame.Name, ownGame.Policy);
    }

    public static void AddOwnPlayerPolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        PolicyModel ownPlayer = AuthorizationPolicies.OwnPlayer(claims);
        options.AddPolicy(ownPlayer.Name, ownPlayer.Policy);
    }

    public static void AddOwnTeamPolicy(this AuthorizationOptions options, IDictionary<string, IEnumerable<string>> claims)
    {
        PolicyModel ownTeam = AuthorizationPolicies.OwnTeam(claims);
        options.AddPolicy(ownTeam.Name, ownTeam.Policy);
    }
}