using SFC.Game.Application.Common.Dto.Data;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Request.Data;

namespace SFC.Game.Application.Features.Request.Data.Common.Dto;
public class RequestStatusDto : DataDto, IMapTo<RequestStatus> { }