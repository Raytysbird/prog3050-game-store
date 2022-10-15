using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameStore.Models
{
    public partial class GameContext : DbContext
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CreditCardInfo> CreditCardInfo { get; set; }
        public virtual DbSet<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual DbSet<FavouritePlatform> FavouritePlatform { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameCategory> GameCategory { get; set; }
        public virtual DbSet<GamePlatform> GamePlatform { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Province> Province { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS19;Database=Game;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReceivePromotions).HasColumnName("receive_promotions");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryCode);

                entity.Property(e => e.CountryCode)
                    .HasColumnName("countryCode")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhonePattern)
                    .HasColumnName("phonePattern")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalPattern)
                    .HasColumnName("postalPattern")
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditCardInfo>(entity =>
            {
                entity.HasKey(e => e.CreditCardId);

                entity.ToTable("Credit Card Info");

                entity.Property(e => e.CreditCardId).HasColumnName("credit_card_id");

                entity.Property(e => e.Ccc).HasColumnName("ccc");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CreditCardInfo)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCredit Car111073");
            });

            modelBuilder.Entity<FavouriteCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.UserId });

                entity.ToTable("Favourite Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFavourite 786863");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouriteCategory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFavourite 75428");
            });

            modelBuilder.Entity<FavouritePlatform>(entity =>
            {
                entity.HasKey(e => new { e.PlatformId, e.UserId });

                entity.ToTable("Favourite Platform");

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.FavouritePlatform)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFavourite 872463");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouritePlatform)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFavourite 99313");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<GameCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.GameId });

                entity.ToTable("Game Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.GameCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame Categ52072");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameCategory)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame Categ302463");
            });

            modelBuilder.Entity<GamePlatform>(entity =>
            {
                entity.HasKey(e => new { e.PlatformId, e.GameId });

                entity.ToTable("Game Platform");

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamePlatform)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame Platf494386");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.GamePlatform)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame Platf260191");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasKey(e => e.PlatforrmId);

                entity.Property(e => e.PlatforrmId).HasColumnName("platforrm_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvinceCode);

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("provinceCode")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("countryCode")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FirstPostalLetter)
                    .HasColumnName("firstPostalLetter")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryCode");
            });
        }
    }
}
