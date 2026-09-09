using System.Reflection;

using Google.Protobuf.WellKnownTypes;

using SFC.Game.Application.Common.Dto.Identity;
using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Dto.Team.Player;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Common.Mappings.Base;
using SFC.Game.Application.Features.Data.Commands.Reset;
using SFC.Game.Application.Features.Data.Common.Dto;
using SFC.Game.Application.Features.Game.Player.Commands.Create;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Application.Features.Identity.Commands.Create;
using SFC.Game.Application.Features.Identity.Commands.CreateRange;
using SFC.Game.Application.Features.Invite.Data.Commands.Reset;
using SFC.Game.Application.Features.Invite.Data.Common.Dto;
using SFC.Game.Application.Features.Player.Commands.Create;
using SFC.Game.Application.Features.Player.Commands.CreateRange;
using SFC.Game.Application.Features.Player.Commands.Update;
using SFC.Game.Application.Features.Request.Data.Commands.Reset;
using SFC.Game.Application.Features.Request.Data.Common.Dto;
using SFC.Game.Application.Features.Team.Data.Commands.Reset;
using SFC.Game.Application.Features.Team.Data.Common.Dto;
using SFC.Game.Application.Features.Team.General.Commands.Create;
using SFC.Game.Application.Features.Team.General.Commands.CreateRange;
using SFC.Game.Application.Features.Team.General.Commands.Update;
using SFC.Game.Application.Features.Team.Player.Commands.Create;
using SFC.Game.Application.Features.Team.Player.Commands.CreateRange;
using SFC.Game.Application.Features.Team.Player.Commands.Update;
using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.General;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Messages.Commands.Game.General;
using SFC.Game.Messages.Commands.Game.Team.General;
using SFC.Game.Messages.Commands.Game.Team.Player;
using SFC.Game.Messages.Events.Game.Team.General;
using SFC.Game.Messages.Events.Game.Team.Player;

