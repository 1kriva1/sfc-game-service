using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Create;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Get;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Gets;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Update;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Updates;
using SFC.Game.Api.Infrastructure.Models.Pagination;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Application.Features.Game.Team.General.Commands.Update;
using SFC.Game.Application.Features.Game.Team.General.Commands.Updates;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.Team.General.Queries.Get;
using SFC.Game.Application.Features.Game.Team.General.Queries.Gets;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Controllers;

/// <summary>
/// Game teams controller
/// </summary>
[Tags("Game Teams")]
[Route("api/Games/{id}")]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
public class GameTeamController : ApiControllerBase
{
    /// <summary>
    /// Create new game team.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="request">Create game team request.</param>
    /// <returns>An ActionResult of type CreateGamTeameResponse</returns>
    /// <response code="201">Returns **new** created game team.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpPost("Teams")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateGameTeamResponse>> CreateGameTeamAsync([FromRoute] long id, [FromBody] CreateGameTeamRequest request)
    {
        CreateGameTeamCommand command = Mapper.Map<CreateGameTeamCommand>(request)
                                              .SetGameId(id);

        CreateGameTeamViewModel model = await Mediator.Send(command).ConfigureAwait(false);

        return CreatedAtRoute("GetGameTeam", new { id = model.GameTeam.GameId, teamId = model.GameTeam.TeamId }, Mapper.Map<CreateGameTeamResponse>(model));
    }

    /// <summary>
    /// Update game team.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="request">Update game team request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if game team **successfully** updated.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    /// <response code="404">Returns when invite **not found**.</response>
    /// <response code="409">Returns when **flow validation** errors.</response>
    [HttpPut("Teams/{teamId}")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdateGameTeamAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromBody] UpdateGameTeamRequest request)
    {
        UpdateGameTeamCommand command = Mapper.Map<UpdateGameTeamCommand>(request)
                                              .SetGameId(id)
                                              .SetTeamId(teamId);

        await Mediator.Send(command)
                      .ConfigureAwait(false);

        return NoContent();
    }

    /// <summary>
    /// Updates game team.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="request">Update game teams request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if game teams **successfully** updated.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    /// <response code="404">Returns when invite **not found**.</response>
    /// <response code="409">Returns when **flow validation** errors.</response>
    [HttpPut("Teams")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdatesGameTeamAsync(
        [FromRoute] long id, [FromBody] UpdatesGameTeamRequest request)
    {
        UpdatesGameTeamCommand command = Mapper.Map<UpdatesGameTeamCommand>(request)
                                               .SetGameId(id);

        await Mediator.Send(command)
                      .ConfigureAwait(false);

        return NoContent();
    }

    /// <summary>
    /// Return game team model by unique identifier.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Game team unique identifier.</param>
    /// <returns>An ActionResult of type GetGameTeamResponse</returns>
    /// <response code="200">Returns game team model.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="404">Returns when game team **not found** by unique identifier.</response>
    [HttpGet("Teams/{teamId}", Name = "GetGameTeam")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetGameTeamResponse>> GetGameTeamAsync([FromRoute] long id, [FromRoute] long teamId)
    {
        GetGameTeamQuery query = new GetGameTeamQuery() { GameId = id, TeamId = teamId }
            .SetIncludes(Request.GetIncludes());

        GetGameTeamViewModel gameTeam = await Mediator.Send(query).ConfigureAwait(false);

        return Ok(Mapper.Map<GetGameTeamResponse>(gameTeam));
    }

    /// <summary>
    /// Return game team models by game Id.
    /// </summary>
    /// <param name="id">Game unique identifier.</param>
    /// <returns>An ActionResult of type GetsGameTeamResponse</returns>
    /// <response code="200">Returns game team models.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpGet("Teams")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetsGameTeamResponse>> GetsGameTeamAsync([FromRoute] long id)
    {
        GetsGameTeamQuery query = new GetsGameTeamQuery() { GameId = id }
            .SetIncludes(Request.GetIncludes());

        GetsGameTeamViewModel gameTeams = await Mediator.Send(query)
                                                        .ConfigureAwait(false);

        return Ok(Mapper.Map<GetsGameTeamResponse>(gameTeams));
    }

    /// <summary>
    /// Return list of game teams.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="request">Get game teams request.</param>
    /// <returns>An ActionResult of type GetGameTeamsResponse</returns>
    /// <response code="200">Returns list of game teams with pagination header.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpGet("Teams/Find")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetGameTeamsResponse>> GetGameTeamsAsync([FromRoute] long id, [FromQuery] GetGameTeamsRequest request)
    {
        BasePaginationRequest<GetGameTeamsViewModel, GetGameTeamsFilterDto> query = Mapper.Map<GetGameTeamsQuery>(request)
                                                                                          .SetGameId(id)
                                                                                          .SetIncludes(Request.GetIncludes());

        GetGameTeamsViewModel result = await Mediator.Send(query)
                                                     .ConfigureAwait(false);

        PageMetadataModel metadata = Mapper.Map<PageMetadataModel>(result.Metadata)
                                           .SetLinks(UriService, Request.QueryString.Value!, Request.Path.Value!);

        Response.AddPaginationHeader(metadata);

        return Ok(Mapper.Map<GetGameTeamsResponse>(result));
    }
}