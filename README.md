Sales Management API

Giới thiệu

Hệ thống quản lý bán hàng sử dụng ASP.NET Core Web API kết hợp SQL Server.

Hệ thống cho phép quản lý:

- Sản phẩm
- Danh mục
- Khách hàng
- Hóa đơn


Kiến trúc hệ thống

Xây dựng theo mô hình 3-layer architecture:

- Presentation Layer (Controller): Nhận request từ client và trả response
- Business Logic Layer (Service): Xử lý nghiệp vụ
- Data Access Layer (Repository): Làm việc với database


Công nghệ sử dụng

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Swagger (test API)


Cơ sở dữ liệu

Tables

- Categories
- Products
- Customers
- Orders
- OrderDetails

View

- vw_ProductWithCategory: hiển thị sản phẩm kèm danh mục
- vw_OrderSummary: hiển thị hóa đơn kèm khách hàng

Function

- fn_TotalOrder(@OrderId): tính tổng tiền hóa đơn

Stored Procedure

- sp_CreateOrder
- sp_GetProductsByCategory

Trigger

- trg_UpdateStock: tự động giảm số lượng sản phẩm khi tạo hóa đơn

Transaction

- Sử dụng transaction để đảm bảo toàn vẹn dữ liệu khi tạo hóa đơn


API chính

Product

- GET /api/Products
- GET /api/Products/{id}
- GET /api/Products/by-category/{id}
- POST /apiProducts
- PUT /api/Products
- DELETE /api/Products/{id}

Category

- GET /api/Categories
- GET /api/Categories/{id}
- POST /api/Categories
- PUT /api/Categories
- DELETE /api/categories/{id}

Customer

- GET /api/customer
- GET /api/customer/{id}
- POST /api/customer
- PUT /api/customer
- DELETE /api/customer/{id}

Order

- GET /api/order
- GET /api/order/{id}
- POST /api/order
- DELETE /api/order/{id}


Luồng xử lý chính

Tạo hóa đơn

1. Nhận danh sách sản phẩm từ client
2. Kiểm tra sản phẩm tồn tại
3. Tạo Order và OrderDetails
4. Trigger tự động cập nhật Stock


Hướng dẫn chạy project

Clone project

bash
git clone https://github.com/2351010035duy/LT-CSDL.git

Tạo database

Mở SQL Server
Chạy file script SQL

Cấu hình connection string

Trong appsettings.json

4. Run project

- Mở bằng Visual Studio
- Nhấn F5

5. Test API

Truy cập:

https://localhost:xxxx/swagger


Tác giả

- Sinh viên thực hiện: 
	+ Nguyễn Hữu Duy - 2351010035
	+ Phùng Quang Minh - 2351010128
- Môn học: Lập trình CSDL
- Giảng viên: ThS. Phạm Hoàng An


Ghi chú

- Dữ liệu mẫu đã được thêm sẵn
- Hệ thống sử dụng DTO để tách dữ liệu
- p dụng LINQ và Entity Framework để truy vấn
