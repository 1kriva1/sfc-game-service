using SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;

namespace SFC.Game.Infrastructure.Settings.RabbitMq.Exchanges;
public class SchemeExchangeValue
{
    public DataExchange<SchemeDataDependentExchange> Data { get; set; } = default!;
}

public class SchemeDataDependentExchange
{
    public DataDependentExchange Game { get; set; } = default!;
}