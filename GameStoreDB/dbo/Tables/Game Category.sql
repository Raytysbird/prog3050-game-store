CREATE TABLE [dbo].[Game Category] (
    [category_id] INT NOT NULL,
    [game_id]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([category_id] ASC, [game_id] ASC),
    CONSTRAINT [FKGame Categ302463] FOREIGN KEY ([game_id]) REFERENCES [dbo].[Game] ([game_id]),
    CONSTRAINT [FKGame Categ52072] FOREIGN KEY ([category_id]) REFERENCES [dbo].[Category] ([category_id])
);

