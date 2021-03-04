CREATE TABLE [audit].[Audit]
(
	[AuditId]		INT				NOT NULL IDENTITY(1,1) CONSTRAINT PK_Audit PRIMARY KEY,
	[AuditDate]		DATETIME2(2)	NOT NULL CONSTRAINT DF_Audit_AuditDate DEFAULT GETUTCDATE(),
	[TableName]		VARCHAR(255)	NOT NULL,
	[Before]		XML				NULL,
	[After]			XML				NULL,
)
GO
CREATE NONCLUSTERED INDEX [IX_Audit_AuditDate] ON [audit].[Audit]
(
	[AuditDate] 
)