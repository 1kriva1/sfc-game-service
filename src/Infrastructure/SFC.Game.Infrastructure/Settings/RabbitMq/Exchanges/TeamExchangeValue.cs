using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;
using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class TeamExchangeValue
{
    public DataExchange<TeamDataDependentExchange> Data { get; set; } = default!;

    public TeamDomainExchange Domain { get; set; } = default!;
}

public class TeamDataDependentExchange
{
    public DataDependentExchange Game { get; set; } = default!;
}

public class TeamDomainExchange
{
    public DomainExchange<TeamDomainEventsExchange> Team { get; set; } = default!;

    public DomainExchange<TeamPlayerDomainEventsExchange> Player { get; set; } = default!;
}

public class TeamDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}

public class TeamPlayerDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}