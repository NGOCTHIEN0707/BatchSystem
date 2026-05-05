using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Application.ProductionOrderDispatchers;
using BatchSystem.Domain.Alarms;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.SeedWork;
using BatchSystem.Host;
using BatchSystem.Host.Hubs;
using BatchSystem.Infrastructure.Communication;
using BatchSystem.Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
            cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
            cfg.RegisterServicesFromAssemblyContaining<Entity>();
        });

        services.AddLogging(config =>
        {
            config.ClearProviders();
            config.AddConsole();
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("BatchSystem"));
        });

        var config = builder.Configuration;
        services.Configure<MqttOptions>(config.GetSection("Mqtt"));
        services.AddHttpClient("RealtimeApi", client =>
        {
            client.BaseAddress = new Uri("https://batchsystem-webapi-hzethfbcbwfya7bh.japaneast-01.azurewebsites.net/");
        });

        services.AddSingleton<IManagedMqttClient, ManagedMqttClient>();
        services.AddScoped<IProductionOrderStatusHistoryRepository, ProductionOrderStatusHistoryRepository>();
        services.AddScoped<IOrderBatchStatusHistoryRepository, OrderBatchStatusHistoryRepository>();
        services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
        services.AddScoped<IOrderBatchRepository, OrderBatchRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IOrderBatchCommandPublisher, OrderBatchCommandPublisher>();
        services.AddScoped<IAlarmDefinitionRepository, AlarmDefinitionRepository>();
        services.AddScoped<IAlarmEventRepository, AlarmEventRepository>();
        services.AddScoped<IProductionOrderDispatcher, ProductionOrderDispatcher>();
        services.AddSignalR();

        services.AddHostedService<BatchStatusWorker>();
        
    })

   //.ConfigureWebHostDefaults(webBuilder =>
   //{
   //    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
   //    {
   //        webBuilder.UseUrls("http://localhost:5005");
   //    }

   //    webBuilder.Configure(app =>
   //    {
   //        app.UseRouting();

   //        app.UseCors();

   //        app.UseEndpoints(endpoints =>
   //        {
   //            endpoints.MapHub<ProcessDataHub>("/hubs/process-data");
   //            endpoints.MapHub<AlarmHub>("/hubs/alarms");
   //            endpoints.MapHub<StatusHub>("/hubs/status");
   //        });
   //    });
   //})


    .Build();

    await host.RunAsync();

