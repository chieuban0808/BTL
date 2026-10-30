/* ============================================================
   CSDL: QUẢN LÝ CỬA HÀNG BÁN SÁCH  (SQL Server 2012 trở lên)
   Chạy toàn bộ file trong SSMS. Có thể chạy lại nhiều lần
   (script sẽ xoá và tạo lại CSDL).
   ============================================================ */
USE master;
GO
IF DB_ID(N'QLCuaHangSach') IS NOT NULL
BEGIN
    ALTER DATABASE QLCuaHangSach SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLCuaHangSach;
END
GO
CREATE DATABASE QLCuaHangSach COLLATE Vietnamese_CI_AS;
GO
USE QLCuaHangSach;
GO

/* ======================= 1. BẢNG DANH MỤC ======================= */
CREATE TABLE LoaiSach (
    MaLoai  INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL
);

CREATE TABLE TacGia (
    MaTG     INT IDENTITY(1,1) PRIMARY KEY,
    TenTG    NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(5) NULL,
    DiaChi   NVARCHAR(200) NULL
);

CREATE TABLE NXB (
    MaNXB     INT IDENTITY(1,1) PRIMARY KEY,
    TenNXB    NVARCHAR(150) NOT NULL,
    DiaChi    NVARCHAR(200) NULL,
    DienThoai VARCHAR(15) NULL
);

CREATE TABLE NhaCungCap (
    MaNCC     INT IDENTITY(1,1) PRIMARY KEY,
    TenNCC    NVARCHAR(150) NOT NULL,
    DiaChi    NVARCHAR(200) NULL,
    DienThoai VARCHAR(15) NULL
);

CREATE TABLE KhachHang (
    MaKhach   INT IDENTITY(1,1) PRIMARY KEY,
    TenKhach  NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) NULL,
    DiaChi    NVARCHAR(200) NULL
);

CREATE TABLE CongViec (
    MaCV  INT IDENTITY(1,1) PRIMARY KEY,
    TenCV NVARCHAR(100) NOT NULL
);

CREATE TABLE NhanVien (
    MaNV        INT IDENTITY(1,1) PRIMARY KEY,
    TenNV       NVARCHAR(100) NOT NULL,
    DienThoai   VARCHAR(15) NULL,
    DiaChi      NVARCHAR(200) NULL,
    MaCV        INT NOT NULL REFERENCES CongViec(MaCV),
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau     VARCHAR(64) NOT NULL   -- lưu SHA-256 (hex), KHÔNG lưu mật khẩu thô
);

