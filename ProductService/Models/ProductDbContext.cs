using Microsoft.EntityFrameworkCore;

namespace ProductService.Models
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext()
        {
        }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("ProductDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Description)
                      .HasMaxLength(1000);

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.CategoryId)
                      .IsRequired();

                // index to speed lookups by category
                entity.HasIndex(e => e.CategoryId);

                entity.Property(e => e.IsAvailable)
                      .HasDefaultValue(true);

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetimeoffset")
                      .HasDefaultValueSql("SYSDATETIMEOFFSET()");
            });
        }


    }
}
