CREATE TABLE [dbo].[Province]
(
	[provinceCode] [char](2) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[countryCode] [char](2) NOT NULL,
	[firstPostalLetter] [varchar](50) NULL,
 CONSTRAINT [PK_province] PRIMARY KEY CLUSTERED ([provinceCode] ASC),
 CONSTRAINT [countryCode] FOREIGN KEY ([countryCode]) REFERENCES [dbo].[Country] ([countryCode])
)