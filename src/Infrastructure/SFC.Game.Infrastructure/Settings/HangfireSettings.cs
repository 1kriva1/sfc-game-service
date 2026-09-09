namespace SFC.Game.Infrastructure.Settings;
public class HangfireSettings
{
    public const string SectionKey = "Hangfire";

    public string SchemaNamePrefix { get; set; } = null!;

    public HangfireAutomaticRetrySettings Retry { get; set; } = default!;

    public HangfireDashboardSettings Dashboard { get; set; } = default!;
}

public class HangfireAutomaticRetrySettings
{
    public int Attempts { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
    public int[] DelaysInSeconds { get; set; } = [];
#pragma warning restore CA1819 // Properties should not return arrays
}

public class HangfireDashboardSettings
{
    public string Title { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}