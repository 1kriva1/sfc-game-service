using Microsoft.AspNetCore.Http;

using SFC.Game.Application.Interfaces.Identity;
using SFC.Game.Infrastructure.Extensions;

namespace SFC.Game.Infrastructure.Services.Identity;
public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserId() => _httpContextAccessor.GetUserId();
}