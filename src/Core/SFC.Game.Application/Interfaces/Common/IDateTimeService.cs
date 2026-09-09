namespace SFC.Game.Application.Interfaces.Common;

/// <summary>
/// Data and time service.
/// </summary>
public interface IDateTimeService
{
    DateTime Now { get; }

    DateTime DateNow { get; }
}