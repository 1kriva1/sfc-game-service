using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class PlayerExchangeValue
{
    public PlayerDomainExchange Domain { get; set; } = default!;
}

public class PlayerDomainExchange
{
    public DomainExchange<PlayerDomainEventsExchange> Player { get; set; } = default!;
}

public class PlayerDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}