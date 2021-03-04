------------------------- begin post deployment steps -------------------------------

if '$(IncludeImport)' = 1 
begin
	truncate table dbo.[Road]
	delete from dbo.[City]

	exec deployment.Import_Data
end

--exec deployment.CreateAuditTriggers

DECLARE @script_content VARCHAR(MAX);
SELECT @script_content = BulkColumn 
FROM OPENROWSET(BULK'$(ScriptPath)Train.publish.sql',SINGLE_CLOB) x;

INSERT INTO deployment.ScriptRunLog ([Script],[MachineName],[Edition],[SqlVersion]) 
VALUES 
(
	@script_content,
	CONVERT(VARCHAR, SERVERPROPERTY('MachineName')),
	CONVERT(VARCHAR, SERVERPROPERTY('Edition')),
	CONVERT(VARCHAR, SERVERPROPERTY('ProductVersion'))
)

---------------------- end post deployment steps ------------------------------------

