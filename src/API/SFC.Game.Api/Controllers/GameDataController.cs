using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Data.GetAll;
using SFC.Game.Application.Features.Game.Data.Queries.GetAll;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Controllers;

[Tags("Game Data")]
[Route("api/Games/Data")]
[Authorize(Policy.General)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status403Forbidden)]
public class GameDataController : ApiControllerBase
{
    /// <summary>
    /// Return all available game data types.
    /// </summary>
    /// <returns>An ActionResult of type GetAllGameDataResponse</returns>
    /// <response code="200">Returns all available **data types**.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetAllGameDataResponse>> GetAllAsync()
    {
        GetAllGameDataQuery query = new();

        GetAllGameDataViewModel model = await Mediator.Send(query).ConfigureAwait(true);

        return Ok(Mapper.Map<GetAllGameDataResponse>(model));
    }
}