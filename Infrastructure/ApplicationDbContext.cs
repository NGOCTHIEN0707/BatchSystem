using Domain.Alarms;
using Domain.Lines;
using Domain.Logins;
using Domain.Materials;
using Domain.OrderBatchs;
using Domain.OrderBatchs.BatchWeightResults;
using Domain.ProductionOrders;
using Domain.Products;
using Domain.Recipes;
using Domain.Stations;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<OrderBatch> OrderBatchs { get; set; }
        public DbSet<BatchWeighingResult> BatchWeighingResults { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<LineCurrentStatus> LineCurrentStatuses { get; set; }
        public DbSet<LineStatusHistory> LineStatusHistories { get; set; }
        public DbSet<OrderBatchStep> OrderBatchSteps { get; set; }
        public DbSet<ProductionOrderDetail> ProductionOrderDetails { get; set; }
        public DbSet<RecipeMaterial> RecipeMaterials { get; set; }
        public DbSet<StationCurrentStatus> StationCurrentStatuses { get; set; }
        public DbSet<StationType> StationTypes { get; set; }
        public DbSet<AlarmDefinition> AlarmDefinitions { get; set; }
        public DbSet<AlarmEvent> AlarmEvents { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StationTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StationCurrentStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LineCurrentStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LineStatusHistoryEntityTypeConfiguration());

            // Cấu hình cho đơn hàng và mẻ (Orders & Batches)
            modelBuilder.ApplyConfiguration(new ProductionOrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionOrderDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderBatchEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderBatchStepEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BatchWeighingResultEntityTypeConfiguration());

            // Cấu hình cho danh mục (Master Data)
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeMaterialEntityTypeConfiguration());

            // Cấu hình cho cảnh báo (Alarms)
            modelBuilder.ApplyConfiguration(new AlarmDefinitionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlarmEventEntityTypeConfiguration());

            // Cấu hình cho bảo mật (Security)
            modelBuilder.ApplyConfiguration(new LoginEntityTypeConfiguration());

        }
    }
}
