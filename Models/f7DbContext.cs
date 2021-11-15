using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace f7.Models
{
    public class f7DbContext : IdentityDbContext<f7AppUser>
    {
        public f7DbContext(DbContextOptions<f7DbContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>()
                .HasKey(o => new { o.ItemId, o.OrderId })
                .HasName("2keys_contraint_Order_Item");

            builder.Entity<Staff>()
                .Property(s => s.StaffName)
                .IsRequired();

            builder.Entity(typeof(Staff))
                .Property("Gender")
                .IsRequired();

            builder.Entity<Warehouse>()
                .Property(w => w.WarehouseId)
                .ValueGeneratedOnAdd();

            builder.Entity<f7AppUser>(entity =>
            {
                entity
                    .HasIndex(user => user.UserName, "Unique_UserName_Constraint")
                    .IsUnique();
            });
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Warehouse> warehouse { get; set; }

    }
}
