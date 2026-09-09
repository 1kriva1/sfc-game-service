using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;
using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class InviteExchangeValue
{
    public DataExchange<InviteDataDependentExchange> Data { get; set; } = default!;

    public InviteDomainExchange Domain { get; set; } = default!;
}

public class InviteDataDependentExchange
{
    public DataDependentExchange Game { get; set; } = default!;
}

public class InviteDomainExchange
{
    public InviteGameDomainExchange Game { get; set; } = default!;
}

public class InviteGameDomainExchange
{
    public DomainExchange<InviteGamePlayerDomainEventsExchange> Player { get; set; } = default!;

    public DomainExchange<InviteGameTeamDomainEventsExchange> Team { get; set; } = default!;
}

public class InviteGamePlayerDomainEventsExchange
{
    public Exchange Updated { get; set; } = default!;
}

public class InviteGameTeamDomainEventsExchange
{
    public Exchange Updated { get; set; } = default!;
}