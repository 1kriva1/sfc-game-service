namespace SFC.Game.Application.Features.Game.General.Common.Extensions;

public static class GameExtensions
{
    public static GameEntity SetStatus(this GameEntity value, GameStatusEnum status)
    {
        value.StatusId = status;
        return value;
    }
}