using Framework.Core.Mediator;
using MediatR;
using Framework.WebApi.Core.Configuration;
using Framework.Core.Data;
using Framework.Core.MongoDb;
using MassTransit;
using Framework.Core.OpenTelemetry;
using Framework.MessageBus;
using StockService.Domain.Models.Repositories;
using StockService.Infra.Data.Repository;
using StockService.Domain.DomainEvents;
using StockService.Infra;
using StockService.Application.Events;
using StockService.Application.Commands.Purchase;
using StockService.Application.Commands.Sell;

namespace StockService.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBusConfiguration(builder.Configuration);
            builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ApiConfigurationWebApiCore.RegisterServices(builder.Services);
            // builder.Services.AddGraphQLServer()
            //     .AddQueryType<Query>()
            //     .RegisterDbContext<StockContext>()
            //     .AddFiltering()
            //     .AddSorting();

            //.AddSubscriptionType<ActivityQuerySubscription>()
            //.AddInMemorySubscriptions();
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
                config.AddEntityFrameworkOutbox<StockContext>(o =>
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
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockResultTransactionRepository, StockResultTransactionRepository>();
        }

        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<PurchaseCommand, PurchaseCommandOutput>, PurchaseCommandHandler>();
            services.AddScoped<IRequestHandler<SellCommand, SellCommandOutput>, SellCommandHandler>();
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
            services.AddScoped<INotificationHandler<TransactionPurchasedEvent>, TransactionPurchasedEventHandler>();
            services.AddScoped<INotificationHandler<TransactionSoldEvent>, TransactionSoldEventHandler>();


        }

        public static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}