namespace SFC.Game.Infrastructure.Mapping;

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

        CreateMap<Timestamp, DateTime>()
           .ConvertUsing(value => value.ToDateTime());

        CreateMap<Duration, TimeSpan>()
            .ConvertUsing(value => value.ToTimeSpan());

        #endregion Simple types

        #region Data

        // messages        
        CreateMapDataMessages();

        #endregion Data

        #region Identity

        // messages        
        CreateMapIdentityMessages();

        // contracts        
        CreateMapIdentityContracts();

        #endregion Identity

        #region Player

        // messages
        CreateMapPlayerMessages();

        // contracts
        CreateMapPlayerContracts();

        #endregion Player

        #region Team

        CreateMapTeamMessages();

        #endregion Team

        #region Invite

        // messages
        CreateMapInviteMessages();

        #endregion Invite

        #region Request

        // messages
        CreateMapRequestMessages();

        #endregion Request

        #region Scheme

        // messages
        CreateMapSchemeMessages();

        #endregion Scheme

        #region Game

        // messages
        CreateMapGameMessages();

        #endregion Game
    }

    #region Data

    private void CreateMapDataMessages()
    {
        CreateMap<SFC.Data.Messages.Events.Data.DataInitialized, ResetDataCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, FootballPositionDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, GameStyleDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, StatCategoryDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, StatSkillDto>();
        CreateMap<SFC.Data.Messages.Models.Data.StatTypeDataValue, StatTypeDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, WorkingFootDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, ShirtDto>();
    }

    #endregion Data

    #region Identity

    private void CreateMapIdentityMessages()
    {
        CreateMap<SFC.Identity.Messages.Events.User.UserCreated, CreateUserCommand>().IgnoreAllNonExisting();

        CreateMap<IEnumerable<SFC.Identity.Messages.Models.User.User>, CreateUsersCommand>()
            .ForMember(p => p.Users, d => d.MapFrom(z => z));

        CreateMap<SFC.Identity.Messages.Models.User.User, UserDto>();
    }

    private void CreateMapIdentityContracts()
    {
        CreateMap<Guid, SFC.Identity.Contracts.Messages.User.Get.GetUserRequest>()
            .ConvertUsing(id => new SFC.Identity.Contracts.Messages.User.Get.GetUserRequest { Id = id.ToString() });
        CreateMap<SFC.Identity.Contracts.Models.User.User, UserDto>();
    }

    #endregion Identity

    #region Player

    private void CreateMapPlayerMessages()
    {
        CreateMap<SFC.Player.Messages.Events.Player.General.PlayerCreated, CreatePlayerCommand>().IgnoreAllNonExisting();

        CreateMap<SFC.Player.Messages.Events.Player.General.PlayerUpdated, UpdatePlayerCommand>().IgnoreAllNonExisting();

        CreateMap<SFC.Player.Messages.Events.Player.General.PlayerUpdated, CreatePlayerCommand>().IgnoreAllNonExisting();

        CreateMap<IEnumerable<SFC.Player.Messages.Models.Player.Player>, CreatePlayersCommand>()
            .ForMember(p => p.Players, d => d.MapFrom(z => z));

        CreateMap<SFC.Player.Messages.Commands.Player.SeedPlayers, CreatePlayersCommand>();

        CreateMap<SFC.Player.Messages.Models.Player.Player, PlayerDto>()
            .ForPath(p => p.Stats!.Values, d => d.MapFrom(z => z.Stats))
            .ForPath(p => p.Stats!.Points, d => d.MapFrom(z => z.Points))
            .ForPath(p => p.Profile!.General, d => d.MapFrom(z => z.GeneralProfile))
            .ForPath(p => p.Profile!.Football, d => d.MapFrom(z => z.FootballProfile))
            .ForPath(p => p.Profile!.General.Photo, d => d.MapFrom(z => z.Photo))
            .ForPath(p => p.Profile!.General.Availability, d => d.MapFrom(z => z.Availability))
            .ForPath(p => p.Profile!.General.Tags, d => d.MapFrom(z => z.Tags));

        // stats
        CreateMap<SFC.Player.Messages.Models.Player.PlayerStat, PlayerStatValueDto>()
            .ForPath(p => p.Type, d => d.MapFrom(z => z.TypeId));
        CreateMap<SFC.Player.Messages.Models.Player.PlayerStatPoints, PlayerStatPointsDto>();

        // general profile
        CreateMap<SFC.Player.Messages.Models.Player.PlayerGeneralProfile, PlayerGeneralProfileDto>();
        CreateMap<SFC.Player.Messages.Models.Player.PlayerPhoto, PlayerPhotoDto>();
        CreateMap<SFC.Player.Messages.Models.Player.PlayerAvailability, PlayerAvailabilityDto>();
        CreateMap<SFC.Player.Messages.Models.Player.PlayerAvailableDay, DayOfWeek>().ConvertUsing(day => day.Day);
        CreateMap<SFC.Player.Messages.Models.Player.PlayerTag, string>().ConvertUsing(tag => tag.Value);

        // football profile
        CreateMap<SFC.Player.Messages.Models.Player.PlayerFootballProfile, PlayerFootballProfileDto>()
            .ForPath(p => p.AdditionalPosition, d => d.MapFrom(z => z.AdditionalPositionId))
            .ForPath(p => p.Position, d => d.MapFrom(z => z.PositionId))
            .ForPath(p => p.GameStyle, d => d.MapFrom(z => z.GameStyleId))
            .ForPath(p => p.WorkingFoot, d => d.MapFrom(z => z.WorkingFootId));
    }

    private void CreateMapPlayerContracts()
    {
        CreateMap<long, SFC.Player.Contracts.Messages.Player.General.Get.GetPlayerRequest>()
            .ConvertUsing(id => new SFC.Player.Contracts.Messages.Player.General.Get.GetPlayerRequest { Id = id });

        CreateMap<SFC.Player.Contracts.Models.Player.General.Player, PlayerDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerProfile, PlayerProfileDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerGeneralProfile, PlayerGeneralProfileDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerAvailability, PlayerAvailabilityDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerFootballProfile, PlayerFootballProfileDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerStats, PlayerStatsDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerStatPoints, PlayerStatPointsDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.PlayerStatValue, PlayerStatValueDto>();
    }

    #endregion Player

    #region Team

    private void CreateMapTeamMessages()
    {
        // data
        // events
        CreateMap<SFC.Team.Messages.Events.Team.Data.DataInitialized, ResetTeamDataCommand>().IgnoreAllNonExisting();
        // models
        CreateMap<SFC.Team.Messages.Models.Data.DataValue, TeamPlayerStatusDto>();

        // domain
        // team
        // events
        CreateMap<SFC.Team.Messages.Events.Team.General.TeamCreated, CreateTeamCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Team.Messages.Events.Team.General.TeamUpdated, UpdateTeamCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Team.Messages.Events.Team.General.TeamUpdated, CreateTeamCommand>().IgnoreAllNonExisting();
        // commands
        CreateMap<SFC.Team.Messages.Commands.Team.General.SeedTeams, CreateTeamsCommand>();
        // models
        CreateMap<IEnumerable<SFC.Team.Messages.Models.Team.General.Team>, CreateTeamsCommand>()
           .ForMember(p => p.Teams, d => d.MapFrom(z => z));
        CreateMap<SFC.Team.Messages.Models.Team.General.Team, TeamDto>()
           .ForPath(p => p.Profile!.General, d => d.MapFrom(z => z.GeneralProfile))
           .ForPath(p => p.Profile!.Financial, d => d.MapFrom(z => z.FinancialProfile))
           .ForPath(p => p.Profile!.Inventary.Shirts, d => d.MapFrom(z => z.Shirts))
           .ForPath(p => p.Profile!.General.Logo, d => d.MapFrom(z => z.Logo))
           .ForPath(p => p.Profile!.General.Availability, d => d.MapFrom(z => z.Availability))
           .ForPath(p => p.Profile!.General.Tags, d => d.MapFrom(z => z.Tags));
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamGeneralProfile, TeamGeneralProfileDto>();
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamFinancialProfile, TeamFinancialProfileDto>();
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamInventaryProfile, TeamInventaryProfileDto>();
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamLogo, TeamLogoDto>();
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamAvailability, TeamAvailabilityDto>();
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamTag, string>().ConvertUsing(tag => tag.Value);
        CreateMap<SFC.Team.Messages.Models.Team.General.TeamShirt, int>().ConvertUsing(shirt => shirt.ShirtId);
        // team player
        // events
        CreateMap<SFC.Team.Messages.Events.Team.Player.TeamPlayerCreated, CreateTeamPlayerCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Team.Messages.Events.Team.Player.TeamPlayerUpdated, UpdateTeamPlayerCommand>().IgnoreAllNonExisting();
        // models
        CreateMap<IEnumerable<SFC.Team.Messages.Models.Team.Player.TeamPlayer>, CreateTeamPlayersCommand>()
           .ForMember(p => p.TeamPlayers, d => d.MapFrom(z => z));
        CreateMap<SFC.Team.Messages.Models.Team.Player.TeamPlayer, TeamPlayerDto>();
    }

    #endregion Team

    #region Invite

    private void CreateMapInviteMessages()
    {
        // data
        // events
        CreateMap<SFC.Invite.Messages.Events.Invite.Data.DataInitialized, ResetInviteDataCommand>();
        // models
        CreateMap<GameStatus, SFC.Invite.Messages.Models.Data.DataValue>();
        CreateMap<GamePlayerStatus, SFC.Invite.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamStatus, SFC.Invite.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamIndex, SFC.Invite.Messages.Models.Data.DataValue>();
        CreateMap<SFC.Invite.Messages.Models.Data.DataValue, InviteStatusDto>();

        // domain
        // game player
        // events
        CreateMap<SFC.Invite.Messages.Events.Invite.Game.Player.GamePlayerInviteUpdated, CreateGamePlayerCommand>()
            .ForMember(p => p.GamePlayer, d => d.MapFrom(z => z.Invite));
        // models
        CreateMap<SFC.Invite.Messages.Models.Invite.Game.Player.GamePlayerInvite, CreateGamePlayerDto>();

        // domain
        // game team
        // events
        CreateMap<SFC.Invite.Messages.Events.Invite.Game.Team.GameTeamInviteUpdated, CreateGameTeamCommand>()
            .ForMember(p => p.GameTeam, d => d.MapFrom(z => z.Invite));
        // models
        CreateMap<SFC.Invite.Messages.Models.Invite.Game.Team.GameTeamInvite, CreateGameTeamDto>();
    }

    #endregion Invite

    #region Request

    private void CreateMapRequestMessages()
    {
        // data
        // events
        CreateMap<SFC.Request.Messages.Events.Request.Data.DataInitialized, ResetRequestDataCommand>();
        // models
        CreateMap<GameStatus, SFC.Request.Messages.Models.Data.DataValue>();
        CreateMap<GamePlayerStatus, SFC.Request.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamStatus, SFC.Request.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamIndex, SFC.Request.Messages.Models.Data.DataValue>();
        CreateMap<SFC.Request.Messages.Models.Data.DataValue, RequestStatusDto>();

        // domain
        // game player
        // events
        CreateMap<SFC.Request.Messages.Events.Request.Game.Player.GamePlayerRequestUpdated, CreateGamePlayerCommand>()
            .ForMember(p => p.GamePlayer, d => d.MapFrom(z => z.Request));
        // models
        CreateMap<SFC.Request.Messages.Models.Request.Game.Player.GamePlayerRequest, CreateGamePlayerDto>();

        // domain
        // game team
        // events
        CreateMap<SFC.Request.Messages.Events.Request.Game.Team.GameTeamRequestUpdated, CreateGameTeamCommand>()
            .ForMember(p => p.GameTeam, d => d.MapFrom(z => z.Request));
        // models
        CreateMap<SFC.Request.Messages.Models.Request.Game.Team.GameTeamRequest, CreateGameTeamDto>();
    }

    #endregion Request

    #region Scheme

    private void CreateMapSchemeMessages()
    {
        // data
        // models
        CreateMap<GameStatus, SFC.Scheme.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamStatus, SFC.Scheme.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamIndex, SFC.Scheme.Messages.Models.Data.DataValue>();
    }

    #endregion Scheme

    #region Game

    private void CreateMapGameMessages()
    {
        // data
        //commands
        CreateMap<SFC.Game.Messages.Commands.Data.InitializeData, ResetDataCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Game.Messages.Commands.Invite.Data.InitializeData, ResetInviteDataCommand>();
        CreateMap<SFC.Game.Messages.Commands.Request.Data.InitializeData, ResetRequestDataCommand>();
        // models
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, FootballPositionDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, GameStyleDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, StatCategoryDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, StatSkillDto>();
        CreateMap<SFC.Game.Messages.Models.Data.StatTypeDataValue, StatTypeDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, WorkingFootDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, ShirtDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, InviteStatusDto>();
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, RequestStatusDto>();

        // game data
        CreateMap<GameStatus, SFC.Game.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamStatus, SFC.Game.Messages.Models.Data.DataValue>();
        CreateMap<GamePlayerStatus, SFC.Game.Messages.Models.Data.DataValue>();
        CreateMap<GameTeamIndex, SFC.Game.Messages.Models.Data.DataValue>();

        // game
        // commands
        CreateMap<SFC.Game.Messages.Commands.Team.Data.InitializeData, ResetTeamDataCommand>().IgnoreAllNonExisting();

        //models
        CreateMap<SFC.Game.Messages.Models.Data.DataValue, TeamPlayerStatusDto>();

        // game
        // events
        CreateMap<GameEntity, SFC.Game.Messages.Events.Game.General.GameCreated>()
            .ForMember(p => p.Game, d => d.MapFrom(z => z));
        CreateMap<GameEntity, SFC.Game.Messages.Events.Game.General.GameUpdated>()
            .ForMember(p => p.Game, d => d.MapFrom(z => z));
        CreateMap<IEnumerable<GameEntity>, SFC.Game.Messages.Events.Game.General.GamesSeeded>()
           .ForMember(p => p.Games, d => d.MapFrom(z => z));
        //commands
        CreateMap<IEnumerable<GameEntity>, SeedGames>()
            .ForMember(p => p.Games, d => d.MapFrom(z => z));
        // models
        CreateMap<GameEntity, Messages.Models.Game.General.Game>();
        CreateMap<GameAvailability, SFC.Game.Messages.Models.Game.General.GameAvailability>();
        CreateMap<GameFinancialProfile, SFC.Game.Messages.Models.Game.General.GameFinancialProfile>();
        CreateMap<GameInventaryProfile, SFC.Game.Messages.Models.Game.General.GameInventaryProfile>();
        CreateMap<GameGeneralProfile, SFC.Game.Messages.Models.Game.General.GameGeneralProfile>();
        CreateMap<GameTag, SFC.Game.Messages.Models.Game.General.GameTag>();

        // game team
        // events
        CreateMap<IEnumerable<GameTeam>, GameTeamsSeeded>()
            .ForMember(p => p.GameTeams, d => d.MapFrom(z => z));
        CreateMap<GameTeam, GameTeamUpdated>()
            .ForMember(p => p.GameTeam, d => d.MapFrom(z => z));
        CreateMap<GameTeam, GameTeamCreated>()
            .ForMember(p => p.GameTeam, d => d.MapFrom(z => z));
        // commands
        CreateMap<IEnumerable<GameTeam>, SeedGameTeams>()
            .ForMember(p => p.GameTeams, d => d.MapFrom(z => z));
        // models
        CreateMap<GameTeam, Messages.Models.Game.Team.General.GameTeam>();

        // game team player
        // events
        CreateMap<IEnumerable<GameTeamPlayer>, GameTeamPlayersSeeded>()
            .ForMember(p => p.GameTeamPlayers, d => d.MapFrom(z => z));
        CreateMap<GameTeamPlayer, GameTeamPlayerUpdated>()
            .ForMember(p => p.GameTeamPlayer, d => d.MapFrom(z => z));
        CreateMap<GameTeamPlayer, GameTeamPlayerCreated>()
            .ForMember(p => p.GameTeamPlayer, d => d.MapFrom(z => z));
        // commands
        CreateMap<IEnumerable<GameTeamPlayer>, SeedGameTeamPlayers>()
            .ForMember(p => p.GameTeamPlayers, d => d.MapFrom(z => z));
        // models
        CreateMap<GameTeamPlayer, Messages.Models.Game.Team.Player.GameTeamPlayer>();

        // game player
        // events
        CreateMap<IEnumerable<GamePlayer>, SFC.Game.Messages.Events.Game.Player.GamePlayersSeeded>()
            .ForMember(p => p.GamePlayers, d => d.MapFrom(z => z));
        CreateMap<GamePlayer, SFC.Game.Messages.Events.Game.Player.GamePlayerUpdated>()
            .ForMember(p => p.GamePlayer, d => d.MapFrom(z => z));
        CreateMap<GamePlayer, SFC.Game.Messages.Events.Game.Player.GamePlayerCreated>()
            .ForMember(p => p.GamePlayer, d => d.MapFrom(z => z));
        // commands
        CreateMap<IEnumerable<GamePlayer>, SFC.Game.Messages.Commands.Game.Player.SeedGamePlayers>()
            .ForMember(p => p.GamePlayers, d => d.MapFrom(z => z));
        // models
        CreateMap<GamePlayer, SFC.Game.Messages.Models.Game.Player.GamePlayer>();
    }

    #endregion Game
}