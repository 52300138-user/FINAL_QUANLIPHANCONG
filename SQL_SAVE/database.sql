USE master;
GO

DECLARE @DBName sysname = N'QuanLiPhanCong';

IF DB_ID(@DBName) IS NOT NULL
BEGIN
    PRINT N'Đang ngắt kết nối và xóa Database cũ: ' + @DBName + N'...';

    -- Ép đóng tất cả connection đang dùng DB đó
    ALTER DATABASE QuanLiPhanCong 
        SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

    -- Xóa hẳn DB
    DROP DATABASE QuanLiPhanCong;
END
GO

-- Tạo Database mới
PRINT N'Đang tạo Database mới...';
CREATE DATABASE QuanLiPhanCong;
GO

-- Chuyển ngữ cảnh sang DB mới
USE QuanLiPhanCong;
GO

-- DROP
IF OBJECT_ID('dbo.PhanCong', 'U') IS NOT NULL DROP TABLE dbo.PhanCong;
IF OBJECT_ID('dbo.CongViec', 'U') IS NOT NULL DROP TABLE dbo.CongViec;
IF OBJECT_ID('dbo.MonHoc', 'U') IS NOT NULL DROP TABLE dbo.MonHoc;
IF OBJECT_ID('dbo.KeHoach', 'U') IS NOT NULL DROP TABLE dbo.KeHoach;
IF OBJECT_ID('dbo.[User]', 'U') IS NOT NULL DROP TABLE dbo.[User];
GO


-- BẢNG 0: USER (NguoiDung)
PRINT 'Creating Table [User]...'
CREATE TABLE dbo.[User] (
    UserID      INT PRIMARY KEY IDENTITY(1,1),
    FullName    NVARCHAR(200) NOT NULL,
    UserName    NVARCHAR(50)  NOT NULL UNIQUE, 
    PassWord    NVARCHAR(128) NOT NULL, -- Cần phải lưu Hash (SHA256)
    Email       NVARCHAR(100) NOT NULL UNIQUE,
    Address     NVARCHAR(255) NULL,
    Gender      NVARCHAR(10)  NOT NULL CHECK (Gender IN (N'Nam', N'Nữ', N'Khác')),
    Role        NVARCHAR(10)  NOT NULL CHECK (Role IN (N'TBM', N'GV')),
    CreatedAt   DATETIME2(0)  NOT NULL DEFAULT SYSDATETIME(),
    SDT         NVARCHAR(20)  NULL UNIQUE,
    IsLocked    BIT           NOT NULL DEFAULT 0
);
GO

-- BẢNG 1: KEHOACH
PRINT 'Creating Table KeHoach...'
CREATE TABLE dbo.KeHoach (
    KeHoachID    INT IDENTITY(1,1) PRIMARY KEY,
    TenKeHoach   NVARCHAR(200) NOT NULL,
    Loai         NVARCHAR(20)  NOT NULL CHECK (Loai IN (N'HocKyI', N'HocKyII',N'HocKyHe', N'NamHoc', N'Khac',N'SuKien',N'DeTai')),
    NgayBatDau   DATE NOT NULL,
    NgayKetThuc  DATE NOT NULL,
    NguoiTaoID   INT  NOT NULL, 
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    
    CONSTRAINT FK_KH_User FOREIGN KEY (NguoiTaoID) REFERENCES dbo.[User](UserID)
);
GO

-- BẢNG 2: MONHOC (Đã thêm TenNhom, TenTo theo yêu cầu)
PRINT 'Creating Table MonHoc...'
CREATE TABLE dbo.MonHoc(
    MonHocID    INT IDENTITY(1,1) PRIMARY KEY,
    MaMonHoc    NVARCHAR(20) NOT NULL,
    TenMonHoc   NVARCHAR(200) NOT NULL,
    TenNhom     NVARCHAR(10) NOT NULL,  
    TenTo       NVARCHAR(10) NULL,     
    SoTinChi    INT NOT NULL,
	HocKy		NVARCHAR(50) NOT NULL,
    SoTiet_LT   INT NOT NULL DEFAULT 0,
    SoTiet_TH   INT NOT NULL DEFAULT 0
);
GO

