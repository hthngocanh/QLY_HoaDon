# HỆ THỐNG QUẢN LÝ HÓA ĐƠN

Đồ án môn học: **Lập trình Windows**
Repository: [https://github.com/hthngocanh/QLY_HoaDon.git](https://github.com/hthngocanh/QLY_HoaDon.git)

---

## 1. Giới thiệu tổng quan

**Hệ thống Quản lý Hóa đơn** là ứng dụng desktop xây dựng trên nền tảng Windows Forms và C#, được thiết kế theo kiến trúc 3 lớp (3-Layer Architecture) kết hợp DTO và Utilities nhằm phục vụ công tác quản lý khách hàng, sản phẩm, nhân viên, tài khoản và lập - theo dõi hóa đơn bán hàng một cách chuyên nghiệp, chính xác và hiệu quả.

---

## 2. Công nghệ sử dụng

- **Ngôn ngữ lập trình:** C# 12
- **Nền tảng giao diện:** Windows Forms (WinForms) trên nền **.NET 10 (LTS)** (`net10.0-windows`)
- **Môi trường phát triển (IDE):** Visual Studio Community 2026 / Visual Studio 2022 (v18+)
- **Hệ quản trị cơ sở dữ liệu:** Microsoft SQL Server
- **Thư viện kết nối CSDL:** `Microsoft.Data.SqlClient`
- **Quản lý cấu hình:** `System.Configuration.ConfigurationManager`

---

## 3. Kiến trúc dự án (3-Layer Architecture)

Dự án tuân thủ nghiêm ngặt nguyên tắc phân tầng và chiều phụ thuộc:
**GUI $\rightarrow$ BLL $\rightarrow$ DAL** (DTO dùng chung trao đổi dữ liệu, Utilities hỗ trợ tiện ích dùng chung).

```
                               ┌─────────────┐
                               │     GUI     │ (Forms, UserControls)
                               └──────┬──────┘
                                      │ calls
                               ┌──────▼──────┐
                               │     BLL     │ (Business Logic & Validations)
                               └──────┬──────┘
                                      │ calls
                               ┌──────▼──────┐
                               │     DAL     │ (Data Access: SQL Server)
                               └──────┬──────┘
                                      │ queries
                               ┌──────▼──────┐
                               │  SQL Server │ (Database: QuanLyHoaDon)
                               └─────────────┘
          [DTO]: Truyền tải dữ liệu giữa các tầng (Data Transfer Objects)
          [Utilities]: Tiện ích dùng chung (Theme, Session, Password, Validation)
```

- **GUI (Graphical User Interface):** Chịu trách nhiệm hiển thị giao diện người dùng, tiếp nhận sự kiện và gọi BLL để xử lý nghiệp vụ. Tuyệt đối không chứa câu lệnh SQL và không gọi trực tiếp DAL.
- **BLL (Business Logic Layer):** Kiểm tra tính hợp lệ của dữ liệu, quy tắc nghiệp vụ, gọi DAL để đọc/ghi dữ liệu và trả kết quả cho GUI.
- **DAL (Data Access Layer):** Chịu trách nhiệm kết nối SQL Server, thực thi truy vấn CRUD thông qua `SqlConnection`, `SqlCommand`, `SqlDataReader`.
- **DTO (Data Transfer Object):** Các lớp đối tượng dữ liệu thuần túy (POCO) dùng để vận chuyển dữ liệu giữa các tầng.
- **Utilities:** Các lớp dùng chung toàn hệ thống:
  - `ThemeHelper`: Quản lý màu sắc (#1E3A5F chủ đạo, #2563EB thao tác chính...), font Segoe UI.
  - `Session`: Quản lý trạng thái phiên đăng nhập của người dùng.
  - `PasswordHelper`: Băm và kiểm tra mật khẩu bằng thuật toán SHA-256 an toàn.
  - `ValidationHelper`: Kiểm tra dữ liệu hợp lệ (rỗng, định dạng email, số điện thoại, số dương).

---

## 4. Cấu trúc thư mục

```text
QLY_HoaDon/
│
├── CSDL/
│   ├── 01_TaoCoSoDuLieu.sql       # Script tạo database QuanLyHoaDon và 7 bảng
│   └── 02_ThemDuLieuMau.sql        # Script nạp dữ liệu mẫu ban đầu
│
├── TaiLieu/                        # Tài liệu đặc tả, báo cáo đồ án
│
├── QuanLyHoaDon/                   # WinForms Project chính
│   ├── GUI/                        # Tầng hiển thị giao diện
│   │   ├── Forms/                  # Các màn hình nghiệp vụ
│   │   │   ├── HeThong/            # Form chính, đăng nhập, thông tin hệ thống
│   │   │   ├── KhachHang/          # Màn hình quản lý khách hàng
│   │   │   ├── SanPham/            # Màn hình quản lý sản phẩm
│   │   │   ├── NhanVien/           # Màn hình quản lý nhân viên
│   │   │   ├── HoaDon/             # Màn hình lập và tra cứu hóa đơn
│   │   │   └── TaiKhoan/           # Màn hình quản trị tài khoản & phân quyền
│   │   └── Controls/               # Các UserControl dùng chung
│   │
│   ├── BLL/                        # Tầng xử lý logic nghiệp vụ
│   ├── DAL/                        # Tầng truy xuất cơ sở dữ liệu
│   │   └── KetNoiCSDL.cs           # Lớp kết nối SQL Server tập trung
│   ├── DTO/                        # Tầng đối tượng truyền tải dữ liệu
│   ├── Utilities/                  # Tầng tiện ích hỗ trợ dùng chung
│   │   ├── ThemeHelper.cs          # Quản lý màu sắc và font chữ Segoe UI
│   │   ├── Session.cs              # Quản lý phiên đăng nhập
│   │   ├── PasswordHelper.cs       # Băm mật khẩu SHA-256
│   │   └── ValidationHelper.cs     # Kiểm tra dữ liệu đầu vào
│   │
│   ├── App.config                  # Cấu hình chuỗi kết nối CSDL
│   ├── Program.cs                  # Điểm khởi chạy ứng dụng
│   └── QuanLyHoaDon.csproj         # Project file C# (.NET 10 LTS)
│
├── .gitignore                      # Cấu hình loại trừ file build, cache
├── README.md                       # Tài liệu hướng dẫn dự án
└── QuanLyHoaDon.sln                # Visual Studio Solution
```

---

## 5. Cơ sở dữ liệu (Database Design)

- **Tên Database:** `QuanLyHoaDon`
- **Gồm 7 bảng chuẩn hóa:**
  1. `VaiTro`: Quản lý quyền hạn (QuanTriVien, NhanVienBanHang...).
  2. `NhanVien`: Quản lý thông tin nhân sự.
  3. `TaiKhoan`: Tài khoản đăng nhập liên kết 1-1 với nhân viên, mật khẩu băm SHA-256.
  4. `KhachHang`: Thông tin khách hàng mua hàng.
  5. `SanPham`: Danh mục hàng hóa, đơn vị tính, đơn giá, tồn kho.
  6. `HoaDon`: Thông tin hóa đơn (Số hóa đơn, ngày lập, nhân viên, khách hàng, tổng tiền).
  7. `ChiTietHoaDon`: Chi tiết các mặt hàng trong hóa đơn, thành tiền tự tính.

---

## 6. Yêu cầu môi trường

- Hệ điều hành: Windows 10 / Windows 11 (64-bit)
- .NET SDK: **.NET 10.0+**
- Visual Studio: **Visual Studio 2026** (hoặc Visual Studio 2022 phiên bản mới nhất hỗ trợ .NET 10) với Workload: *.NET desktop development* (Phát triển ứng dụng cho máy tính để bàn với .NET).
- SQL Server: Microsoft SQL Server 2019/2022 hoặc SQL Server Express / LocalDB.

---

## 7. Hướng dẫn mở và chạy dự án

### Bước 1: Thiết lập Cơ sở dữ liệu
1. Mở **SQL Server Management Studio (SSMS)**.
2. Kết nối tới SQL Server instance trên máy.
3. Mở và thực thi lần lượt 2 file script theo thứ tự:
   - `CSDL/01_TaoCoSoDuLieu.sql` (Tạo database và cấu trúc 7 bảng).
   - `CSDL/02_ThemDuLieuMau.sql` (Nạp dữ liệu mẫu ban đầu).

### Bước 2: Cấu hình chuỗi kết nối
Mở file `QuanLyHoaDon/App.config` và điều chỉnh `connectionString` nếu instance SQL Server của bạn có tên khác mặc định:
```xml
<connectionStrings>
  <add name="QuanLyHoaDonConnection"
       connectionString="Data Source=.;Initial Catalog=QuanLyHoaDon;Integrated Security=True;TrustServerCertificate=True;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

### Bước 3: Mở và chạy trên Visual Studio
1. Nhấp đúp vào file `QuanLyHoaDon.sln` để mở Solution bằng Visual Studio.
2. Nhấn tổ hợp phím `Ctrl + Shift + B` để build toàn bộ Solution.
3. Nhấn `F5` hoặc nút `Start` để khởi chạy ứng dụng.
4. Màn hình khởi động `FormChinh` sẽ xuất hiện với đầy đủ thông tin xác nhận kiến trúc hệ thống.

Hoặc chạy nhanh từ dòng lệnh:
```bash
dotnet build QuanLyHoaDon.sln
dotnet run --project QuanLyHoaDon/QuanLyHoaDon.csproj
```

