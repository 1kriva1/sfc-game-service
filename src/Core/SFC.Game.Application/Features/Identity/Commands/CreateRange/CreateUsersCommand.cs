using SFC.Game.Application.Common.Dto.Identity;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Identity.Commands.CreateRange;
public class CreateUsersCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateUsers; }

    public IEnumerable<UserDto> Users { get; set; } = null!;
}