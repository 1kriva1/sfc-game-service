namespace SFC.Game.Application.Common.Settings;
public class CacheSettings
{
    public const string SectionKey = "Cache";

    public bool Enabled { get; set; }

    public string InstanceName { get; set; } = default!;

    public RefreshCacheSettings Refresh { get; set; } = default!;

    public int AbsoluteExpirationInMinutes { get; set; }

    public int SlidingExpirationInMinutes { get; set; }

    public IEnumerable<string> RelatedInstances { get; set; } = [];
}

public class RefreshCacheSettings
{
    public string Cron { get; set; } = default!;
}