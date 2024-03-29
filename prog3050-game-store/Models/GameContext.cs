﻿using System;
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

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartGame> CartGame { get; set; }
        public virtual DbSet<CartMerchandise> CartMerchandise { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CreditCardInfo> CreditCardInfo { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<FavouriteCategory> FavouriteCategory { get; set; }
        public virtual DbSet<FavouritePlatform> FavouritePlatform { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameCategory> GameCategory { get; set; }
        public virtual DbSet<GamePlatform> GamePlatform { get; set; }
        public virtual DbSet<Merchandise> Merchandise { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Relation> Relation { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<UserEvent> UserEvent { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<WishlistItem> WishlistItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=KANGPC\\SQLEXPRESS19;Database=GameStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.AptNumber)
                    .HasColumnName("apt_number")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Building)
                    .HasColumnName("building")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsShipping).HasColumnName("isShipping");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("street_address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNumber)
                    .HasColumnName("unit_number")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AddressNavigation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_id");
            });

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

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CreditCardId).HasColumnName("credit_card_id");

                entity.Property(e => e.StateOfOrder)
                    .HasColumnName("state_of_order")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCost).HasColumnName("total_cost");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.CreditCard)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.CreditCardId)
                    .HasConstraintName("FKCredit 786863");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser 75428");
            });

            modelBuilder.Entity<CartGame>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.GameId });

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartGame)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCart 754");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.CartGame)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame 7868");
            });

            modelBuilder.Entity<CartMerchandise>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.MerchandiseId });

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartMerchandise)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCart 75498");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.CartMerchandise)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMerchandise 7868");
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

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CreditCardInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FKCredit Car111073");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");
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

                entity.Property(e => e.ImagePath)
                    .HasColumnName("imagePath")
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

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Merchandise)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__Merchandi__game___1209AD79");
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

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.ToTable("relation");

                entity.Property(e => e.RelationId).HasColumnName("relation_id");

                entity.Property(e => e.AreFriends).HasColumnName("areFriends");

                entity.Property(e => e.FromUser)
                    .IsRequired()
                    .HasColumnName("from_user")
                    .HasMaxLength(450);

                entity.Property(e => e.ToUser)
                    .IsRequired()
                    .HasColumnName("to_user")
                    .HasMaxLength(450);

                entity.HasOne(d => d.FromUserNavigation)
                    .WithMany(p => p.RelationFromUserNavigation)
                    .HasForeignKey(d => d.FromUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("from_user");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.RelationToUserNavigation)
                    .HasForeignKey(d => d.ToUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("to_user");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.AspUserId)
                    .IsRequired()
                    .HasColumnName("asp_user_id")
                    .HasMaxLength(450);

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Review1)
                    .HasColumnName("review")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AspUser)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.AspUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__asp_user__3D2915A8");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__Review__game_id__3E1D39E1");
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.Property(e => e.AspUserId)
                    .IsRequired()
                    .HasColumnName("asp_user_id")
                    .HasMaxLength(450);

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.HasOne(d => d.AspUser)
                    .WithMany(p => p.UserEvent)
                    .HasForeignKey(d => d.AspUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserEvent__asp_u__690797E6");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.UserEvent)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserEvent__event__69FBBC1F");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wishlist__user_i__00DF2177");
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FKGame 754");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.MerchandiseId)
                    .HasConstraintName("merch_fk");

                entity.HasOne(d => d.Wishlist)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.WishlistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWishList 7868");
            });
        }
    }
}
