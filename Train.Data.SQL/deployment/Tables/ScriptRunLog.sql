CREATE TABLE [deployment].[ScriptRunLog] 
(
    [Id] INT NOT NULL Identity,
    [RunDate] DATETIME2 (7) DEFAULT (GETUTCDATE()) NOT NULL,
    [Script] VARCHAR(MAX) NOT NULL,
	[MachineName] VARCHAR(200) NULL,
	[Edition] VARCHAR(200) NULL,
	[SqlVersion] VARCHAR(200) NULL,
    
	CONSTRAINT [PK_ScriptRunLog] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
