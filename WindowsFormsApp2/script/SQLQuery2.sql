-- LẤY RoleId
DECLARE @ADMIN   INT = (SELECT RoleId FROM Roles WHERE RoleCode='ADMIN');
DECLARE @SALES   INT = (SELECT RoleId FROM Roles WHERE RoleCode='SALES');
DECLARE @KETOAN  INT = (SELECT RoleId FROM Roles WHERE RoleCode='KETOAN');
DECLARE @KHO     INT = (SELECT RoleId FROM Roles WHERE RoleCode='KHO');
DECLARE @SANXUAT INT = (SELECT RoleId FROM Roles WHERE RoleCode='SANXUAT');
DECLARE @GIAMDOC INT = (SELECT RoleId FROM Roles WHERE RoleCode='GIAMDOC');

-- tiện: hàm chèn quyền theo PermCode
CREATE TABLE #perm_list(code NVARCHAR(100));

-- ADMIN: UserMgmt CRUD, các module khác View
INSERT INTO #perm_list(code) VALUES
('UserMgmt.View'),('UserMgmt.Create'),('UserMgmt.Update'),('UserMgmt.Delete'),
('BaoGia.View'),('PO.View'),('SanXuat.View'),('Kho.View'),('HoaDon.View');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @ADMIN, p.PermId
FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@ADMIN AND rp.PermId=p.PermId);
TRUNCATE TABLE #perm_list;

-- SALES: Báo giá CRUD
INSERT INTO #perm_list(code) VALUES
('BaoGia.View'),('BaoGia.Create'),('BaoGia.Update'),('BaoGia.Delete');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @SALES, p.PermId FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@SALES AND rp.PermId=p.PermId);
TRUNCATE TABLE #perm_list;

-- KẾ TOÁN: Báo giá View, PO.Approve, Kho.View, Hóa đơn CRUD
INSERT INTO #perm_list(code) VALUES
('BaoGia.View'),
('PO.View'),('PO.Approve'),
('Kho.View'),
('HoaDon.View'),('HoaDon.Create'),('HoaDon.Update'),('HoaDon.Delete');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @KETOAN, p.PermId FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@KETOAN AND rp.PermId=p.PermId);
TRUNCATE TABLE #perm_list;

-- KHO: SanXuat.Issue, Kho CRUD + Issue
INSERT INTO #perm_list(code) VALUES
('SanXuat.Issue'),
('Kho.View'),('Kho.Create'),('Kho.Update'),('Kho.Delete'),('Kho.Issue');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @KHO, p.PermId FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@KHO AND rp.PermId=p.PermId);
TRUNCATE TABLE #perm_list;

-- SẢN XUẤT (MO): CRUD
INSERT INTO #perm_list(code) VALUES
('SanXuat.View'),('SanXuat.Create'),('SanXuat.Update'),('SanXuat.Delete');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @SANXUAT, p.PermId FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@SANXUAT AND rp.PermId=p.PermId);
TRUNCATE TABLE #perm_list;

-- GIÁM ĐỐC: UserMgmt.View, BaoGia.Approve, PO.ApproveOver20tr, các module khác View
INSERT INTO #perm_list(code) VALUES
('UserMgmt.View'),
('BaoGia.View'),('BaoGia.Approve'),
('PO.View'),('PO.ApproveOver20tr'),
('SanXuat.View'),('Kho.View'),('HoaDon.View');

INSERT INTO RolePermissions(RoleId, PermId)
SELECT @GIAMDOC, p.PermId FROM Permissions p JOIN #perm_list l ON l.code=p.PermCode
WHERE NOT EXISTS (SELECT 1 FROM RolePermissions rp WHERE rp.RoleId=@GIAMDOC AND rp.PermId=p.PermId);

DROP TABLE #perm_list;
