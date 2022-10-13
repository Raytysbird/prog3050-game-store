CREATE TABLE [dbo].[Favourite Category] (
    [category_id] INT            NOT NULL,
    [user_id]     NVARCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([category_id] ASC, [user_id] ASC),
    CONSTRAINT [FKFavourite 75428] FOREIGN KEY ([user_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FKFavourite 786863] FOREIGN KEY ([category_id]) REFERENCES [dbo].[Category] ([category_id])
);

