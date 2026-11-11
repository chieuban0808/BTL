# Sơ đồ hệ thống quản lý cửa hàng sách

Các sơ đồ dưới đây được xây dựng theo database `QLCuaHangSach` và các chức năng hiện có trong ứng dụng WinForms.

## 1. ERD – Mô hình thực thể liên kết

```mermaid
erDiagram
    CONGVIEC ||--o{ NHANVIEN : phan_cong
    NHANVIEN ||--o{ HOADONNHAP : lap
    NHACUNGCAP ||--o{ HOADONNHAP : cung_cap
    HOADONNHAP ||--|{ CHITIETHDN : gom
    SACH ||--o{ CHITIETHDN : duoc_nhap
    NHANVIEN ||--o{ HOADONBAN : lap
    KHACHHANG ||--o{ HOADONBAN : mua
    HOADONBAN ||--|{ CHITIETHDB : gom
    SACH ||--o{ CHITIETHDB : duoc_ban
    LOAISACH ||--o{ SACH : phan_loai
    TACGIA o|--o{ SACH : viet
    NXB o|--o{ SACH : xuat_ban
    LINHVUC o|--o{ SACH : thuoc
    NGONNGU o|--o{ SACH : su_dung
    SACH ||--o| KHOSACH : ton_kho
    SACH ||--o{ MATSACH : ghi_nhan_mat

    CONGVIEC {
        int MaCV PK
        nvarchar TenCV
    }
    NHANVIEN {
        int MaNV PK
        nvarchar TenNV
        varchar DienThoai
        nvarchar DiaChi
        int MaCV FK
        varchar TenDangNhap UK
        varchar MatKhau
    }
    KHACHHANG {
        int MaKhach PK
        nvarchar TenKhach
        varchar DienThoai
        nvarchar DiaChi
    }
    NHACUNGCAP {
        int MaNCC PK
        nvarchar TenNCC
        nvarchar DiaChi
        varchar DienThoai
    }
    LOAISACH {
        int MaLoai PK
        nvarchar TenLoai
    }
    TACGIA {
        int MaTG PK
        nvarchar TenTG
        date NgaySinh
        nvarchar GioiTinh
        nvarchar DiaChi
    }
    NXB {
        int MaNXB PK
        nvarchar TenNXB
        nvarchar DiaChi
        varchar DienThoai
    }
    LINHVUC {
        int MaLinhVuc PK
        nvarchar TenLinhVuc UK
    }
    NGONNGU {
        int MaNgonNgu PK
        nvarchar TenNgonNgu UK
    }
    SACH {
        int MaSach PK
        nvarchar TenSach
        int MaLoai FK
        int MaTG FK
        int MaNXB FK
        int MaLinhVuc FK
        int MaNgonNgu FK
        int SoTrang
        nvarchar Anh
    }
    KHOSACH {
        int MaSach PK, FK
        int SoLuong
        decimal DonGiaNhap
        decimal DonGiaBan
    }
    HOADONNHAP {
        int SoHDN PK
        int MaNV FK
        int MaNCC FK
        date NgayNhap
        decimal TongTien
    }
    CHITIETHDN {
        int SoHDN PK, FK
        int MaSach PK, FK
        int SLNhap
        decimal DonGiaNhap
        decimal KhuyenMai
        decimal ThanhTien
    }
    HOADONBAN {
        int SoHDB PK
        int MaNV FK
        int MaKhach FK
        date NgayBan
        decimal TongTien
    }
    CHITIETHDB {
        int SoHDB PK, FK
        int MaSach PK, FK
        int SLBan
        decimal DonGiaBan
        decimal KhuyenMai
        decimal ThanhTien
    }
    MATSACH {
        int MaLanMat PK
        int MaSach FK
        date NgayMat
        int SLMat
        nvarchar LyDo
    }
```

**Ghi chú:** `ChiTietHDN` và `ChiTietHDB` có khóa chính ghép từ mã hóa đơn và mã sách. Số lượng tồn kho được theo dõi tại `KhoSach`; trigger cập nhật tồn khi phát sinh nhập, bán hoặc ghi nhận mất sách.

## 2. BFD – Sơ đồ phân rã chức năng

Hệ thống gồm 5 nhóm chức năng và 11 chức năng con, theo phạm vi sơ đồ mẫu.

```mermaid
flowchart LR
    A[Quản lý<br/>cửa hàng sách]
    A --> B[1. Quản lý danh mục]
    B --> B1[Sách, loại sách, tác giả, NXB]
    B --> B2[Khách hàng, NCC, nhân viên, công việc]
    A --> C[2. Quản lý nhập hàng]
    C --> C1[Lập hóa đơn nhập]
    C --> C2[Tìm hóa đơn nhập]
    A --> D[3. Quản lý bán hàng]
    D --> D1[Lập hóa đơn bán]
    A --> E[4. Quản lý kho]
    E --> E1[Tìm kiếm sách theo tên, loại, NXB]
    E --> E2[Ghi nhận sách mất]
    A --> F[5. Báo cáo, thống kê]
    F --> F1[Sách không bán được theo quý]
    F --> F2[5 hóa đơn nhập lớn nhất theo năm]
    F --> F3[Hóa đơn và tổng tiền bán theo quý]
    F --> F4[Khách không mua hàng theo tháng]
```

## 3. DFD – Sơ đồ luồng dữ liệu

### 3.1. DFD mức 1

Quy ước: hình chữ nhật là tác nhân ngoài, tiến trình được đánh số, kho dữ liệu ký hiệu D1–D4. Nhập hàng, bán hàng và ghi nhận sách mất đều cập nhật kho D1; báo cáo chỉ đọc dữ liệu.

```mermaid
flowchart LR
    QL[Quản lý] -->|danh mục| P1((1. Quản lý danh mục))
    P1 -->|cập nhật danh mục sách và liên quan| D1[(D1 Kho sách và danh mục)]

    NCC[Nhà cung cấp] -->|hàng nhập| P2((2. Nhập hàng))
    P2 -->|hóa đơn nhập| D2[(D2 Hóa đơn nhập)]
    P2 -->|cập nhật tồn kho| D1

    KH[Khách hàng] -->|yêu cầu mua| P3((3. Bán hàng))
    P3 -->|hóa đơn| KH
    P3 -->|hóa đơn bán| D3[(D3 Hóa đơn bán)]
    P3 -->|cập nhật tồn kho| D1

    NVK[Nhân viên kho] -->|sách mất| P4((4. Ghi nhận sách mất))
    P4 -->|lần mất sách| D4[(D4 Mất sách)]
    P4 -->|cập nhật tồn kho| D1

    QL -->|chọn kỳ| P5((5. Báo cáo, thống kê))
    D2 -->|dữ liệu nhập| P5
    D3 -->|dữ liệu bán| P5
    D1 -->|dữ liệu sách và tồn kho| P5
    P5 -->|báo cáo| QL
```

