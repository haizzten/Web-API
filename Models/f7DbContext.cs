using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace f7.Models
{
    public class f7DbContext : IdentityDbContext<f7AppUser>
    {
        public static ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });
        public f7DbContext(DbContextOptions<f7DbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseLoggerFactory(loggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetailModels>()
                .HasKey(o => new { o.ItemId, o.OrderId })
                .HasName("2keys_contraint_Order_Item");

            builder.Entity<StaffModels>()
                .Property(s => s.Name)
                .IsRequired();

            builder.Entity(typeof(StaffModels))
                .Property("Gender")
                .IsRequired();

            builder.Entity<WarehouseModels>()
                .Property(w => w.WarehouseId)
                .ValueGeneratedOnAdd();

            builder.Entity<f7AppUser>(entity =>
            {
                entity.HasIndex(user => user.UserName, "Unique_UserName_Constraint")
                    .IsUnique();
            });

            builder.Entity<BatchModels>(wr =>
            {
                wr.HasOne<WarehouseReceiptModels>(wrd => wrd.WarehouseReceipt)
                  .WithMany();
            });
            builder.Entity<BatchModels>(b =>
            {
                b.HasKey(b => b.ID);
                b.HasIndex(b => new { b.BatchId, b.WarehouseReceiptId })
                    .IsUnique();
                b.HasOne<ItemModels>(b => b.Item)
                    .WithMany(i => i.Batches);
            });

            builder.Entity<StockCardModels>(sc =>
            {
                sc.HasKey(sc => sc.ID);
                sc.HasIndex(sc => new { sc.ItemId, sc.DateTime })
                    .IsUnique();
                sc.HasOne<ItemModels>(sc => sc.Item)
                    .WithMany();
                sc.HasOne<BatchModels>(sc => sc.Batch)
                    .WithMany();
            });

            builder.Entity<GoodIssueNoteModels>(e =>
            {
                e.HasOne<StaffModels>(e => e.Staff)
                    .WithMany()
                    .OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<PurchaseOrderDetailModels>(pod =>
            {
                pod.HasOne<PurchaseOrderModels>(pod => pod.PurchaseOrder)
                    .WithMany()
                    .OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<WarehouseReceiptModels>(po =>
            {
                po.HasOne<PurchaseOrderModels>(po => po.PurchaseOrder)
                    .WithOne()
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // builder.Entity<WarehouseReceiptModels>(wr =>
            // {
            //     wr.HasOne<WarehouseModels>(po => po.Warehouse)
            //         .WithMany()
            //         .OnDelete(DeleteBehavior.SetNull);
            // });

            builder.Entity<PurchaseOrderModels>(po =>
            {
                po.HasOne<StaffModels>(po => po.Staff)
                    .WithOne()
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

        public DbSet<CustomerModels> customers { get; set; }
        public DbSet<OrderModels> orders { get; set; }
        public DbSet<OrderDetailModels> orderDetail { get; set; }
        public DbSet<ItemModels> items { get; set; }
        public DbSet<StaffModels> staffs { get; set; }
        public DbSet<WarehouseModels> warehouses { get; set; }
        public DbSet<WarehouseReceiptModels> warehouseReceipts { get; set; }
        public DbSet<BatchModels> batches { get; set; }
        public DbSet<ProviderModels> providers { get; set; }
        public DbSet<StockCardModels> stockCards { get; set; }
        public DbSet<PurchaseOrderModels> purchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetailModels> purchaseOrdersDetail { get; set; }
    }
}
