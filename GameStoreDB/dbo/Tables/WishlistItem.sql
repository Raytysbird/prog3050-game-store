CREATE TABLE [dbo].[WishlistItem] 
(
Id INT IDENTITY (1, 1) NOT NULL,
wishlist_id int NOT NULL, 
game_id int NULL, 
merchandise_id int NULL, 
PRIMARY KEY (Id),
CONSTRAINT [FKGame 754] FOREIGN KEY (game_id) REFERENCES [dbo].[Game] ([game_id]),
CONSTRAINT [FKWishList 7868] FOREIGN KEY (wishlist_id) REFERENCES [dbo].[Wishlist] (wishlist_id),
CONSTRAINT [merch_fk] FOREIGN KEY (merchandise_id) REFERENCES [dbo].[Merchandise] (merchandise_id),
);

