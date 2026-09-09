using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Infrastructure.Extensions;
public static class EnvironmentExtensions
{
    public static bool IsRunningInContainer => Environment.GetEnvironmentVariable(EnvironmentConstants.RunningInContainer) == "true";
}