using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GutHealth.Models; 

public class GutHealthDbContext : IdentityDbContext<ApplicationUser>
{
    public GutHealthDbContext(DbContextOptions<GutHealthDbContext> options)
        : base(options)
    {
    }

    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<FoodCategory> FoodCategories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Fluent API configurations (if any)
        builder.Entity<FoodItem>()
            .Property(f => f.Price)
            .HasColumnType("decimal(18,2)");
        builder.Entity<FoodCategory>()
            .HasIndex(f => f.Name)
            .IsUnique();
        builder.Entity<Cart>()
            .HasIndex(c => c.UserId)
            .IsUnique();
        builder.Entity<Order>()
            .HasIndex(o => o.UserId);
        builder.Entity<OrderItem>()
            .Property(o => o.Price)
            .HasColumnType("decimal(18,2)");


        }
}
