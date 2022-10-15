CREATE TABLE [dbo].[Credit Card Info] (
    [credit_card_id] INT            IDENTITY (1, 1) NOT NULL,
    [user_id]        NVARCHAR (450) NULL,
    [number]         NVARCHAR (450) NULL,
    [exp_date]       VARCHAR (255)  NULL,
    [ccc]            INT            NULL,
    PRIMARY KEY CLUSTERED ([credit_card_id] ASC),
    CONSTRAINT [FKCredit Car111073] FOREIGN KEY ([user_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
