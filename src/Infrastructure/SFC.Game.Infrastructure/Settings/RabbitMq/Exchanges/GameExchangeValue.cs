using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;
using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class GameExchangeValue
{
    public DataExchange<GameDataDependentExchange> Data { get; set; } = default!;

    public GameDomainExchange Domain { get; set; } = default!;
}

public class GameDataDependentExchange
{
    public DataDependentExchange Data { get; set; } = default!;

    public DataDependentExchange Team { get; set; } = default!;

    public DataDependentExchange Invite { get; set; } = default!;

    public DataDependentExchange Request { get; set; } = default!;
}

public class GameDomainExchange
{
    /// <summary>
    /// Should be replaces by service(s) that required seed for Games
    /// </summary>
    public DomainExchange<GameGameDomainEventsExchange> Game { get; set; } = default!;

    public GameTeamDomainExchange Team { get; set; } = default!;

    public DomainExchange<GamePlayerDomainEventsExchange> Player { get; set; } = default!;
}

public class GameTeamDomainExchange
{
    public DomainExchange<GameTeamTeamDomainEventsExchange> Team { get; set; } = default!;

    public DomainExchange<GameTeamPlayerDomainEventsExchange> Player { get; set; } = default!;
}

public class GameGameDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}

public class GameTeamTeamDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}

public class GamePlayerDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}

public class GameTeamPlayerDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}