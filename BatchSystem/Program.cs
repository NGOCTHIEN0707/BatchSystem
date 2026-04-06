
using BatchSystem.Domain.Alarms;
using BatchSystem.Domain.Lines;
using BatchSystem.Domain.Logins;
using BatchSystem.Domain.Materials;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using BatchSystem.Domain.SeedWork;
using BatchSystem.Domain.Stations;
using BatchSystem.Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<IAlarmDefinitionRepository, AlarmDefinitionRepository>();
            builder.Services.AddScoped<IAlarmEventRepository, AlarmEventRepository>();
            builder.Services.AddScoped<ILineRepository, LineRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<IOrderBatchRepository, OrderBatchRepository>();
            builder.Services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IStationRepository, StationRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                //cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
                cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
                cfg.RegisterServicesFromAssemblyContaining<Entity>();
                //cfg.RegisterServicesFromAssemblyContaining<Buffer>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
