using Microsoft.AspNetCore.Authorization;

namespace SFC.Game.Infrastructure.Authorization.OwnGame;
public class OwnGameRequirement : IAuthorizationRequirement
{
    public OwnGameRequirement() { }

    public override string ToString()
    {
        return "OwnGameRequirement: Requires user has be owner of resource.";
    }
}