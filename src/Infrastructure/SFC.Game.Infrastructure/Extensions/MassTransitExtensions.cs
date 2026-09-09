using System.Globalization;
using System.Reflection;

using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SFC.Game.Infrastructure.Extensions;
using SFC.Game.Infrastructure.Settings.RabbitMq;
using SFC.Game.Messages.Commands.Common;
using SFC.Game.Messages.Commands.Game.General;
using SFC.Game.Messages.Commands.Game.Player;
using SFC.Game.Messages.Commands.Game.Team.General;
using SFC.Game.Messages.Commands.Game.Team.Player;
using SFC.Game.Messages.Events.Game.Data;
using SFC.Game.Messages.Events.Game.General;
using SFC.Game.Messages.Events.Game.Player;
using SFC.Game.Messages.Events.Game.Team.General;
using SFC.Game.Messages.Events.Game.Team.Player;

namespace SFC.Game.Infrastructure.Extensions;

public static class MassTransitExtensions
{
    private const string EXCHANGE_ENDPOINT_SHORT_ADDRESS = "exchange";
    private const string EXCHANGE_ENDPOINT_AUTO_DELETE_PART = "autodelete";

    #region Public

    public static IServiceCollection AddMassTransit(this WebApplicationBuilder builder)
    {
        return builder.Services.AddMassTransit(masTransitConfigure =>
        {
            masTransitConfigure.AddConsumers(Assembly.GetExecutingAssembly());

            masTransitConfigure.UsingRabbitMq((context, rabbitMqConfigure) =>
            {
                RabbitMqSettings settings = builder.Configuration.GetRabbitMqSettings();

                string rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMq")!;

                rabbitMqConfigure.Host(new Uri(rabbitMqConnectionString), settings.Name, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                rabbitMqConfigure.UseRetries(settings.Retry);

                rabbitMqConfigure.AddExchanges(builder.Environment, settings.Exchanges);

                rabbitMqConfigure.ConfigureEndpoints(context);

                MapEndpoints(settings.Exchanges, builder.Environment);
            });
        });
    }

    public static string BuildExchangeRoutingKey(this string initiator, string key)
        => $"{key.ToLower(CultureInfo.CurrentCulture)}.{initiator.ToString().ToLower(CultureInfo.CurrentCulture)}";

    #endregion Public

    #region Private

    private static void AddExchanges(
        this IRabbitMqBusFactoryConfigurator configure,
        IWebHostEnvironment environment,
        RabbitMqExchangesSettings exchangesSettings)
    {
        // "sfc.game.data.initialized"
        configure.AddExchange<DataInitialized>(exchangesSettings.Game.Value.Data.Source.Initialized);

        // "sfc.game.game.created"
        configure.AddExchange<GameCreated>(exchangesSettings.Game.Value.Domain.Game.Events.Created);

        // "sfc.game.game.updated"
        configure.AddExchange<GameUpdated>(exchangesSettings.Game.Value.Domain.Game.Events.Updated);

        // "sfc.game.team.created"
        configure.AddExchange<GameTeamCreated>(exchangesSettings.Game.Value.Domain.Team.Team.Events.Created);

        // "sfc.game.team.updated"
        configure.AddExchange<GameTeamUpdated>(exchangesSettings.Game.Value.Domain.Team.Team.Events.Updated);

        // "sfc.game.player.created"
        configure.AddExchange<GamePlayerCreated>(exchangesSettings.Game.Value.Domain.Player.Events.Created);

        // "sfc.game.player.updated"
        configure.AddExchange<GamePlayerUpdated>(exchangesSettings.Game.Value.Domain.Player.Events.Updated);

        // "sfc.game.team.player.created"
        configure.AddExchange<GameTeamPlayerCreated>(exchangesSettings.Game.Value.Domain.Team.Player.Events.Created);

        // "sfc.game.team.player.updated"
        configure.AddExchange<GameTeamPlayerUpdated>(exchangesSettings.Game.Value.Domain.Team.Player.Events.Updated);

        if (environment.IsDevelopment())
        {
            // "sfc.game.games.seed"
            configure.AddExchange<SeedGames>(exchangesSettings.Game.Value.Domain.Game.Seed.Seed, exchangesSettings.Game.Key);

            // "sfc.game.games.seeded"
            configure.AddExchange<GamesSeeded>(exchangesSettings.Game.Value.Domain.Game.Seed.Seeded);

            // "sfc.game.team.teams.seed"
            configure.AddExchange<SeedGameTeams>(exchangesSettings.Game.Value.Domain.Team.Team.Seed.Seed, exchangesSettings.Game.Key);

            // "sfc.game.team.teams.seeded"
            configure.AddExchange<GameTeamsSeeded>(exchangesSettings.Game.Value.Domain.Team.Team.Seed.Seeded);

            // "sfc.game.players.seed"
            configure.AddExchange<SeedGamePlayers>(exchangesSettings.Game.Value.Domain.Player.Seed.Seed, exchangesSettings.Game.Key);

            // "sfc.game.players.seeded"
            configure.AddExchange<GamePlayersSeeded>(exchangesSettings.Game.Value.Domain.Player.Seed.Seeded);

            // "sfc.game.team.players.seed"
            configure.AddExchange<SeedGameTeamPlayers>(exchangesSettings.Game.Value.Domain.Team.Player.Seed.Seed, exchangesSettings.Game.Key);

            // "sfc.game.teams.player.seeded"
            configure.AddExchange<GameTeamPlayersSeeded>(exchangesSettings.Game.Value.Domain.Team.Player.Seed.Seeded);
        }

        // exclude base command
        configure.Publish<InitiatorCommand>(p => p.Exclude = true);
    }

    private static void MapEndpoints(RabbitMqExchangesSettings exchangesSettings, IWebHostEnvironment environment)
    {
        EndpointConvention.Map<SFC.Game.Messages.Commands.Data.RequireData>(exchangesSettings.Game.Value.Data.Dependent.Data.RequireInitialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Game.Messages.Commands.Team.Data.RequireData>(exchangesSettings.Game.Value.Data.Dependent.Team.RequireInitialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Game.Messages.Commands.Invite.Data.RequireData>(exchangesSettings.Game.Value.Data.Dependent.Invite.RequireInitialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Game.Messages.Commands.Request.Data.RequireData>(exchangesSettings.Game.Value.Data.Dependent.Request.RequireInitialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Invite.Messages.Commands.Game.Data.InitializeData>(exchangesSettings.Invite.Value.Data.Dependent.Game.Initialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Request.Messages.Commands.Game.Data.InitializeData>(exchangesSettings.Request.Value.Data.Dependent.Game.Initialize.GetExchangeEndpointUri());

        EndpointConvention.Map<SFC.Scheme.Messages.Commands.Game.Data.InitializeData>(exchangesSettings.Scheme.Value.Data.Dependent.Game.Initialize.GetExchangeEndpointUri());

        if (environment.IsDevelopment())
        {
            // "sfc.identity.users.seed.require"
            EndpointConvention.Map<SFC.Identity.Messages.Commands.User.RequireUsersSeed>(exchangesSettings.Identity.Value.Domain.User.Seed.RequireSeed.GetExchangeEndpointUri());

            // "sfc.player.players.seed.require"
            EndpointConvention.Map<SFC.Player.Messages.Commands.Player.RequirePlayersSeed>(exchangesSettings.Player.Value.Domain.Player.Seed.RequireSeed.GetExchangeEndpointUri());

            // "sfc.team.teams.seed.require"
            EndpointConvention.Map<SFC.Team.Messages.Commands.Team.General.RequireTeamsSeed>(exchangesSettings.Team.Value.Domain.Team.Seed.RequireSeed.GetExchangeEndpointUri());

            // "sfc.team.player.seed.require"
            EndpointConvention.Map<SFC.Team.Messages.Commands.Team.Player.RequireTeamPlayersSeed>(exchangesSettings.Team.Value.Domain.Player.Seed.RequireSeed.GetExchangeEndpointUri());
        }
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange)
        where T : class
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Publish<T>(x =>
        {
            x.AutoDelete = true;
            x.ExchangeType = exchange.Type;
        });
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange, Func<SendContext<T>, string?> formatter)
        where T : class
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Send<T>(x => x.UseRoutingKeyFormatter(formatter));
        configure.Publish<T>(x =>
        {
            x.AutoDelete = true;
            x.ExchangeType = exchange.Type;
        });
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange, string key)
        where T : InitiatorCommand
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Send<T>(x => x.UseRoutingKeyFormatter(context => context.Message.Initiator.BuildExchangeRoutingKey(key)));
        configure.Publish<T>(x =>
        {
            x.AutoDelete = true;
            x.ExchangeType = exchange.Type;
        });
    }

    private static void UseRetries(this IRabbitMqBusFactoryConfigurator configure, RabbitMqRetrySettings settings)
    {
        configure.UseDelayedRedelivery(r =>
            r.Intervals(settings.Intervals.Select(i => TimeSpan.FromMinutes(i)).ToArray()));
        configure.UseMessageRetry(r => r.Immediate(settings.Limit));
    }

    private static Uri GetExchangeEndpointUri(this Message exchange) =>
       new($"{EXCHANGE_ENDPOINT_SHORT_ADDRESS}:{exchange.Name}?{EXCHANGE_ENDPOINT_AUTO_DELETE_PART}={true}");

    #endregion Private
}