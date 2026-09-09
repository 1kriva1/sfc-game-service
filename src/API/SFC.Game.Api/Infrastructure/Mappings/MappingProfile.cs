using System.Reflection;

using Google.Protobuf.WellKnownTypes;

using SFC.Game.Api.Infrastructure.Models.Common;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Base;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;
using SFC.Game.Application.Features.Game.Data.Queries.GetAll;
using SFC.Game.Application.Features.Game.General.Common.Dto;
using SFC.Game.Application.Features.Game.General.Queries.Find;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.General.Queries.Get;
using SFC.Game.Application.Features.Game.Player.Common.Dto;
using SFC.Game.Application.Features.Game.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.Player.Queries.Get;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.Team.General.Queries.Get;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Get;

namespace SFC.Game.Api.Infrastructure.Mappings;

public class MappingProfile : BaseMappingProfile
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    public MappingProfile() : base()
    {
        ApplyCustomMappings();
    }

    private void ApplyCustomMappings()
    {
        #region Simple types

        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(value => DateTime.SpecifyKind(value, DateTimeKind.Utc).ToTimestamp());

        CreateMap<TimeSpan, Duration>()
            .ConvertUsing(value => Duration.FromTimeSpan(value));

        CreateMap<Duration, TimeSpan>()
            .ConvertUsing(value => value.ToTimeSpan());

        CreateMap<DateOnly, Timestamp>()
            .ConvertUsing(value => DateTime.SpecifyKind(value.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToTimestamp());

        CreateMap<Timestamp, DateOnly>()
            .ConvertUsing(value => DateOnly.FromDateTime(value.ToDateTime()));

        #endregion Simple types

        #region Generic types

        CreateMap(typeof(RangeLimitModel<>), typeof(RangeLimitDto<>));

        #endregion Generic types

        #region Complex types

        // data
        CreateMapGameDataContracts();

        // contracts
        CreateMapGameContracts();

        #endregion Complex types        
    }
    private void CreateMapGameDataContracts()
    {
        CreateMap<DataValueDto, SFC.Game.Contracts.Models.Game.Data.DataValue>();
        CreateMap<GetAllGameDataViewModel, SFC.Game.Contracts.Messages.Game.Data.GetAll.GetAllGameDataResponse>();
    }

    private void CreateMapGameContracts()
    {
        // game
        CreateMap<GameDto, SFC.Game.Contracts.Models.Game.General.Game>();
        CreateMap<GameProfileDto, SFC.Game.Contracts.Models.Game.General.GameProfile>();
        CreateMap<GameGeneralProfileDto, SFC.Game.Contracts.Models.Game.General.GameGeneralProfile>();
        CreateMap<GameAvailabilityDto, SFC.Game.Contracts.Models.Game.General.GameAvailability>();
        CreateMap<GameFinancialProfileDto, SFC.Game.Contracts.Models.Game.General.GameFinancialProfile>();
        CreateMap<GameInventaryProfileDto, SFC.Game.Contracts.Models.Game.General.GameInventaryProfile>();

        // get game
        CreateMap<GetGameViewModel, SFC.Game.Contracts.Messages.Game.General.Get.GetGameResponse>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Get.GetGameRequest, GetGameQuery>();
        CreateMap<GameDto, SFC.Game.Contracts.Headers.AuditableHeader>()
            .IgnoreAllNonExisting();

        // get games
        // (filters)
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.GetGamesRequest, GetGamesQuery>();
        CreateMap<SFC.Game.Contracts.Models.Common.Pagination, PaginationDto>();
        CreateMap<SFC.Game.Contracts.Models.Common.Sorting, SortingDto>();
        CreateMap(typeof(SFC.Game.Contracts.Models.Common.RangeLimit), typeof(RangeLimitDto<>));
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesFilter, GetGamesFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesProfileFilter, GetGamesProfileFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesGeneralProfileFilter, GetGamesGeneralProfileFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesAvailabilityLimit, GetGamesAvailabilityLimitDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesFinancialProfileFilter, GetGamesFinancialProfileFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.General.Find.Filters.GamesInventaryProfileFilter, GetGamesInventaryProfileFilterDto>();
        // (result)
        CreateMap<GetGamesViewModel, SFC.Game.Contracts.Messages.Game.General.Find.GetGamesResponse>();
        // (headers)
        CreateMap<PageMetadataDto, SFC.Game.Contracts.Headers.PaginationHeader>()
            .IgnoreAllNonExisting();

        // game team
        CreateMap<GameTeamDto, SFC.Game.Contracts.Models.Game.Team.General.GameTeam>();

        // get game team
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.General.Get.GetGameTeamRequest, GetGameTeamQuery>();
        CreateMap<GetGameTeamViewModel, SFC.Game.Contracts.Messages.Game.Team.General.Get.GetGameTeamResponse>();
        CreateMap<GameTeamDto, SFC.Game.Contracts.Headers.AuditableHeader>();

        // get game teams
        // (filters)
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.General.Find.GetGameTeamsRequest, GetGameTeamsQuery>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.General.Find.Filters.GameTeamsFilter, GetGameTeamsFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.General.Find.Filters.GameTeamFilter, GetGameTeamsGameTeamFilterDto>();
        // (result)
        CreateMap<GetGameTeamsViewModel, SFC.Game.Contracts.Messages.Game.Team.General.Find.GetGameTeamsResponse>();

        // game player
        CreateMap<GamePlayerDto, SFC.Game.Contracts.Models.Game.Player.GamePlayer>();

        // get game player
        CreateMap<SFC.Game.Contracts.Messages.Game.Player.Get.GetGamePlayerRequest, GetGamePlayerQuery>();
        CreateMap<GetGamePlayerViewModel, SFC.Game.Contracts.Messages.Game.Player.Get.GetGamePlayerResponse>();
        CreateMap<GamePlayerDto, SFC.Game.Contracts.Headers.AuditableHeader>();

        // get game players
        // (filters)
        CreateMap<SFC.Game.Contracts.Messages.Game.Player.Find.GetGamePlayersRequest, GetGamePlayersQuery>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Player.Find.Filters.GamePlayersFilter, GetGamePlayersFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Player.Find.Filters.GamePlayerFilter, GetGamePlayersGamePlayerFilterDto>();
        // (result)
        CreateMap<GetGamePlayersViewModel, SFC.Game.Contracts.Messages.Game.Player.Find.GetGamePlayersResponse>();

        // game team player
        CreateMap<GameTeamPlayerDto, SFC.Game.Contracts.Models.Game.Team.Player.GameTeamPlayer>();

        // get game team player
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.Player.Get.GetGameTeamPlayerRequest, GetGameTeamPlayerQuery>();
        CreateMap<GetGameTeamPlayerViewModel, SFC.Game.Contracts.Messages.Game.Team.Player.Get.GetGameTeamPlayerResponse>();
        CreateMap<GameTeamPlayerDto, SFC.Game.Contracts.Headers.AuditableHeader>();

        // get game team players
        // (filters)
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.Player.Find.GetGameTeamPlayersRequest, GetGameTeamPlayersQuery>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.Player.Find.Filters.GameTeamPlayersFilter, GetGameTeamPlayersFilterDto>();
        CreateMap<SFC.Game.Contracts.Messages.Game.Team.Player.Find.Filters.GameTeamPlayerFilter, GetGameTeamPlayersGameTeamPlayerFilterDto>();
        // (result)
        CreateMap<GetGameTeamPlayersViewModel, SFC.Game.Contracts.Messages.Game.Team.Player.Find.GetGameTeamPlayersResponse>();
    }
}