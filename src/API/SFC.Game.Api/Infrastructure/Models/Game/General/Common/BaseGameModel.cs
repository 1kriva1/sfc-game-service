namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// **Base** game model.
/// </summary>
public class BaseGameModel
{
    /// <summary>
    /// Game's profile model.
    /// </summary>
    public GameProfileModel? Profile { get; set; } = null!;
}