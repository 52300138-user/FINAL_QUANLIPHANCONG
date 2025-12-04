IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PermissionAudit')
BEGIN
    CREATE TABLE dbo.PermissionAudit
    (
        AuditId     INT IDENTITY(1,1) PRIMARY KEY,
        EventTime   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
        UserId      INT           NULL,
        UserName    NVARCHAR(100) NULL,
        ActionCode  NVARCHAR(100) NOT NULL,  -- User.Create / User.Update / User.Lock / User.ResetPw / Role.Assign / Report.View...
        TargetInfo  NVARCHAR(4000) NULL,
        Result      NVARCHAR(20)  NOT NULL   -- SUCCESS / DENIED / ERROR
    );
END
