using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserDbContext: DbContext
    {
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { 

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("UserDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.UserEmail)
                      .IsRequired()                      
                      .HasMaxLength(100);

                // enforce uniqueness on email
                entity.HasIndex(e => e.UserEmail)
                      .IsUnique();

                entity.Property(e => e.PhoneNumber)
                      .HasMaxLength(20);

                entity.Property(e => e.DOB)
                      .HasColumnType("datetimeoffset");

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("GETDATE()");
            });
        }

    }
}
