CREATE TABLE [dbo].[City]
(
	[Id] int NOT NULL IDENTITY ,
	[Name] varchar(25) NOT NULL,
	[CreatedDate] datetime2(7) NOT NULL DEFAULT (GETUTCDATE()),
	[ModifiedDate] datetime2(7) NULL,

	CONSTRAINT [PK_City] PRIMARY KEY NONCLUSTERED 	([Id] ASC)
)
