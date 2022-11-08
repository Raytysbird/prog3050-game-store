CREATE TABLE [dbo].[Address](
    [address_id] [int] IDENTITY(1,1) NOT NULL,
    [street_address] [varchar](255) NULL,
    [apt_number] [varchar](255) NULL,
    [unit_number] [varchar](255) NULL,
    [building] [varchar](255) NULL,
    [isShipping] [bit] NULL,
    [user_id] [nvarchar](450) NOT NULL,
    [city] [varchar](255) NULL,
    [postal_code] [varchar](255) NULL,
    [province] [varchar](255) NULL,
    PRIMARY KEY CLUSTERED ([address_id] ASC),
    CONSTRAINT [user_id] FOREIGN KEY ([user_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);