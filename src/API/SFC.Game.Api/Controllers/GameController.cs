using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.General.Create;
using SFC.Game.Api.Infrastructure.Models.Game.General.Find;
using SFC.Game.Api.Infrastructure.Models.Game.General.Get;
using SFC.Game.Api.Infrastructure.Models.Game.General.Update;
using SFC.Game.Api.Infrastructure.Models.Pagination;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Game.General.Commands.Create;
using SFC.Game.Application.Features.Game.General.Commands.Update;
using SFC.Game.Application.Features.Game.General.Queries.Find;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.General.Queries.Get;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Controllers;

[Tags("Games")]
[Route("api/Games")]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
public class GameController : ApiControllerBase
{
    /// <summary>
    /// Create new game.
    /// </summary>
    /// <param name="request">Create game request.</param>
    /// <returns>An ActionResult of type CreateGameResponse</returns>
    /// <response code="201">Returns **new** created game.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpPost]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateGameResponse>> CreateGameAsync([FromBody] CreateGameRequest request)
    {
        CreateGameCommand command = Mapper.Map<CreateGameCommand>(request);

        CreateGameViewModel model = await Mediator.Send(command).ConfigureAwait(false);

        return CreatedAtRoute("GetGame", new { id = model.Game.Id }, Mapper.Map<CreateGameResponse>(model));
    }

    /// <summary>
    /// Update existing game.
    /// </summary>
    /// <param name="id">Game unique identifier.</param>
    /// <param name="request">Update game request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if game updated **successfully**.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpPut("{id}")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateGameAsync([FromRoute] long id, [FromBody] UpdateGameRequest request)
    {
        UpdateGameCommand command = Mapper.Map<UpdateGameCommand>(request)
                                          .SetGameId(id);

        await Mediator.Send(command).ConfigureAwait(false);

        return NoContent();
    }

    /// <summary>
    /// Return game model by unique identifier.
    /// </summary>
    /// <param name="id">Game unique identifier.</param>
    /// <returns>An ActionResult of type GetGameResponse</returns>
    /// <response code="200">Returns game model.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="404">Returns when game **not found** by unique identifier.</response>
    [HttpGet("{id}", Name = "GetGame")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetGameResponse>> GetGameAsync([FromRoute] long id)
    {
        GetGameQuery query = new GetGameQuery() { Id = id }
            .SetIncludes(Request.GetIncludes());

        GetGameViewModel game = await Mediator.Send(query).ConfigureAwait(false);

        return Ok(Mapper.Map<GetGameResponse>(game));
    }

    /// <summary>
    /// Return list of games
    /// </summary>
    /// <param name="request">Get games request.</param>
    /// <returns>An ActionResult of type GetGamesResponse</returns>
    /// <response code="200">Returns list of games with pagination header.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpGet("find")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetGamesResponse>> GetGamesAsync([FromQuery] GetGamesRequest request)
    {
        BasePaginationRequest<GetGamesViewModel, GetGamesFilterDto> query = Mapper
            .Map<GetGamesQuery>(request)
            .SetIncludes(Request.GetIncludes());

        GetGamesViewModel result = await Mediator.Send(query).ConfigureAwait(false);

        PageMetadataModel metadata = Mapper.Map<PageMetadataModel>(result.Metadata)
                                           .SetLinks(UriService, Request.QueryString.Value!, Request.Path.Value!);

        Response.AddPaginationHeader(metadata);

        return Ok(Mapper.Map<GetGamesResponse>(result));
    }
}