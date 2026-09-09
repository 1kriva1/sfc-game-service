namespace SFC.Game.Application.Interfaces.Common;

/// <summary>
/// URI service.
/// </summary>
public interface IUriService
{
    public Uri GetPageUri(string queryString, string route, int page);
}