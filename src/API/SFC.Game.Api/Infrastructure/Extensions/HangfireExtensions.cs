using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings;

namespace SFC.Game.Api.Infrastructure.Extensions;

public static class HangfireExtensions
{
    public static void UseHangfireDashboard(this WebApplication app)
    {
        HangfireSettings settings = app.Configuration.GetHangfireSettings();

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = settings.Dashboard.Title,
            IsReadOnlyFunc = (context) => !app.Environment.IsDevelopment(),
            Authorization = [
                new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions{
                    Users = [
                        new BasicAuthAuthorizationUser {
                            Login = settings.Dashboard.Login,
                            PasswordClear  = settings.Dashboard.Password
                        }
                    ]
                })
            ]
        });
    }
}