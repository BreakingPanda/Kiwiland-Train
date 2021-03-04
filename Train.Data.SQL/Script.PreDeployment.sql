------------------------- begin post deployment steps -------------------------------

IF EXISTS (SELECT 1 FROM sys.databases WHERE [name]='Train')
BEGIN
	-- Should I make a backup here?  MAYBE :-)
	-- TODO: Maybe improve this piece to create differential backup
    DECLARE @var AS VARCHAR (MAX);
    SELECT @var = CONVERT(VARCHAR, GETUTCDATE(), 23) + '-' + REPLACE(CONVERT(VARCHAR, GETUTCDATE(), 8), ':', '-')
	SET @var = 'BACKUP DATABASE [Train] TO DISK = ''' + '$(BackupPath)' + '\Train-' + @var + '.BAK''';
	EXEC (@var);
END


---------------------- end post deployment steps ------------------------------------
