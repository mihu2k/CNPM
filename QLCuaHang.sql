create database QLCuaHangTienLoi
go
use QLCuaHangTienLoi

create table tbNhanVien(
	MaNV varchar(20) primary key NOT NULL,
	TenNhanVien nvarchar(50) NOT NULL,
	GioiTinh nvarchar(5) NOT NULL,
	DiaChi nvarchar(100) NOT NULL,
	SDT varchar(15) NOT NULL,
	NgaySinh Datetime NOT NULL,
	MatKhau varchar(20) NOT NULL,
)

create table tbKhachHang(
	MaKH varchar(20) primary key not null,
	TenKhachHang nvarchar(50) not null,
	SDT varchar(15),
	DiaChi nvarchar(100),
	CMND varchar(10) not null
)

insert into tbKhachHang values('KH001', N'Trương Minh Quang', '0933244123', N'200 Lê Văn Lương', '366244123')
insert into tbKhachHang values('KH002', N'Trương Minh Hưng', '0334066112', N'19 Lê Hồng Phong', '388573957')
insert into tbKhachHang values('KH003', N'Nguyễn Văn Đạt', '0856376476', N'26 CMT8', '288538673')

insert into tbNhanVien values('TN001', N'Nguyễn Văn A', N'Nam', N'98 Trần Phú', '0123456789', '7/21/1990', '123456')
insert into tbNhanVien values('TN002', N'Nguyễn Thị B', N'Nữ', N'8 Trần Phú', '0123456788', '5/7/1990', '123456')
insert into tbNhanVien values('QL001', N'Nguyễn Văn An', N'Nam', N'9 Trần Phú', '0123456787', '5/9/1990', '123456')
insert into tbNhanVien values('QL002', N'Nguyễn Văn C', N'Nam', N'9 Trần Phú', '0123456787', '5/9/1990', '123456')
insert into tbNhanVien values('KO002', N'Nguyễn Thị D', N'Nữ', N'90 Trần Phú', '0123456786', '5/10/1990', '123456')
insert into tbNhanVien values('KO001', N'Nguyễn Thị Dược', N'Nữ', N'90 Trần Phú', '0123456786', '5/10/1990', '123456')