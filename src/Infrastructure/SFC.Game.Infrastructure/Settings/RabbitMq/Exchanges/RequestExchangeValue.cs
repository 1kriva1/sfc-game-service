using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;
using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class RequestExchangeValue
{
    public DataExchange<RequestDataDependentExchange> Data { get; set; } = default!;

    public RequestDomainExchange Domain { get; set; } = default!;
}

public class RequestDataDependentExchange
{
    public DataDependentExchange Game { get; set; } = default!;
}

public class RequestDomainExchange
{
    public RequestGameDomainExchange Game { get; set; } = default!;
}

public class RequestGameDomainExchange
{
    public DomainExchange<RequestGamePlayerDomainEventsExchange> Player { get; set; } = default!;

    public DomainExchange<RequestGameTeamDomainEventsExchange> Team { get; set; } = default!;
}

public class RequestGamePlayerDomainEventsExchange
{
    public Exchange Updated { get; set; } = default!;
}

public class RequestGameTeamDomainEventsExchange
{
    public Exchange Updated { get; set; } = default!;
}