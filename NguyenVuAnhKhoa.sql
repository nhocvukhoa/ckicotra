CREATE DATABASE NguyenVuAnhKhoaDB

USE NguyenVuAnhKhoaDB

CREATE TABLE UserAccount(
	ID  bigint IDENTITY(1,1) primary key,
	Username varchar(50) null,
	Password varchar(32) null,
	Status nvarchar(30) null
)

CREATE TABLE Category(
	ID bigint IDENTITY(1,1) primary key,
	Name nvarchar(100) null,
	Description nvarchar(255) null 
)

CREATE TABLE Product(
	ID bigint IDENTITY(1,1) primary key,
	Name nvarchar(250) null,
	UnitCost decimal(18,0) null,
	Quantity int null,
	Image nvarchar(250) null,
	Description nvarchar(255) null,
	Status nvarchar(30) null,
	ProductType bigint null,
	constraint fk_Product_Category foreign key (ProductType) references Category(ID)
)

ALTER TABLE Product
ALTER COLUMN Status int null