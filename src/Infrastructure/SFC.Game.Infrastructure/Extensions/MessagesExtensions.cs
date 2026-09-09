using AutoMapper;

using SFC.Game.Application.Interfaces.Game.Data.Models;
using SFC.Game.Messages.Commands.Common;
using SFC.Game.Messages.Events.Game.Data;
using SFC.Game.Messages.Models.Data;

using InviteDataValue = SFC.Invite.Messages.Models.Data.DataValue;
using InviteInitializeData = SFC.Invite.Messages.Commands.Game.Data.InitializeData;
using RequestDataValue = SFC.Request.Messages.Models.Data.DataValue;
using RequestInitializeData = SFC.Request.Messages.Commands.Game.Data.InitializeData;
using SchemeDataValue = SFC.Scheme.Messages.Models.Data.DataValue;
using SchemeInitializeData = SFC.Scheme.Messages.Commands.Game.Data.InitializeData;

namespace SFC.Game.Infrastructure.Extensions;

public static class MessagesExtensions
{
    public static DataInitialized BuildGameDataInitializedEvent(this IMapper mapper, GetAllGameDataModel model)
    {
        DataInitialized message = new()
        {
            GameStatuses = mapper.Map<IEnumerable<DataValue>>(model.GameStatuses),
            GameTeamStatuses = mapper.Map<IEnumerable<DataValue>>(model.GameTeamStatuses),
            GamePlayerStatuses = mapper.Map<IEnumerable<DataValue>>(model.GamePlayerStatuses),
            GameTeamIndexes = mapper.Map<IEnumerable<DataValue>>(model.GameTeamIndexes)
        };

        return message;
    }

    public static InviteInitializeData BuildInitializeDataCommand(this IMapper mapper, GetInviteDataModel model)
    {
        InviteInitializeData message = new()
        {
            GameStatuses = mapper.Map<IEnumerable<InviteDataValue>>(model.GameStatuses),
            GamePlayerStatuses = mapper.Map<IEnumerable<InviteDataValue>>(model.GamePlayerStatuses),
            GameTeamStatuses = mapper.Map<IEnumerable<InviteDataValue>>(model.GameTeamStatuses),
            GameTeamIndexes = mapper.Map<IEnumerable<InviteDataValue>>(model.GameTeamIndexes)
        };

        return message;
    }

    public static RequestInitializeData BuildInitializeDataCommand(this IMapper mapper, GetRequestDataModel model)
    {
        RequestInitializeData message = new()
        {
            GameStatuses = mapper.Map<IEnumerable<RequestDataValue>>(model.GameStatuses),
            GamePlayerStatuses = mapper.Map<IEnumerable<RequestDataValue>>(model.GamePlayerStatuses),
            GameTeamStatuses = mapper.Map<IEnumerable<RequestDataValue>>(model.GameTeamStatuses),
            GameTeamIndexes = mapper.Map<IEnumerable<RequestDataValue>>(model.GameTeamIndexes)
        };

        return message;
    }

    public static SchemeInitializeData BuildInitializeDataCommand(this IMapper mapper, GetSchemeDataModel model)
    {
        SchemeInitializeData message = new()
        {
            GameStatuses = mapper.Map<IEnumerable<SchemeDataValue>>(model.GameStatuses),
            GameTeamStatuses = mapper.Map<IEnumerable<SchemeDataValue>>(model.GameTeamStatuses),
            GameTeamIndexes = mapper.Map<IEnumerable<SchemeDataValue>>(model.GameTeamIndexes)
        };

        return message;
    }

    public static T SetCommandInitiator<T>(this T command, string initiator) where T : InitiatorCommand
    {
        command.Initiator = initiator;
        return command;
    }
}