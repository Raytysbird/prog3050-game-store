CREATE TABLE [dbo].[CartMerchandise] 
(
cart_id int IDENTITY NOT NULL,
merchandise_id int NOT NULL,

PRIMARY KEY CLUSTERED (cart_id ASC, merchandise_id ASC),
    CONSTRAINT [FKCart 75498] FOREIGN KEY (cart_id) REFERENCES [dbo].[Cart] ([cart_id]),
    CONSTRAINT [FKMerchandise 7868] FOREIGN KEY (merchandise_id) REFERENCES [dbo].[Merchandise] (merchandise_id)
);