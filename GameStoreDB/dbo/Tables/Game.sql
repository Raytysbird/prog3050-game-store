CREATE TABLE [dbo].[Game] (
    [game_id]     INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (255) NOT NULL,
    [description] VARCHAR (255) NULL,
    [price]       REAL          NOT NULL,
    [imagePath]   VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([game_id] ASC)
);

