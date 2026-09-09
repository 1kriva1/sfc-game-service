using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class IdentityExchangeValue
{
    public IdentityDomainExchange Domain { get; set; } = default!;
}

public class IdentityDomainExchange
{
    public DomainExchange<IdentityDomainEventsExchange> User { get; set; } = default!;
}

public class IdentityDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;
}