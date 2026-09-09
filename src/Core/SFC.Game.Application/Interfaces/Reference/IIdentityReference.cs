using SFC.Game.Application.Common.Dto.Identity;
using SFC.Game.Domain.Entities.Identity;

namespace SFC.Game.Application.Interfaces.Reference;

/// <summary>
/// Identity user reference service.
/// </summary>
public interface IIdentityReference : IReference<User, Guid, UserDto> { }