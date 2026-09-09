using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Application;
using SFC.Game.Application.Common.Constants;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Infrastructure.Extensions;

public static class LocalizationExtensions
{
    public static void AddLocalization(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = CommonConstants.ResourcePath);

        builder.Services.Configure<RequestLocalizationOptions>(options => options.SetDefaultCulture(CommonConstants.SupportedCultures[0])
                .AddSupportedCultures(CommonConstants.SupportedCultures)
                .AddSupportedUICultures(CommonConstants.SupportedCultures));
    }

    public static void UseLocalization(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        IOptions<RequestLocalizationOptions> localizationOptions =
            app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;

        app.UseRequestLocalization(localizationOptions.Value);

        // inject localizer explicity for error messages
        IStringLocalizer<Resources> localizer =
            app.Services.GetService<IStringLocalizer<Resources>>()!;

        Localization.Configure(localizer);
    }
}