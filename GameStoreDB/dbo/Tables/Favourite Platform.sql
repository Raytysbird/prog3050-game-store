CREATE TABLE [dbo].[Favourite Platform] (
    [platform_id] INT            NOT NULL,
    [user_id]     NVARCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([platform_id] ASC, [user_id] ASC),
    CONSTRAINT [FKFavourite 872463] FOREIGN KEY ([platform_id]) REFERENCES [dbo].[Platform] ([platforrm_id]),
    CONSTRAINT [FKFavourite 99313] FOREIGN KEY ([user_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

