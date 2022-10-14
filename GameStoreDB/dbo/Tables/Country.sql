CREATE TABLE [dbo].[Country]
(
	[countryCode] [char](2) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[postalPattern] [varchar](120) NULL,
	[phonePattern] [varchar](50) NULL,
 CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED  ([countryCode] ASC)
)
