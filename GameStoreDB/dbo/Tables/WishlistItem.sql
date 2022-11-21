CREATE TABLE [dbo].[WishlistItem] 
(
wishlist_id int NOT NULL, 
game_id int NOT NULL, 

PRIMARY KEY CLUSTERED (wishlist_id ASC, game_id ASC),
    CONSTRAINT [FKGame 754] FOREIGN KEY (game_id) REFERENCES [dbo].[Game] ([game_id]),
    CONSTRAINT [FKWishList 7868] FOREIGN KEY (wishlist_id) REFERENCES [dbo].[Wishlist] (wishlist_id)
);