CREATE TABLE LinhVuc (
    MaLinhVuc  INT IDENTITY(1,1) PRIMARY KEY,
    TenLinhVuc NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE NgonNgu (
    MaNgonNgu  INT IDENTITY(1,1) PRIMARY KEY,
    TenNgonNgu NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Sach (
    MaSach     INT IDENTITY(1,1) PRIMARY KEY,
    TenSach    NVARCHAR(200) NOT NULL,
    MaLoai     INT NOT NULL REFERENCES LoaiSach(MaLoai),
    MaTG       INT NULL REFERENCES TacGia(MaTG),
    MaNXB      INT NULL REFERENCES NXB(MaNXB),
    MaLinhVuc  INT NULL REFERENCES LinhVuc(MaLinhVuc),
    MaNgonNgu  INT NULL REFERENCES NgonNgu(MaNgonNgu),
    SoTrang    INT NULL CHECK (SoTrang > 0),
    Anh        NVARCHAR(260) NULL   -- đường dẫn ảnh bìa
);

/* Kho sách: mỗi sách 1 dòng, do trigger tự cập nhật */
CREATE TABLE KhoSach (
    MaSach     INT PRIMARY KEY REFERENCES Sach(MaSach),
    SoLuong    INT NOT NULL DEFAULT 0 CHECK (SoLuong >= 0),
    DonGiaNhap DECIMAL(18,0) NOT NULL DEFAULT 0,
    DonGiaBan  DECIMAL(18,0) NOT NULL DEFAULT 0
);

/* ======================= 2. BẢNG NGHIỆP VỤ ======================= */
CREATE TABLE HoaDonNhap (
    SoHDN    INT IDENTITY(1,1) PRIMARY KEY,
    MaNV     INT NOT NULL REFERENCES NhanVien(MaNV),
    MaNCC    INT NOT NULL REFERENCES NhaCungCap(MaNCC),
    NgayNhap DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0
);

CREATE TABLE ChiTietHDN (
    SoHDN      INT NOT NULL REFERENCES HoaDonNhap(SoHDN) ON DELETE CASCADE,
    MaSach     INT NOT NULL REFERENCES Sach(MaSach),
    SLNhap     INT NOT NULL CHECK (SLNhap > 0),
    DonGiaNhap DECIMAL(18,0) NOT NULL CHECK (DonGiaNhap >= 0),
    KhuyenMai  DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (KhuyenMai BETWEEN 0 AND 100), -- %
    ThanhTien  AS CONVERT(DECIMAL(18,0), SLNhap * DonGiaNhap * (100 - KhuyenMai) / 100) PERSISTED,
    PRIMARY KEY (SoHDN, MaSach)
);

CREATE TABLE HoaDonBan (
    SoHDB    INT IDENTITY(1,1) PRIMARY KEY,
    MaNV     INT NOT NULL REFERENCES NhanVien(MaNV),
    MaKhach  INT NOT NULL REFERENCES KhachHang(MaKhach),
    NgayBan  DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    TongTien DECIMAL(18,0) NOT NULL DEFAULT 0
);

CREATE TABLE ChiTietHDB (
    SoHDB     INT NOT NULL REFERENCES HoaDonBan(SoHDB) ON DELETE CASCADE,
    MaSach    INT NOT NULL REFERENCES Sach(MaSach),
    SLBan     INT NOT NULL CHECK (SLBan > 0),
    DonGiaBan DECIMAL(18,0) NOT NULL DEFAULT 0,  -- trigger tự lấy từ KhoSach
    KhuyenMai DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (KhuyenMai BETWEEN 0 AND 100), -- %
    ThanhTien AS CONVERT(DECIMAL(18,0), SLBan * DonGiaBan * (100 - KhuyenMai) / 100) PERSISTED,
    PRIMARY KEY (SoHDB, MaSach)
);

CREATE TABLE MatSach (
    MaLanMat INT IDENTITY(1,1) PRIMARY KEY,
    MaSach   INT NOT NULL REFERENCES Sach(MaSach),
    NgayMat  DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    SLMat    INT NOT NULL CHECK (SLMat > 0),
    LyDo     NVARCHAR(200) NULL
);
GO

/* ======================= 3. TRIGGER (yêu cầu 1, 2, 3) ======================= */
/* Các trigger xử lý cả INSERT, UPDATE, DELETE nhiều dòng. Khi một sách có
   nhiều dòng nhập trong cùng lệnh, giá nhập mới lấy từ hóa đơn nhập có SoHDN lớn nhất. */

CREATE TRIGGER trg_ChiTietHDN_Stock ON ChiTietHDN AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DeltaNhap TABLE (MaSach INT PRIMARY KEY, SoLuong INT NOT NULL);
    INSERT INTO @DeltaNhap
    SELECT MaSach, SUM(SoLuong)
    FROM (
        SELECT MaSach, SLNhap AS SoLuong FROM inserted
        UNION ALL
        SELECT MaSach, -SLNhap AS SoLuong FROM deleted
    ) x GROUP BY MaSach;

    IF EXISTS (
        SELECT 1 FROM @DeltaNhap d
        LEFT JOIN KhoSach k ON k.MaSach = d.MaSach
        WHERE k.MaSach IS NULL AND d.SoLuong < 0
           OR k.MaSach IS NOT NULL AND k.SoLuong + d.SoLuong < 0
    )
    BEGIN
        RAISERROR(N'Không thể xóa hoặc sửa chi tiết nhập: số lượng tồn kho không đủ.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    INSERT INTO KhoSach (MaSach, SoLuong, DonGiaNhap, DonGiaBan)
    SELECT d.MaSach, 0, 0, 0
    FROM @DeltaNhap d LEFT JOIN KhoSach k ON k.MaSach = d.MaSach
    WHERE k.MaSach IS NULL AND d.SoLuong >= 0;

    UPDATE k SET k.SoLuong = k.SoLuong + d.SoLuong
    FROM KhoSach k JOIN @DeltaNhap d ON d.MaSach = k.MaSach;

    UPDATE k
    SET k.DonGiaNhap = ISNULL(g.DonGiaNhap, 0),
        k.DonGiaBan = ROUND(ISNULL(g.DonGiaNhap, 0) * 1.1, 0)
    FROM KhoSach k
    JOIN @DeltaNhap d ON d.MaSach = k.MaSach
    OUTER APPLY (
        SELECT TOP (1) c.DonGiaNhap
        FROM ChiTietHDN c
        WHERE c.MaSach = k.MaSach
        ORDER BY c.SoHDN DESC
    ) g;

    UPDATE h
    SET h.TongTien = ISNULL((SELECT SUM(c.ThanhTien) FROM ChiTietHDN c WHERE c.SoHDN = h.SoHDN), 0)
    FROM HoaDonNhap h
    WHERE h.SoHDN IN (SELECT SoHDN FROM inserted UNION SELECT SoHDN FROM deleted);
END
GO

/* Bán: khóa dòng kho trong lúc kiểm tra để các giao dịch đồng thời không bán vượt tồn. */
CREATE TRIGGER trg_ChiTietHDB_Ins ON ChiTietHDB INSTEAD OF INSERT AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Ban TABLE (MaSach INT PRIMARY KEY, SLBan INT NOT NULL);
    INSERT INTO @Ban SELECT MaSach, SUM(SLBan) FROM inserted GROUP BY MaSach;

    IF EXISTS (
        SELECT 1 FROM @Ban b
        LEFT JOIN KhoSach k WITH (UPDLOCK, HOLDLOCK) ON k.MaSach = b.MaSach
        WHERE k.MaSach IS NULL OR k.SoLuong < b.SLBan
    )
    BEGIN
        RAISERROR(N'Số lượng tồn kho không đủ hoặc sách chưa có trong kho.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    INSERT INTO ChiTietHDB (SoHDB, MaSach, SLBan, DonGiaBan, KhuyenMai)
    SELECT i.SoHDB, i.MaSach, i.SLBan, k.DonGiaBan, i.KhuyenMai
    FROM inserted i JOIN KhoSach k ON k.MaSach = i.MaSach;

    UPDATE k SET k.SoLuong = k.SoLuong - b.SLBan
    FROM KhoSach k JOIN @Ban b ON b.MaSach = k.MaSach;

    UPDATE h
    SET h.TongTien = ISNULL((SELECT SUM(c.ThanhTien) FROM ChiTietHDB c WHERE c.SoHDB = h.SoHDB), 0)
    FROM HoaDonBan h
    WHERE h.SoHDB IN (SELECT SoHDB FROM inserted);
END
GO

CREATE TRIGGER trg_ChiTietHDB_Upd ON ChiTietHDB AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    -- Trigger tự đặt lại DonGiaBan; bỏ qua lần gọi lồng do chính thao tác này tạo ra.
    IF TRIGGER_NESTLEVEL() > 1 RETURN;

    DECLARE @Delta TABLE (MaSach INT PRIMARY KEY, SL INT NOT NULL);
    INSERT INTO @Delta
    SELECT MaSach, SUM(SL) FROM (
        SELECT MaSach, SLBan AS SL FROM deleted
        UNION ALL SELECT MaSach, -SLBan FROM inserted
    ) x GROUP BY MaSach;

    IF EXISTS (SELECT 1 FROM @Delta d JOIN KhoSach k WITH (UPDLOCK, HOLDLOCK) ON k.MaSach = d.MaSach
               WHERE k.SoLuong + d.SL < 0)
       OR EXISTS (SELECT 1 FROM @Delta d LEFT JOIN KhoSach k ON k.MaSach = d.MaSach WHERE k.MaSach IS NULL AND d.SL < 0)
    BEGIN
        RAISERROR(N'Số lượng tồn kho không đủ để sửa chi tiết bán.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    UPDATE c SET c.DonGiaBan = k.DonGiaBan
    FROM ChiTietHDB c JOIN inserted i ON i.SoHDB = c.SoHDB AND i.MaSach = c.MaSach
    JOIN KhoSach k ON k.MaSach = c.MaSach;

    UPDATE k SET k.SoLuong = k.SoLuong + d.SL
    FROM KhoSach k JOIN @Delta d ON d.MaSach = k.MaSach;

    UPDATE h
    SET h.TongTien = ISNULL((SELECT SUM(c.ThanhTien) FROM ChiTietHDB c WHERE c.SoHDB = h.SoHDB), 0)
    FROM HoaDonBan h
    WHERE h.SoHDB IN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted);
END
GO

CREATE TRIGGER trg_ChiTietHDB_Del ON ChiTietHDB AFTER DELETE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE k SET k.SoLuong = k.SoLuong + d.SLBan
    FROM KhoSach k JOIN (SELECT MaSach, SUM(SLBan) AS SLBan FROM deleted GROUP BY MaSach) d ON d.MaSach = k.MaSach;

    UPDATE h
    SET h.TongTien = ISNULL((SELECT SUM(c.ThanhTien) FROM ChiTietHDB c WHERE c.SoHDB = h.SoHDB), 0)
    FROM HoaDonBan h WHERE h.SoHDB IN (SELECT SoHDB FROM deleted);
END
GO

CREATE TRIGGER trg_MatSach_Stock ON MatSach AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Delta TABLE (MaSach INT PRIMARY KEY, SL INT NOT NULL);
    INSERT INTO @Delta
    SELECT MaSach, SUM(SL) FROM (
        SELECT MaSach, -SLMat AS SL FROM inserted
        UNION ALL SELECT MaSach, SLMat FROM deleted
    ) x GROUP BY MaSach;

    IF EXISTS (SELECT 1 FROM @Delta d LEFT JOIN KhoSach k WITH (UPDLOCK, HOLDLOCK) ON k.MaSach = d.MaSach
               WHERE k.MaSach IS NULL AND d.SL < 0 OR k.MaSach IS NOT NULL AND k.SoLuong + d.SL < 0)
    BEGIN
        RAISERROR(N'Số lượng tồn kho không đủ để ghi nhận sách mất.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    INSERT INTO KhoSach (MaSach, SoLuong, DonGiaNhap, DonGiaBan)
    SELECT d.MaSach, 0, 0, 0 FROM @Delta d LEFT JOIN KhoSach k ON k.MaSach = d.MaSach
    WHERE k.MaSach IS NULL AND d.SL >= 0;

    UPDATE k SET k.SoLuong = k.SoLuong + d.SL
    FROM KhoSach k JOIN @Delta d ON d.MaSach = k.MaSach;
END
GO

/* ======================= 4. STORED PROCEDURE ======================= */

/* ---- Tìm kiếm (yêu cầu 4, 5): tham số nào NULL thì bỏ qua điều kiện đó ---- */
CREATE PROCEDURE sp_TimSach
    @TenSach NVARCHAR(200) = NULL,
    @MaLoai  INT = NULL,
    @MaNXB   INT = NULL,
    @MaLinhVuc INT = NULL,
    @MaNgonNgu INT = NULL
AS
BEGIN
    SELECT s.MaSach, s.TenSach, s.MaLoai, s.MaTG, s.MaNXB,
           s.MaLinhVuc, s.MaNgonNgu, s.SoTrang, s.Anh,
           l.TenLoai, tg.TenTG, x.TenNXB,
           lv.TenLinhVuc, nn.TenNgonNgu,
           ISNULL(k.SoLuong, 0) AS SoLuong,
           ISNULL(k.DonGiaNhap, 0) AS DonGiaNhap,
           ISNULL(k.DonGiaBan, 0) AS DonGiaBan
    FROM Sach s
    JOIN LoaiSach l ON l.MaLoai = s.MaLoai
    LEFT JOIN TacGia tg ON tg.MaTG = s.MaTG
    LEFT JOIN NXB x ON x.MaNXB = s.MaNXB
    LEFT JOIN LinhVuc lv ON lv.MaLinhVuc = s.MaLinhVuc
    LEFT JOIN NgonNgu nn ON nn.MaNgonNgu = s.MaNgonNgu
    LEFT JOIN KhoSach k ON k.MaSach = s.MaSach
    WHERE (@TenSach IS NULL OR s.TenSach LIKE N'%' + @TenSach + N'%')
      AND (@MaLoai IS NULL OR s.MaLoai = @MaLoai)
      AND (@MaNXB  IS NULL OR s.MaNXB  = @MaNXB)
      AND (@MaLinhVuc IS NULL OR s.MaLinhVuc = @MaLinhVuc)
      AND (@MaNgonNgu IS NULL OR s.MaNgonNgu = @MaNgonNgu);
END
GO

CREATE PROCEDURE sp_TimHoaDonNhap
    @MaSach INT = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL
AS
BEGIN
    SELECT h.SoHDN, h.NgayNhap, ncc.TenNCC, nv.TenNV, h.TongTien
    FROM HoaDonNhap h
    JOIN NhaCungCap ncc ON ncc.MaNCC = h.MaNCC
    JOIN NhanVien nv ON nv.MaNV = h.MaNV
    WHERE (@TuNgay  IS NULL OR h.NgayNhap >= @TuNgay)
      AND (@DenNgay IS NULL OR h.NgayNhap <= @DenNgay)
      AND (@MaSach  IS NULL OR EXISTS (SELECT 1 FROM ChiTietHDN c
                                       WHERE c.SoHDN = h.SoHDN AND c.MaSach = @MaSach))
    ORDER BY h.NgayNhap DESC;
END
GO

/* ---- Yêu cầu 6: sách không bán được trong quý ---- */
CREATE PROCEDURE sp_SachKhongBanTheoQuy @Quy INT, @Nam INT AS
BEGIN
    SELECT s.MaSach, s.TenSach, ISNULL(k.SoLuong, 0) AS TonKho
    FROM Sach s
    LEFT JOIN KhoSach k ON k.MaSach = s.MaSach
    WHERE s.MaSach NOT IN (
        SELECT c.MaSach
        FROM ChiTietHDB c JOIN HoaDonBan h ON h.SoHDB = c.SoHDB
        WHERE DATEPART(QUARTER, h.NgayBan) = @Quy AND YEAR(h.NgayBan) = @Nam);
END
GO

/* ---- Yêu cầu 7: 5 hoá đơn nhập lớn nhất trong năm ---- */
CREATE PROCEDURE sp_Top5HoaDonNhap @Nam INT AS
BEGIN
    SELECT TOP 5 h.SoHDN, h.NgayNhap, ncc.TenNCC, h.TongTien
    FROM HoaDonNhap h JOIN NhaCungCap ncc ON ncc.MaNCC = h.MaNCC
    WHERE YEAR(h.NgayNhap) = @Nam
    ORDER BY h.TongTien DESC;
END
GO

/* ---- Yêu cầu 8: hoá đơn bán và tổng tiền theo quý ---- */
CREATE PROCEDURE sp_HoaDonBanTheoQuy @Quy INT, @Nam INT AS
BEGIN
    SELECT h.SoHDB, h.NgayBan, kh.TenKhach, nv.TenNV, h.TongTien
    FROM HoaDonBan h
    JOIN KhachHang kh ON kh.MaKhach = h.MaKhach
    JOIN NhanVien nv ON nv.MaNV = h.MaNV
    WHERE DATEPART(QUARTER, h.NgayBan) = @Quy AND YEAR(h.NgayBan) = @Nam
    ORDER BY h.NgayBan;

    SELECT ISNULL(SUM(TongTien), 0) AS TongTienQuy
    FROM HoaDonBan
    WHERE DATEPART(QUARTER, NgayBan) = @Quy AND YEAR(NgayBan) = @Nam;
END
GO

/* ---- Yêu cầu 9: khách không mua hàng trong tháng ---- */
CREATE PROCEDURE sp_KhachKhongMuaTheoThang @Thang INT, @Nam INT AS
BEGIN
    SELECT kh.MaKhach, kh.TenKhach, kh.DienThoai
    FROM KhachHang kh
    WHERE kh.MaKhach NOT IN (
        SELECT MaKhach FROM HoaDonBan
        WHERE MONTH(NgayBan) = @Thang AND YEAR(NgayBan) = @Nam);
END
GO

/* ======================= 5. DỮ LIỆU MẪU ======================= */
INSERT INTO CongViec (TenCV) VALUES (N'Quản lý'), (N'Nhân viên bán hàng'), (N'Thủ kho');

-- Mật khẩu mẫu: 123456 (đã băm SHA-256)
INSERT INTO NhanVien (TenNV, DienThoai, DiaChi, MaCV, TenDangNhap, MatKhau) VALUES
(N'Nguyễn Văn An', '0901000001', N'Hà Nội', 1, 'admin', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
(N'Trần Thị Bình', '0901000002', N'Hà Nội', 2, 'banhang', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
(N'Lê Văn Cường', '0901000003', N'Hà Nội', 3, 'thukho', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123456'), 2));

INSERT INTO LoaiSach (TenLoai) VALUES (N'Văn học'), (N'Thiếu nhi'), (N'Kinh tế'), (N'Công nghệ thông tin');

INSERT INTO TacGia (TenTG, GioiTinh) VALUES
(N'Nguyễn Nhật Ánh', N'Nam'), (N'Tô Hoài', N'Nam'), (N'Robert C. Martin', N'Nam'), (N'Dale Carnegie', N'Nam');

INSERT INTO NXB (TenNXB, DiaChi, DienThoai) VALUES
(N'NXB Kim Đồng', N'Hà Nội', '02439434730'),
(N'NXB Trẻ', N'TP. Hồ Chí Minh', '02839316289'),
(N'NXB Lao Động', N'Hà Nội', '02438515380');

INSERT INTO LinhVuc (TenLinhVuc) VALUES
(N'Tiểu thuyết'), (N'Truyện thiếu nhi'), (N'Lập trình'), (N'Kỹ năng sống');

INSERT INTO NgonNgu (TenNgonNgu) VALUES (N'Tiếng Việt'), (N'Tiếng Anh');

INSERT INTO Sach (TenSach, MaLoai, MaTG, MaNXB, MaLinhVuc, MaNgonNgu, SoTrang) VALUES
(N'Cho tôi xin một vé đi tuổi thơ', 1, 1, 2, 1, 1, 220),
(N'Dế Mèn phiêu lưu ký',          2, 2, 1, 2, 1, 160),
(N'Clean Code',                     4, 3, 3, 3, 2, 464),
(N'Đắc nhân tâm',                   3, 4, 2, 4, 1, 320),
(N'Mắt biếc',                       1, 1, 2, 1, 1, 300);

INSERT INTO NhaCungCap (TenNCC, DiaChi, DienThoai) VALUES
(N'Công ty Phát hành sách Hà Nội', N'Hà Nội', '02438000001'),
(N'Công ty Sách Fahasa',           N'TP. Hồ Chí Minh', '02838000002');

INSERT INTO KhachHang (TenKhach, DienThoai, DiaChi) VALUES
(N'Phạm Minh Dũng', '0912000001', N'Hà Nội'),
(N'Hoàng Thị Em',   '0912000002', N'Hà Nội'),
(N'Vũ Quốc Phong',  '0912000003', N'Hải Phòng'),
(N'Đỗ Thu Giang',   '0912000004', N'Hà Nội');

-- Hoá đơn nhập (trigger tự cập nhật kho và tổng tiền)
INSERT INTO HoaDonNhap (MaNV, MaNCC, NgayNhap) VALUES (3, 1, '2026-09-05'), (3, 2, '2026-10-10');
INSERT INTO ChiTietHDN (SoHDN, MaSach, SLNhap, DonGiaNhap, KhuyenMai) VALUES
(1, 1, 100,  45000, 0),
(1, 2,  80,  30000, 0),
(1, 3,  50,  90000, 5),
(2, 4,  40, 120000, 0),
(2, 5,  60,  60000, 0);

-- Hoá đơn bán (trigger tự trừ kho, lấy giá bán từ kho)
INSERT INTO HoaDonBan (MaNV, MaKhach, NgayBan) VALUES
(2, 1, '2026-09-20'), (2, 2, '2026-10-12'), (2, 3, '2026-09-28');
INSERT INTO ChiTietHDB (SoHDB, MaSach, SLBan, KhuyenMai) VALUES
(1, 1, 3, 0),
(1, 2, 2, 0),
(2, 3, 1, 0),
(2, 1, 2, 10),
(3, 3, 2, 0);

-- Sách mất
INSERT INTO MatSach (MaSach, NgayMat, SLMat, LyDo) VALUES (2, '2026-10-15', 1, N'Thất lạc khi kiểm kê');
GO

/* ======================= 6. CHẠY THỬ (bỏ comment để thử) =======================
SELECT * FROM KhoSach;
SELECT * FROM HoaDonNhap;
SELECT * FROM HoaDonBan;
EXEC sp_TimSach @TenSach = N'code';
EXEC sp_TimHoaDonNhap @MaSach = 1;
EXEC sp_SachKhongBanTheoQuy @Quy = 3, @Nam = 2026;
EXEC sp_Top5HoaDonNhap @Nam = 2026;
EXEC sp_HoaDonBanTheoQuy @Quy = 3, @Nam = 2026;
EXEC sp_KhachKhongMuaTheoThang @Thang = 10, @Nam = 2026;
=============================================================================== */
