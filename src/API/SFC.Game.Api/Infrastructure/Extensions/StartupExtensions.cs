using SFC.Game.Application;
using SFC.Game.Infrastructure;
using SFC.Game.Infrastructure.Constants;
using SFC.Game.Infrastructure.Persistence;

namespace SFC.Game.Api.Infrastructure.Extensions;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddApplicationServices();

        builder.AddPersistenceServices();

        builder.AddInfrastructureServices();

        builder.AddApiServices();

        builder.AddControllers();

        builder.AddLocalization();

        builder.AddAuthentication();

        if (builder.Environment.IsDevelopment())
        {
            builder.AddSwagger();
        }

        builder.Services.AddHealthChecks();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // global cors policy
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(CommonConstants.PaginationHeaderKey)
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());// allow credentials

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
        }

        app.UseHttpsRedirection();

        app.UseLocalization();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCustomExceptionHandler();

        app.UseHangfireDashboard();

        app.MapHealthChecks("/health");

        app.MapControllers();

        app.UseGrpc();

        return app;
    }
}