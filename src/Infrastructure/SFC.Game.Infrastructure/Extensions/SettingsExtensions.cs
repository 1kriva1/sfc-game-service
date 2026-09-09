using Microsoft.Extensions.Configuration;

using SFC.Game.Application.Common.Settings;
using SFC.Game.Infrastructure.Constants;
using SFC.Game.Infrastructure.Settings;
using SFC.Game.Infrastructure.Settings.Grpc;
using SFC.Game.Infrastructure.Settings.RabbitMq;

namespace SFC.Game.Infrastructure.Extensions;

public static class SettingsExtensions
{
    public static bool UseAuthentication(this ConfigurationManager configuration)
        => configuration.GetValue<bool>(SettingConstants.Authentication, true);

    public static IdentitySettings GetIdentitySettings(this IConfiguration configuration)
        => configuration?.GetSection(IdentitySettings.SectionKey)
                        .Get<IdentitySettings>()!;

    public static RabbitMqSettings GetRabbitMqSettings(this IConfiguration configuration)
        => configuration?.GetSection(RabbitMqSettings.SectionKey)
                        .Get<RabbitMqSettings>()!;

    public static CacheSettings GetCacheSettings(this IConfiguration configuration)
        => configuration?.GetSection(CacheSettings.SectionKey)
                        .Get<CacheSettings>()!;

    public static GrpcSettings GetGrpcSettings(this IConfiguration configuration)
        => configuration.GetSection(GrpcSettings.SectionKey)
                        .Get<GrpcSettings>()!;

    public static GrpcSettingsDevelopment GetDevelopmentGrpcSettings(this IConfiguration configuration)
        => configuration.GetSection(GrpcSettingsDevelopment.SectionKey)
                        .Get<GrpcSettingsDevelopment>()!;

    public static HangfireSettings GetHangfireSettings(this IConfiguration configuration)
        => configuration.GetSection(HangfireSettings.SectionKey)
                        .Get<HangfireSettings>()!;

    public static RedisSettings GetRedisSettings(this IConfiguration configuration)
        => configuration.GetSection(RedisSettings.SectionKey)
                        .Get<RedisSettings>()!;
}