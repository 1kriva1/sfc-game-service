using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Player.Find;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Find;
using SFC.Game.Api.Infrastructure.Models.Pagination;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Game.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Controllers;

/// <summary>
/// Game players controller
/// </summary>
[Tags("Game Players")]
[Route("api/Games/{id}")]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
public class GamePlayerController : ApiControllerBase
{
    /// <summary>
    /// Return list of game players.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="request">Get game players request.</param>
    /// <returns>An ActionResult of type GetGamePlayersResponse</returns>
    /// <response code="200">Returns list of game players with pagination header.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpGet("Players/Find")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetGamePlayersResponse>> GetGamePlayersAsync([FromRoute] long id, [FromQuery] GetGamePlayersRequest request)
    {
        BasePaginationRequest<GetGamePlayersViewModel, GetGamePlayersFilterDto> query = Mapper.Map<GetGamePlayersQuery>(request)
                                                                                              .SetGameId(id)
                                                                                              .SetIncludes(Request.GetIncludes());

        GetGamePlayersViewModel result = await Mediator.Send(query)
                                                       .ConfigureAwait(false);

        PageMetadataModel metadata = Mapper.Map<PageMetadataModel>(result.Metadata)
                                           .SetLinks(UriService, Request.QueryString.Value!, Request.Path.Value!);

        Response.AddPaginationHeader(metadata);

        return Ok(Mapper.Map<GetGamePlayersResponse>(result));
    }
}