CREATE TABLE [dbo].[Game Platform] (
    [platform_id] INT NOT NULL,
    [game_id]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([platform_id] ASC, [game_id] ASC),
    CONSTRAINT [FKGame Platf260191] FOREIGN KEY ([platform_id]) REFERENCES [dbo].[Platform] ([platforrm_id]),
    CONSTRAINT [FKGame Platf494386] FOREIGN KEY ([game_id]) REFERENCES [dbo].[Game] ([game_id])
);

