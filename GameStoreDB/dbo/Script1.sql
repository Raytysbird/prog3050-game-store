
--GO
--INSERT [dbo].[Country] ([countryCode], [name], [postalPattern], [phonePattern]) VALUES (N'CA', N'Canada', N'[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d', N'\d{3}-\d{3}-\d{4}')
--GO
--INSERT [dbo].[Country] ([countryCode], [name], [postalPattern], [phonePattern]) VALUES (N'US', N'United States Of America', N'\d{5}(-\d{4})?', N'\(\d{3}\) \d{3}-\d{4}')



GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'AB', N'Alberta', N'CA', N'T')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'AK', N'Alaska', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'AR', N'Arkansas', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'AZ', N'Arizona', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'BC', N'British Columbia', N'CA', N'V')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'CA', N'California', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'CO', N'Colorado', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'CT', N'Connecticut', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'DC', N'District of Columbia', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'DE', N'Delaware', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'FL', N'Forida', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'GA', N'Georgia', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'HI', N'Hawaii', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'IA', N'Iowa', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'ID', N'Idaho', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'IL', N'Illinois', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'IN', N'Indiana', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'KA', N'Kansas', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'KY', N'Kentucky', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'LA', N'Louisiana', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MA', N'Massachusetts', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MB', N'Manitoba', N'CA', N'R')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MD', N'MaryLand', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'ME', N'Maine', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MI', N'Michigan', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MN', N'Minnesota', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MO', N'Missouri', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MS', N'Mississippi', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'MT', N'Montana', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NB', N'New Brunswick                 ', N'CA', N'E')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NC', N'North Carolina', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'ND', N'North Dakota', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NE', N'Nebraska', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NH', N'New Hampshire', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NJ', N'New Jersey', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NL', N'Newfoundland and Labrador', N'CA', N'A')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NM', N'New Mexico', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NS', N'Nova Scotia', N'CA', N'B')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NT', N'Northwest Territories', N'CA', N'X')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NU', N'Nunavut', N'CA', N'X')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NV', N'Nevada', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'NY', N'New York', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'OH', N'Ohio', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'OK', N'Oklahoma', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'ON', N'Ontario', N'CA', N'KLMNP')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'OR', N'Oregon', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'PA', N'Pennsylvania', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'PI', N'Prince Edward Island', N'CA', N'C')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'PR', N'Puerto Rico', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'QC', N'Quebec', N'CA', N'GHJ')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'RI', N'Rhode Island', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'SC', N'South Carolina', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'SD', N'South Dakota', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'SK', N'Saskatchewan', N'CA', N'S')
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'TN', N'Tennessee', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'TX', N'Texas', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'UT', N'Utah', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'VA', N'Virginia', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'VT', N'Vermont', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'WA', N'Washington', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'WI', N'Wisconsin', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'WV', N'West Virginia', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'WY', N'Wyoming', N'US', NULL)
GO
INSERT [dbo].[Province] ([provinceCode], [name], [countryCode], [firstPostalLetter]) VALUES (N'YT', N'Yukon', N'CA', N'Y')
GO
