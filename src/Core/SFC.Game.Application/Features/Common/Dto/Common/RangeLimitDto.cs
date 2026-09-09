namespace SFC.Game.Application.Features.Common.Dto.Common;

/// <summary>
/// Generic class for all from-to filter/entity classes.
/// </summary>
/// <typeparam name="T">Limit type (ex. number, DateTime...).</typeparam>
public class RangeLimitDto<T>
{
    public T? From { get; set; }

    public T? To { get; set; }
}