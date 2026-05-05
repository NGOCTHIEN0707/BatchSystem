using BatchSystem.Application.Commands.ProductionOrders.Create;
using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Application.ProductionOrderDispatchers;
using BatchSystem.Domain.Alarms;
using BatchSystem.Domain.Lines;
using BatchSystem.Domain.Logins;
using BatchSystem.Domain.Logins.StaffCodes;
using BatchSystem.Domain.Materials;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using BatchSystem.Domain.SeedWork;
using BatchSystem.Domain.Stations;
using BatchSystem.DTOs;
using BatchSystem.Hubs;
using BatchSystem.Infrastructure.Communication;
using BatchSystem.Infrastructure.Repositories;
using BatchSystem.Mapping;
using BatchSystem.TokenServices;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BatchSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BatchSystem"));
            });
            builder.Services.AddCors(options =>
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "BatchSystem",
                    ValidAudience = "BatchSystemClient",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("your-secret-key"))
                };
            });

            builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<IAlarmDefinitionRepository, AlarmDefinitionRepository>();
            builder.Services.AddScoped<IAlarmEventRepository, AlarmEventRepository>();
            builder.Services.AddScoped<ILineRepository, LineRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<IOrderBatchRepository, OrderBatchRepository>();
            builder.Services.AddScoped<IProductionOrderStatusHistoryRepository, ProductionOrderStatusHistoryRepository>();
            builder.Services.AddScoped<IOrderBatchStatusHistoryRepository, OrderBatchStatusHistoryRepository>();
            builder.Services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IStationRepository, StationRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IOrderBatchCommandPublisher, OrderBatchCommandPublisher>();
            builder.Services.AddScoped<IProductionOrderDispatcher, ProductionOrderDispatcher>();
            builder.Services.AddScoped<IStaffCodeRepository, StaffCodeRepository>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
                cfg.RegisterServicesFromAssemblyContaining<CreateProductionOrderCommandHandler>(); // MediatR nhìn được Assembly API
                cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>(); // MediatR nhìn được Infrastructure
                cfg.RegisterServicesFromAssemblyContaining<Entity>(); // MediatR nhìn được Domain
                //cfg.RegisterServicesFromAssemblyContaining<Buffer>();
            });
            builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("Mqtt"));
            builder.Services.AddAutoMapper(x => x.AddProfile<ModelToViewModelProfile>());
            builder.Services.AddSingleton<IManagedMqttClient, ManagedMqttClient>();
            builder.Services.AddSignalR();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<ProcessDataHub>("/hubs/process-data");
            app.MapHub<AlarmHub>("/hubs/alarms");
            app.MapHub<StatusHub>("/hubs/status");
            app.MapPost("/internal/realtime/process-data",
            async (
                ProcessDataRealtimeDto data,
                IHubContext<ProcessDataHub> hubContext,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("API received process data");

                await hubContext.Clients.All.SendAsync(
                    "processData",
                    data,
                    cancellationToken);

                return Results.Ok();
            });
            app.MapPost("/internal/realtime/status",
            async (
                object data,
                IHubContext<StatusHub> hubContext,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("API received status");

                await hubContext.Clients.All.SendAsync(
                    "StatusUpdated",
                    data,
                    cancellationToken);

                return Results.Ok();
            });
            app.MapPost("/internal/realtime/alarm",
            async (
                object data,
                IHubContext<AlarmHub> hubContext,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("API received alarm");

                await hubContext.Clients.All.SendAsync(
                    "ReceiveAlarmUpdate",
                    data,
                    cancellationToken);

                return Results.Ok();
            });
            app.MapControllers();

            app.Run();
        }
    }
}
