using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace f7.Models
{
    public class f7DbContext : IdentityDbContext<f7AppUser>
    {
        public f7DbContext(DbContextOptions<f7DbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetailModels>()
                .HasKey(o => new { o.ItemId, o.OrderId })
                .HasName("2keys_contraint_Order_Item");

            builder.Entity<StaffModels>()
                .Property(s => s.StaffName)
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

            builder.Entity<ItemDetailModels>(id =>
            {
                id.HasKey(id => new { id.ConsignmentId, id.ItemId });
                id.HasOne<ItemModels>(id => id.ItemModels)
                  .WithMany();
            });
        }

        public DbSet<CustomerModels> customers { get; set; }
        public DbSet<OrderModels> orders { get; set; }
        public DbSet<OrderDetailModels> orderDetail { get; set; }
        public DbSet<ItemModels> items { get; set; }
        public DbSet<ItemDetailModels> itemDetails { get; set; }
        public DbSet<StaffModels> staffs { get; set; }
        public DbSet<WarehouseModels> warehouse { get; set; }

    }
}
