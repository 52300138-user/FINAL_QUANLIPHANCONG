USE WindowsFormsApp2_DB;
GO

-- User
IF OBJECT_ID('dbo.[User]') IS NULL
BEGIN
    CREATE TABLE dbo.[User](
        UserID   INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NULL,
        UserName NVARCHAR(50)  NOT NULL UNIQUE,
        PassWord NVARCHAR(256) NOT NULL,
        Email    NVARCHAR(200) NULL UNIQUE,
        SDT      NVARCHAR(20)  NULL UNIQUE,
        IsLocked BIT NOT NULL CONSTRAINT DF_User_IsLocked DEFAULT(0)
    );
END
ELSE
BEGIN
    IF COL_LENGTH('dbo.[User]','IsLocked') IS NULL
        ALTER TABLE dbo.[User] ADD IsLocked BIT NOT NULL CONSTRAINT DF_User_IsLocked DEFAULT(0) WITH VALUES;
END
GO

-- Roles
IF OBJECT_ID('dbo.Roles') IS NULL
BEGIN
    CREATE TABLE dbo.Roles(
        RoleId   INT IDENTITY(1,1) PRIMARY KEY,
        RoleCode NVARCHAR(50) NOT NULL UNIQUE,
        RoleName NVARCHAR(100) NULL
    );
END
GO

-- Permissions
IF OBJECT_ID('dbo.Permissions') IS NULL
BEGIN
    CREATE TABLE dbo.Permissions(
        PermId   INT IDENTITY(1,1) PRIMARY KEY,
        PermCode NVARCHAR(100) NOT NULL UNIQUE
    );
END
GO

-- RolePermissions
IF OBJECT_ID('dbo.RolePermissions') IS NULL
BEGIN
    CREATE TABLE dbo.RolePermissions(
        RoleId INT NOT NULL,
        PermId INT NOT NULL,
        CONSTRAINT PK_RolePermissions PRIMARY KEY(RoleId, PermId),
        CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY(RoleId) REFERENCES dbo.Roles(RoleId) ON DELETE CASCADE,
        CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY(PermId) REFERENCES dbo.Permissions(PermId) ON DELETE CASCADE
    );
END
GO

-- UserRoles
IF OBJECT_ID('dbo.UserRoles') IS NULL
BEGIN
    CREATE TABLE dbo.UserRoles(
        UserId INT NOT NULL,
        RoleId INT NOT NULL,
        CONSTRAINT PK_UserRoles PRIMARY KEY(UserId, RoleId),
        CONSTRAINT FK_UserRoles_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserID) ON DELETE CASCADE,
        CONSTRAINT FK_UserRoles_Roles FOREIGN KEY(RoleId) REFERENCES dbo.Roles(RoleId) ON DELETE CASCADE
    );
END
GO
