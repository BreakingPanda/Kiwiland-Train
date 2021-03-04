CREATE TABLE [dbo].[NutrimetTicData]
(
	[NutrimetTicDataId] uniqueidentifier NOT NULL PRIMARY KEY DEFAULT (newid()),
	[Type]	char(1) NOT NULL DEFAULT 'P',
	[DownloadedDate] datetime2 NOT NULL DEFAULT (getdate()),
	[Content] nvarchar(max) NULL
)
