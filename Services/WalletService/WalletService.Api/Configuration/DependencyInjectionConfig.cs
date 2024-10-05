using Framework.Core.Mediator;
using MediatR;
using Framework.WebApi.Core.Configuration;
using Framework.Core.Data;
using Framework.Core.MongoDb;
using MassTransit;
using Framework.Core.OpenTelemetry;
using Framework.MessageBus;
using WalletService.Domain.Models.Repositories;
using WalletService.Infra.Data.Repository;
using WalletService.Domain.DomainEvents;
using WalletService.Infra;
using WalletService.Application.Events;
using WalletService.Application.Commands.Purchase;
using WalletService.Application.Commands.Sell;

namespace WalletService.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBusConfiguration(builder.Configuration);
            builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ApiConfigurationWebApiCore.RegisterServices(builder.Services);

            builder.Services.RegisterRepositories();
            builder.Services.RegisterCommands();
            builder.Services.RegisterRules();
            builder.Services.RegisterQueries();
            builder.Services.RegisterIntegrationService();
            builder.Services.RegisterEvents();
            builder.RegisterEventStored();
            builder.RegisterOpenTelemetry();
            builder.Services.AddMessageBus();
            builder.Services.AddMemoryCache();

        }

        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var messageQueueConnection = new
            {
                Host = configuration.GetSection("MessageQueueConnection").GetSection("host").Value,
                Username = configuration.GetSection("MessageQueueConnection").GetSection("username").Value,
                Passwoord = configuration.GetSection("MessageQueueConnection").GetSection("password").Value,
            };
            services.AddMassTransit(config =>
            {
                config.AddEntityFrameworkOutbox<WalletContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(1);
                    o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);

                    o.UseBusOutbox();
                });

                // config.AddConsumer<Activity_ActivityAcceptedIntegrationHandle>();
                // config.AddConsumer<Activity_ActivityRejectedIntegrationHandle>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(messageQueueConnection.Host, x =>
                    {
                        x.Username(messageQueueConnection.Username);
                        x.Password(messageQueueConnection.Passwoord);
                        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));
                        cfg.SingleActiveConsumer = true;

                        cfg.ConfigureEndpoints(ctx);
                    });
                });
            });
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStockWalletRepository, StockWalletRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddStockWalletCommand, AddStockWalletCommandOutput>, AddStockWalletCommandHandler>();
            services.AddScoped<IRequestHandler<DecreaseStockWalletCommand, DecreaseStockWalletCommandOutput>, DecreaseStockWalletCommandHandler>();
        }

        public static void RegisterRules(this IServiceCollection services)
        {


        }


        public static void RegisterQueries(this IServiceCollection services)
        {


        }

        public static void RegisterIntegrationService(this IServiceCollection services)
        {

        }

        public static void RegisterEvents(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<StockWalletAddedEvent>, StockWalletAddedEventHandler>();
            services.AddScoped<INotificationHandler<StockWalletDecreasedEvent>, StockWalletDecreasedEventHandler>();
        }

        public static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}
