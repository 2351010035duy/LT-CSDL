-- =============================================
-- CREATE DATABASE
-- =============================================
IF DB_ID('SalesDB') IS NOT NULL
    DROP DATABASE SalesDB;
GO

CREATE DATABASE SalesDB;
GO

USE SalesDB;
GO

-- =============================================
-- TABLES
-- =============================================

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Price DECIMAL(10,2),
    Stock INT,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(200) -- ✅ THÊM MỚI
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT,
    ProductId INT,
    Quantity INT,
    Price DECIMAL(10,2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- =============================================
-- SAMPLE DATA 
-- =============================================

INSERT INTO Categories(Name) VALUES
(N'Laptop'),
(N'Điện thoại'),
(N'Phụ kiện'),
(N'Màn hình'),
(N'Thiết bị văn phòng');

INSERT INTO Products(Name, Price, Stock, CategoryId) VALUES
(N'Dell XPS 13', 2000, 10, 1),
(N'Macbook Pro M2', 2500, 5, 1),
(N'HP Pavilion', 1500, 8, 1),

(N'iPhone 14', 1200, 20, 2),
(N'Samsung S23', 1000, 15, 2),
(N'Xiaomi 13', 800, 25, 2),

(N'Chuột Logitech', 50, 100, 3),
(N'Bàn phím cơ', 120, 50, 3),

(N'Màn hình LG 27"', 300, 20, 4),
(N'Màn hình Samsung 24"', 250, 15, 4),

(N'Máy in Canon', 200, 10, 5);

INSERT INTO Customers(Name, Phone, Address) VALUES
(N'Nguyễn Văn A', '0123456789', N'Hà Nội'),
(N'Trần Thị B', '0987654321', N'Hồ Chí Minh'),
(N'Lê Văn C', '0111111111', N'Đà Nẵng'),
(N'Phạm Thị D', '0222222222', N'Hải Phòng'),
(N'Hoàng Văn E', '0333333333', N'Cần Thơ');

INSERT INTO Orders(CustomerId) VALUES
(1),(2),(3),(1),(4);

INSERT INTO OrderDetails(OrderId, ProductId, Quantity, Price) VALUES
(1, 1, 1, 2000),
(1, 7, 2, 50),

(2, 4, 1, 1200),
(2, 8, 1, 120),

(3, 5, 2, 1000),

(4, 2, 1, 2500),
(4, 9, 1, 300),

(5, 10, 2, 250);

-- =============================================
-- VIEW
-- =============================================

CREATE VIEW vw_ProductWithCategory AS
SELECT 
    p.ProductId,
    p.Name AS ProductName,
    p.Price,
    p.Stock,
    c.Name AS CategoryName
FROM Products p
JOIN Categories c ON p.CategoryId = c.CategoryId;

CREATE VIEW vw_OrderSummary AS
SELECT 
    o.OrderId,
    c.Name AS CustomerName,
    c.Address,
    o.OrderDate
FROM Orders o
JOIN Customers c ON o.CustomerId = c.CustomerId;

-- =============================================
-- FUNCTION
-- =============================================

CREATE FUNCTION fn_TotalOrder(@OrderId INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total DECIMAL(10,2);

    SELECT @total = SUM(Quantity * Price)
    FROM OrderDetails
    WHERE OrderId = @OrderId;

    RETURN ISNULL(@total, 0);
END;

-- =============================================
-- STORED PROCEDURE
-- =============================================

CREATE PROCEDURE sp_CreateOrder
    @CustomerId INT
AS
BEGIN
    INSERT INTO Orders(CustomerId)
    VALUES (@CustomerId);
END;

CREATE PROCEDURE sp_GetProductsByCategory
    @CategoryId INT
AS
BEGIN
    SELECT * 
    FROM Products
    WHERE CategoryId = @CategoryId;
END;

-- =============================================
-- TRIGGER
-- =============================================

CREATE TRIGGER trg_UpdateStock
ON OrderDetails
AFTER INSERT
AS
BEGIN
    UPDATE p
    SET p.Stock = p.Stock - i.Quantity
    FROM Products p
    JOIN inserted i ON p.ProductId = i.ProductId;
END;

-- =============================================
-- TRANSACTION TEST
-- =============================================

BEGIN TRANSACTION;

BEGIN TRY
    DECLARE @OrderId INT;

    INSERT INTO Orders(CustomerId)
    VALUES (1);

    SET @OrderId = SCOPE_IDENTITY();

    INSERT INTO OrderDetails(OrderId, ProductId, Quantity, Price)
    VALUES (@OrderId, 1, 2, 2000);

    COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;

-- =============================================
-- TEST
-- =============================================

SELECT * FROM vw_ProductWithCategory;
SELECT dbo.fn_TotalOrder(1);
EXEC sp_GetProductsByCategory 1;