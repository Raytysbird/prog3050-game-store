﻿using System;
using System.Collections.Generic;

namespace GameStore.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AddressNavigation = new HashSet<Address>();
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Cart = new HashSet<Cart>();
            CreditCardInfo = new HashSet<CreditCardInfo>();
            FavouriteCategory = new HashSet<FavouriteCategory>();
            FavouritePlatform = new HashSet<FavouritePlatform>();
            RelationFromUserNavigation = new HashSet<Relation>();
            RelationToUserNavigation = new HashSet<Relation>();
            Review = new HashSet<Review>();
            UserEvent = new HashSet<UserEvent>();
            Wishlist = new HashSet<Wishlist>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool? ReceivePromotions { get; set; }

        public ICollection<Address> AddressNavigation { get; set; }
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<CreditCardInfo> CreditCardInfo { get; set; }
        public ICollection<FavouriteCategory> FavouriteCategory { get; set; }
        public ICollection<FavouritePlatform> FavouritePlatform { get; set; }
        public ICollection<Relation> RelationFromUserNavigation { get; set; }
        public ICollection<Relation> RelationToUserNavigation { get; set; }
        public ICollection<Review> Review { get; set; }
        public ICollection<UserEvent> UserEvent { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
