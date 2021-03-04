CREATE PROCEDURE deployment.CreateAuditTriggers
AS
BEGIN

BEGIN TRANSACTION


DECLARE @whitelist TABLE 
(
	[SchemaName.TableName] NVARCHAR(255)
)
	
--Insert below tables that should not have audit trigger created on it.
INSERT @whitelist 
VALUES  ('audit.Audit'),
		('deployment.ScriptRunLog')


DECLARE @sql AS TABLE ([Seq] int identity(1,1), [query] NVARCHAR(MAX))
INSERT @sql (query)
SELECT  'CREATE TRIGGER ['+TABLE_SCHEMA+'].[Trigger_'+TABLE_NAME+'_Audit]
ON ['+TABLE_SCHEMA+'].['+TABLE_NAME+'] 
AFTER DELETE, INSERT, UPDATE 
AS
BEGIN
	SET NOCOUNT ON

	IF (NOT EXISTS(SELECT 1 FROM INSERTED) AND NOT EXISTS (SELECT 1 FROM DELETED)) 
	RETURN

	DECLARE @before XML  = (SELECT * FROM DELETED FOR XML AUTO)
	DECLARE @after XML = (SELECT * FROM INSERTED FOR XML AUTO)

	-- Insert the changes into the Log table
	INSERT INTO audit.Audit
	(
			[TableName], 
			[Before], 
			[After]
	)
	VALUES 
	(
	'
			+''''+TABLE_SCHEMA +'.'+ TABLE_NAME + ''',
			@before, 
			@after
	)
END
'
FROM [INFORMATION_SCHEMA].[TABLES] trgt
WHERE TABLE_TYPE = 'BASE TABLE'
AND NOT LEFT(TABLE_NAME, 2) = '__'
AND TABLE_SCHEMA NOT IN ('audit', 'data')
AND NOT EXISTS (SELECT 1
				FROM sys.objects src
				WHERE [type] = 'TR' --Only Triggers 
				AND SCHEMA_NAME(src.[schema_id]) = trgt.TABLE_SCHEMA 
				AND src.[name] = 'Trigger_'+TABLE_NAME+'_Audit' 
				)
AND NOT EXISTS (SELECT 1
				FROM @whitelist
				WHERE [TABLE_SCHEMA] + '.' + [TABLE_NAME] = [SchemaName.TableName]
				)

				
DECLARE @querySeq NVARCHAR(MAX)
DECLARE @min INT = (SELECT Min([Seq]) FROM @sql)

WHILE @min <= (SELECT Max([Seq]) FROM @sql)
BEGIN 
		
	SELECT @querySeq = [query] FROM @sql WHERE [Seq] = @min
	EXEC(@querySeq); 
			
	SET @min =  @min + 1

END

COMMIT
END