# Quản lý cửa hàng sách

Ứng dụng WinForms quản lý bán hàng, nhập hàng, kho sách, danh mục, tra cứu và báo cáo.

## Yêu cầu

- Windows và Visual Studio với workload **.NET desktop development**.
- .NET Framework 4.7.2 Developer Pack/Targeting Pack.
- SQL Server Express LocalDB với instance `MSSQLLocalDB`.
- SQL Server Management Studio (SSMS) hoặc công cụ tương đương để chạy script tạo dữ liệu.

## Tạo cơ sở dữ liệu

1. Mở `TaoMoi_QLCuaHangSach.sql` trong SSMS.
2. Chọn instance `(localdb)\MSSQLLocalDB` và chạy toàn bộ script.
3. Script tạo database `QLCuaHangSach`, các bảng, trigger, stored procedure và dữ liệu mẫu. **Script sẽ xóa database `QLCuaHangSach` hiện có trước khi tạo lại**, vì vậy chỉ chạy khi muốn tạo mới/reset database.

Nếu dùng SQL Server khác, cập nhật `Data Source` trong `QuanLyCuaHangSach/QuanLyCuaHangSach/App.config` cho đúng instance. Database mặc định trong connection string là `QLCuaHangSach`.

## Mở và chạy ứng dụng

1. Mở `QuanLyCuaHangSach/QuanLyCuaHangSach.slnx` bằng Visual Studio.
2. Chọn project `QuanLyCuaHangSach` làm startup project.
3. Build solution rồi chạy bằng `F5`.

## Tài khoản mẫu

Script SQL tạo ba tài khoản dưới đây. Mật khẩu ban đầu cho cả ba là `123456` (được lưu dưới dạng SHA-256).

| Vai trò | Tên đăng nhập | Mật khẩu |
| --- | --- | --- |
| Quản lý | `admin` | `123456` |
| Nhân viên bán hàng | `banhang` | `123456` |
| Thủ kho | `thukho` | `123456` |

## Ghi chú phát triển

Các thư mục `.vs`, `bin` và `obj` là dữ liệu cục bộ của Visual Studio/build; chúng được bỏ qua bởi Git. Sau khi clone hoặc pull, build solution để Visual Studio tạo lại các tệp cần thiết.
