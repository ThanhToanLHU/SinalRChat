
CREATE DATABASE [HoaDon]
GO
USE [HoaDon]
GO



/****** Object:  Table [dbo].[KhachHang]    Script Date: 13/04/2022 8:20:33 SA ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [KhachHang](
	[MaKH] [int] NOT NULL primary key,
	[TenKH] [nvarchar](255) NULL,
	[DCKH] [nvarchar](255) NULL,
	[DTKH] [nvarchar](255) NULL
	)
 GO

 CREATE TABLE [KhuVuc](
	[MaKV] [int] NOT NULL primary key,
	[TenKV] [nvarchar](255) NULL,
	[MaNVQL] [int] NULL
) 
go


CREATE TABLE [dbo].[LoaiHang](
	[MaLH] [nvarchar](255) NOT NULL PRIMARY KEY,
	[TenLH] [nvarchar](255) NULL)

GO
CREATE TABLE [MatHang](
	[MaMH] [int] NOT NULL PRIMARY KEY,
	[TenMH] [nvarchar](255) NULL,
	[DonViTinh] [nvarchar](255) NULL,
	[DonGia] [float] NULL,
	[SoTon] [int] NULL,
	[MaLH] [nvarchar](255) NULL,
	Foreign key (MaLH) References LoaiHang (MaLH)
	)
 
 GO

CREATE TABLE [MatHang2](
	[MaMH] [int] NOT NULL PRIMARY KEY,
	[TenMH] [nvarchar](255) NULL,
	[DonViTinh] [nvarchar](255) NULL,
	[DonGia] [float] NULL,
	[SoTon] [int] NULL,
	[MaLH] [nvarchar](255) NULL)
 
 GO
 CREATE TABLE [NhanVien](
	[MaNV] [int] NOT NULL PRIMARY KEY,
	[Ho] [nvarchar](255) NULL,
	[Ten] [nvarchar](255) NULL,
	[Nu] [int] NULL,
	[LuongCB] [float] NULL,
	[CongViec] [nvarchar](255) NULL,
	[MaKV] [int] NULL,
	Foreign key (MaKV) References KhuVuc (MaKV)
	)
GO

CREATE TABLE [HoaDon](
	[SoHD] [int] NOT NULL PRIMARY KEY,
	[NgayHD] [datetime] NULL,
	[NgayGiao] [datetime] NULL,
	[MaKH] [int] NULL,
	[MaNV] [int] NULL,
	Foreign key (MaNV) References NhanVien (MaNV),
	Foreign key (MaKH) References KhachHang (MaKH)
	)

GO
CREATE TABLE [CTDH](
	[SoHD] [int] NOT NULL,
	[MaMH] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[DGBan] [float] NULL,
	Foreign key (SoHD) References HoaDon (SoHD),
	Foreign key (MaMH) References MatHang (MaMH),
	PRIMARY KEY (SoHD, MaMH)
) 

GO

INSERT INTO [LoaiHang]  VALUES
('L1',N'Loại 1'),
('L2',N'Loại 2'),
('L3',N'Loại 3'),
('L4',N'Loại 4')
GO
INSERT INTO [MatHang]  VALUES
(1,N'Ruợu',N'Chai',230,5,N'L2'),
(2,N'Gia Vị',N'Thùng',40,0,N'L3'),
(3,N'Bánh Kem',N'Cái',10,10,N'L2'),
(4,N'Bơ',N'KG',38,0,N'L3'),
(5,N'Bánh mì',N'Cái',8,20,N'L2')

GO
INSERT INTO [MatHang2]  VALUES
(3,N'Bánh kem',N'Cái',10,10,N'L2'),
(4,N'Bơ',N'Kg',38,15,N'L3'),
(5,N'Bánh Mì',N'Cái',8,20,N'L2'),
(6,N'Nem',N'Kg',24,25,N'L3')
GO
INSERT INTO [KhuVuc]  VALUES
(1,N'Khu A Tầng  trệt',3),
(2,N'Khu A Tầng trệt',6),
(3,N'Khu A Lầu 2',9),
(4,N'Khu B tầng trệt',5),
(5,N'Khu B lầu 1',10)

GO

INSERT INTO [NhanVien]  VALUES
(1,N'Nguyễn Ngọc',N'Nga',1,2.34,N'NVVP',1),
(2,N'Hà Vĩnh',N'Phát',0,3.67,N'Kế Toán',1),
(3,N'Trần Tuyết',N'Oanh',1,2.67,N'nvvp',2),
(4,N'Nguyễn Kim',N'Ngọc',1,2.9,N'IT',3),
(5,N'Trương Duy',N'Hùng',0,3.1,N'KTV',4),
(6,N'Lương Bá',N'Tháng',0,3.1,N'NVVP',3),
(7,N'Lâm Sơn',N'Hoàng',0,3.67,N'IT',2),
(8,N'Nguyễn Minh',N'Hoồng',0,4.1,N'Kế Toán',3),
(9,N'Vương',N'Ngọc',1,4.1,N'KTV',4)

GO
INSERT INTO [KhachHang]  VALUES
(2,N'Cơ sở dân dụng',N'534 Lê Văn Sỹ P14',N'(058) 8647207'),
(3,N'Công nghệ mới',N'81 Trang Tư',N'(04) 8369254'),
(4,N'Công nghiệp cao su',N'84 Bình Thiên P3',N'(04) 8452178'),
(6,N'SXKD Dịch vụ tổng hợp',N'170 Hậu Giang P6',N'(031) 8631792'),
(7,N'Hóa nhựa Vĩnh Tiến',N'11 vạn tuơng P13',N'(058) 8796540'),
(8,N'Vận tải biển Việt Nam',N'220 Lê Văn Sỹ P14',N'(04) 8654298'),
(9,N'Vạn Thịnh Phát',N'110 Hùng vương P16 Q11',N'(08) 8762059')

GO

INSERT INTO [HoaDon]  VALUES
(10144,'4/10/1999','10/16/1999',4,7),
(10145,'3/11/1999','10/11/1999',6,9),
(10148,'1/14/2000','11/2/2000',7,1),
(10150,'1/17/2000','2/28/2000',2,4)
GO
INSERT INTO [CTDH]  VALUES
(10144,1,35,230),
(10156,5,25,230),
(10145,1,12,230),
(10156,2,25,40),
(10145,2,8,40),
(10144,3,10,10),
(10150,3,20,50)

GO
Create procedure sp_rptCTDH
@SoHD as int
as
if @SoHD=0
select rOW_NUMBER() OVER (order by CTDH.MaMH desc) AS STT, SoHD , TenMH , SoLuong, DGBan , ThanhTien = [SoLuong] * [DGBan]
from CTDH , MatHang
where CTDH.MaMH = MatHang.MaMH
else 
select rOW_NUMBER() OVER (order by CTDH.MaMH desc) AS STT, SoHD , TenMH , SoLuong, DGBan , ThanhTien = [SoLuong] * [DGBan]
from CTDH , MatHang
where CTDH.MaMH = MatHang.MaMH and  SoHD=@SoHD

drop table CTDH
drop table HoaDon
drop table KhachHang
drop table KhuVuc
drop table LoaiHang
drop table MatHang
drop table MatHang2
drop table NhanVien

select * from MatHang
select * from LoaiHang