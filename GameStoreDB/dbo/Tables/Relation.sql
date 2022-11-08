
CREATE TABLE [dbo].[Relation](
    [relation_id] [int] IDENTITY(1,1) NOT NULL,
    [from_user] [nvarchar](450) NOT NULL,
    [to_user] [nvarchar](450) NOT NULL,
    [areFriends] [bit] NULL,
    PRIMARY KEY CLUSTERED ([relation_id] ASC),
    CONSTRAINT [from_user] FOREIGN KEY ([from_user]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [to_user] FOREIGN KEY ([to_user]) REFERENCES [dbo].[AspNetUsers] ([Id])
);