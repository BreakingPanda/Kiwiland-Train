CREATE TABLE [dbo].[Road]
(
	[Id] int NOT NULL Identity,
	[From] int NOT NULL FOREIGN KEY REFERENCES City(Id),
	[To] int NOT NULL FOREIGN KEY REFERENCES City(Id),
	[Distance] int NOT NULL DEFAULT 0,
	[CreatedDate] datetime2(7) NOT NULL DEFAULT (GETUTCDATE()),
	[ModifiedDate] datetime2(7) NULL	
)
GO

CREATE UNIQUE INDEX IX_Road_Composite ON dbo.Road ([From] ASC, [To] ASC)

