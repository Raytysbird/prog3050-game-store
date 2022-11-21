CREATE TABLE [dbo].[Wishlist]
(
    [wishlist_id] INT            NOT NULL,
    [user_id]     NVARCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([wishlist_id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [AspNetUsers](Id)
);

