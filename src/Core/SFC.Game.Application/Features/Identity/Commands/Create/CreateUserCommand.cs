using SFC.Game.Application.Common.Dto.Identity;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Identity.Commands.Create;
public class CreateUserCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateUser; }

    public UserDto User { get; set; } = null!;
}