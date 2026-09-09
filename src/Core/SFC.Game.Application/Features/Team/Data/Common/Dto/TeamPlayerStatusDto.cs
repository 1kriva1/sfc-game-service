using SFC.Game.Application.Common.Dto.Data;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Team.Data;

namespace SFC.Game.Application.Features.Team.Data.Common.Dto;
public class TeamPlayerStatusDto : DataDto, IMapTo<TeamPlayerStatus> { }