-- BẢNG 3: CONGVIEC
PRINT 'Creating Table CongViec...'
CREATE TABLE dbo.CongViec (
    CongViecID  INT IDENTITY(1,1) PRIMARY KEY,
    KeHoachID   INT NULL,
    
    TenCongViec NVARCHAR(200) NOT NULL,
    MoTa        NVARCHAR(MAX) NULL,     -- Ghi chú chung của TBM
    
    Loai        NVARCHAR(20) NOT NULL 
                CHECK (Loai IN (N'GiangDay', N'NghienCuu', N'SuKien', N'HanhChinh')), 

    HanChot      DATE NULL,
    MucUuTien   NVARCHAR(10) NOT NULL DEFAULT N'MED' CHECK (MucUuTien IN (N'LOW', N'MED', N'HIGH')),
    TrangThai   NVARCHAR(20) NOT NULL DEFAULT N'MOI' CHECK (TrangThai IN (N'MOI', N'DANG_LAM', N'HOAN_THANH', N'QUA_HAN')),
    
    NguoiGiaoID INT NOT NULL, 
    CreatedAt   DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(), 

    -- Chi tiết
    MonHocID    INT NULL,       
    LopPhuTrach NVARCHAR(100) NULL, 
    SoTiet      INT NULL,       
    MaDeTai     NVARCHAR(50) NULL, 
    DiaDiem     NVARCHAR(200) NULL, 
    
    CONSTRAINT FK_CV_MonHoc FOREIGN KEY (MonHocID) REFERENCES dbo.MonHoc(MonHocID),
    CONSTRAINT FK_CV_KH     FOREIGN KEY (KeHoachID) REFERENCES dbo.KeHoach(KeHoachID) ON DELETE SET NULL, 
    CONSTRAINT FK_CV_User   FOREIGN KEY (NguoiGiaoID) REFERENCES dbo.[User](UserID)
);
GO

-- BẢNG 4: PHANCONG
PRINT 'Creating Table PhanCong...'
CREATE TABLE dbo.PhanCong (
    PC_ID               INT IDENTITY(1,1) PRIMARY KEY,
    CongViecID          INT NOT NULL,
    UserID              INT NOT NULL, -- GV được gán
    
    -- Phần TBM giao
    VaiTro              NVARCHAR(20) NOT NULL DEFAULT N'HoTro' CHECK (VaiTro IN (N'ChuTri', N'HoTro')),
    NgayGiao            DATE NULL,
    GhiChu_TBM          NVARCHAR(255) NULL, 

    -- Phần Báo cáo ngược của GV
    TrangThaiGV         NVARCHAR(20) NOT NULL DEFAULT N'MOI' 
                        CHECK (TrangThaiGV IN (N'MOI', N'DANG_LAM', N'CHO_DUYET', N'HOAN_THANH', N'TU_CHOI')),
    PhanTram            INT NOT NULL DEFAULT 0 CHECK (PhanTram BETWEEN 0 AND 100),
    GhiChu_GV           NVARCHAR(MAX) NULL, 
    
    ThoiDiemCapNhatCuoi DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),

    -- Ràng buộc (Foreign Keys & Uniqueness)
    CONSTRAINT UQ_PhanCong UNIQUE (CongViecID, UserID), 
    CONSTRAINT FK_PC_CV FOREIGN KEY (CongViecID) REFERENCES dbo.CongViec(CongViecID) ON DELETE CASCADE, 
    CONSTRAINT FK_PC_User FOREIGN KEY (UserID) REFERENCES dbo.[User](UserID) ON DELETE CASCADE
);
GO

CREATE INDEX IX_PhanCong_User ON dbo.PhanCong(UserID);
GO
CREATE INDEX IX_MonHoc_MaMonHoc ON dbo.MonHoc(MaMonHoc);
GO
CREATE INDEX IX_MonHoc_MaMonHoc_HocKy ON dbo.MonHoc(MaMonHoc, HocKy);
GO

ALTER TABLE dbo.MonHoc ADD GhiChu NVARCHAR(MAX) NULL;
GO

ALTER TABLE dbo.PhanCong
ADD ThoiGianNop DATETIME NULL;
GO

CREATE TRIGGER trg_UpdateThoiGianNop
ON dbo.PhanCong
AFTER UPDATE
AS
BEGIN
    UPDATE pc
    SET pc.ThoiGianNop = GETDATE()
    FROM dbo.PhanCong pc
    INNER JOIN inserted i ON pc.PC_ID = i.PC_ID
    -- FIX: Ghi nhận thời điểm nộp khi trạng thái chuyển sang CHỜ DUYỆT hoặc HOAN_THANH
    WHERE i.TrangThaiGV IN ('CHO_DUYET', 'HOAN_THANH') 
      AND i.ThoiGianNop IS NULL;
END;
GO

----------------------------------------------------
-- 5: BẢNG FILE NỘP 
----------------------------------------------------
PRINT 'Creating Table FileNop...'
CREATE TABLE dbo.FileNop (
    FileID          INT IDENTITY(1,1) PRIMARY KEY,
    PC_ID           INT NOT NULL,             -- Mối liên hệ tới Phân công
    FileName        NVARCHAR(255) NOT NULL,
    FilePath        NVARCHAR(500) NOT NULL,   -- Đường dẫn file lưu trong server
    FileType        NVARCHAR(50) NOT NULL DEFAULT N'PDF',
    FileSizeKB      INT NULL,
    
    TrangThaiDuyet  NVARCHAR(20) NOT NULL DEFAULT N'CHO_DUYET'
                    CHECK (TrangThaiDuyet IN (N'CHO_DUYET', N'DA_DUYET', N'TU_CHOI')),
    GhiChuDuyet     NVARCHAR(500) NULL,

    CreatedAt       DATETIME2(0) NOT NULL DEFAULT SYSDATETIME(),
    ApprovedAt      DATETIME2(0) NULL,
    ApprovedBy      INT NULL,

    CONSTRAINT FK_FN_PC FOREIGN KEY (PC_ID)
        REFERENCES dbo.PhanCong(PC_ID) ON DELETE CASCADE,
    CONSTRAINT FK_FN_User FOREIGN KEY (ApprovedBy)
        REFERENCES dbo.[User](UserID)
);
GO

