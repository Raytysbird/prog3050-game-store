CREATE TABLE [dbo].[Category] (
    [category_id] INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([category_id] ASC)
);

