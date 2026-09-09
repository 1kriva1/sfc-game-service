using SFC.Game.Application.Common.Dto.Common;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Identity;

namespace SFC.Game.Application.Common.Dto.Identity;
public class UserDto : AuditableDto, IMapTo<User>
{
    public Guid Id { get; set; }
}