CREATE INDEX IX_FileNop_PC ON dbo.FileNop(PC_ID);
GO

INSERT INTO dbo.[User] 
(
    FullName,
    UserName,
    PassWord,
    Email,
    Address,
    Gender,
    Role,
    CreatedAt,
    SDT,
    IsLocked
)
VALUES
(N'adminTBM', N'adminTBM', 
 N'889709fd8c0242d8891e28b6838019b58bda86b058c1e6ece5ba104f9f372f5d',
 N'adminTBM@gmail.com',
 N'adminTBM', 
 N'Nam', 
 N'TBM', 
 SYSDATETIME(), 
 N'0912312312', 
 0),

(N'adminGV1', N'adminGV1', 
 N'86f97c914cddf7d7b224138930485ba57ae2b54aa305a642fe5f980e7e7a1642',
 N'adminGV1@gmail.com',
 N'adminGV1', 
 N'Nam', 
 N'GV', 
 SYSDATETIME(), 
 N'0111231231', 
 0),

(N'adminGV2', N'adminGV2', 
 N'd81c7663992bb690c3f070906da613515ed01c3e15ecbc046e68e0edf17b4b70',
 N'adminGV2@gmail.com',
 N'adminGV2', 
 N'Nam', 
 N'GV', 
 SYSDATETIME(), 
 N'0212312311', 
 0);
GO

INSERT INTO dbo.KeHoach (TenKeHoach, Loai, NgayBatDau, NgayKetThuc, NguoiTaoID, CreatedAt)
VALUES (N'Kế hoạch học kỳ I', N'HocKyI', '2025-11-30', '2026-02-28', 1, '2025-11-30 22:26:35');

INSERT INTO dbo.MonHoc (MaMonHoc, TenMonHoc, TenNhom, TenTo, 
                        SoTinChi, HocKy, SoTiet_LT, SoTiet_TH)
VALUES
(N'HM001', N'Nhập Môn Học Máy', N'N01', NULL, 4, N'HocKyI', 30, 20),
(N'HM001', N'Nhập Môn Học Máy', N'N01', N'T01', 4, N'HocKyI', 30, 20),
(N'HM001', N'Nhập Môn Học Máy', N'N01', NULL, 4, N'HocKyII', 30, 20),
(N'HM001', N'Nhập Môn Học Máy', N'N01', N'T01', 4, N'HocKyII', 30, 20);

INSERT INTO dbo.CongViec
(KeHoachID, TenCongViec, MoTa, Loai, HanChot, 
 MucUuTien, TrangThai, NguoiGiaoID, CreatedAt,
 MonHocID, LopPhuTrach, SoTiet, MaDeTai, DiaDiem)
VALUES
(1, N'Test', N'Không có', N'SuKien', '2026-01-30', 
 N'MED', N'HOAN_THANH', 1, '2025-12-02 11:50:22', 
 NULL, NULL, NULL, NULL, NULL),

(1, N'Giảng dạy lý thuyết', NULL, N'GiangDay', '2025-11-30', 
 N'MED', N'MOI', 1, '2025-12-02 20:05:55', 
 1, N'N01', 0, NULL, NULL),

(1, N'Giảng dạy thực hành', NULL, N'GiangDay', '2026-01-10', 
 N'MED', N'DANG_LAM', 1, '2025-12-02 20:06:07', 
 2, N'N01 T01', 0, NULL, NULL);

 INSERT INTO dbo.PhanCong
(CongViecID, UserID, VaiTro, NgayGiao, GhiChu_TBM,
 TrangThaiGV, PhanTram, GhiChu_GV, ThoiDiemCapNhatCuoi)
VALUES
(2, 3, N'ChuTri', '2025-12-02', NULL,
 N'MOI', 0, NULL, '2025-12-02 12:02:41'),

(2, 2, N'HoTro', '2025-12-02', NULL,
 N'MOI', 0, NULL, '2025-12-02 12:03:39'),

(3, 2, N'ChuTri', '2025-12-02', NULL,
 N'MOI', 0, NULL, '2025-12-02 20:06:29'),

(1, 1, N'ChuTri', '2025-12-02', NULL,
 N'MOI', 0, NULL, '2025-12-02 21:03:53');

