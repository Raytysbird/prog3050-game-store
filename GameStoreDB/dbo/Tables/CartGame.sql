CREATE TABLE [dbo].[CartGame] 
(
cart_id int IDENTITY NOT NULL,
game_id int NOT NULL,

PRIMARY KEY CLUSTERED (cart_id ASC, game_id ASC),
    CONSTRAINT [FKCart 754] FOREIGN KEY (cart_id) REFERENCES [dbo].[Cart] ([cart_id]),
    CONSTRAINT [FKGame 7868] FOREIGN KEY (game_id) REFERENCES [dbo].[Game] ([game_id])
);