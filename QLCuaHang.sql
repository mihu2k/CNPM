create database QLCuaHangTienLoi
go
use QLCuaHangTienLoi
go
create table tbNhanVien(
	MaNV varchar(20) primary key NOT NULL,
	TenNhanVien nvarchar(50) NOT NULL,
	GioiTinh nvarchar(5) NOT NULL,
	DiaChi nvarchar(100) NOT NULL,
	SDT varchar(15) NOT NULL,
	NgaySinh Datetime NOT NULL,
	MatKhau varchar(20) NOT NULL,
)
go
create table tbKhachHang(
	MaKH varchar(20) primary key not null,
	TenKhachHang nvarchar(50) not null,
	SDT varchar(15),
	DiaChi nvarchar(100),
	CMND varchar(10) not null
)
go
create table tbSanPham(
	MaSP varchar(20) primary key not null,
	TenSP nvarchar(50) not null,
	DonViTinh nvarchar(50),
	SoLuong int,
	Gia int not null,
	NgayNhap date,
	NgayHetHan date,
)
go
create table tbHoaDon(
	MaHD varchar(20) primary key not null,
	MaNV varchar(20) not null,
	MaKH varchar(20) not null,
	NgayLap date,
	TongThanhTien float default 0,
	foreign key (MaNV) references tbNhanVien(MaNV),
	foreign key (MaKH) references tbKhachHang(MaKH)
)
go
create table tbCTHD(
	MaHD varchar(20) not null,
	MaSP varchar(20) not null,
	primary key(MaHD, MaSP),
	TenSP nvarchar(50),
	SoLuong int,
	DonGia int,
	GiamGia int,
	TongTien int,
	foreign key (MaHD) references tbHoaDon(MaHD),
	foreign key (MaSP) references tbSanPham(MaSP)
)
go
create table tbNhaCungCap(
	MaNCC varchar(20) not null primary key,
	TenNCC nvarchar(50)
)
go
create table tbDonDatHang(
	MaDDH varchar(20) primary key not null,
	MaNV varchar(20) not null,
	MaNCC varchar(20) not null,
	NgayLap date,
	foreign key (MaNV) references tbNhanVien(MaNV),
	foreign key (MaNCC) references tbNhaCungCap(MaNCC)
)
go
create table tbCTDDH(
	MaSP varchar(20) not null,
	MaDDH varchar(20) not null,
	primary key(MaSP, MaDDH),
	TenSP nvarchar(50) not null,
	SoLuong int,
	DonViTinh nvarchar(50),
	foreign key (MaSP) references tbSanPham(MaSP),
	foreign key (MaDDH) references tbDonDatHang(MaDDH)
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

insert into tbSanPham values('SP001', N'Cà phê phin', N'Ly', 100, 20000, '12/12/2019', '01/05/2020')
insert into tbSanPham values('SP002', N'Trà chanh', N'Ly', 100, 15000, '12/10/2019', '01/12/2020')
insert into tbSanPham values('SP003', N'Kem', N'Cây', 200, 10000, '11/11/2019', '01/15/2020')
insert into tbSanPham values('SP004', N'Sandwich', N'Cái', 40, 30000, '06/06/2020', '06/24/2020')
insert into tbSanPham values('SP005', N'Bánh bao', N'Cái', 30, 20000, '09/10/2020', '09/11/2020')
insert into tbSanPham values('SP006', N'Dimsum', N'Cái', 50, 10000, '10/10/2020', '11/11/2020')
insert into tbSanPham values('SP007', N'Xúc xích', N'Cái', 100, 10000, '10/10/2020', '10/11/2020')
insert into tbSanPham values('SP008', N'Milo', N'Ly', 100, 10000, '01/01/2020', '01/02/2020')
insert into tbSanPham values('SP009', N'Froster', N'Ly', 100, 10000, '01/01/2020', '01/02/2020')
insert into tbSanPham values('SP010', N'Trà đào', N'Ly', 80, 10000, '06/05/2020', '07/05/2020')

insert into tbKhachHang values('KH000', N'User Name', null, null, 'Không')
insert into tbHoaDon values('HD001','TN001','KH000','12/13/2019',35000)
insert into tbHoaDon values('HD002','TN001','KH001','12/13/2019',50000)
insert into tbHoaDon values('HD003','TN002','KH000','12/13/2019',30000)

insert into tbCTHD values('HD001', 'SP001', N'Cà phê phin',1,20000,0,20000)
insert into tbCTHD values('HD001', 'SP002', N'Trà chanh',1,15000,0,15000)
insert into tbCTHD values('HD002', 'SP004', N'Sandwich',1,30000,0,30000)
insert into tbCTHD values('HD002', 'SP005', N'Bánh Bao',1,20000,0,20000)
insert into tbCTHD values('HD003', 'SP009', N'Cà phê phin',2,20000,0,20000)
insert into tbCTHD values('HD003', 'SP010', N'Trà chanh',1,10000,0,10000)
go

CREATE FUNCTION [dbo].[GetUnsignString](@strInput NVARCHAR(4000)) 
RETURNS NVARCHAR(4000)
AS
BEGIN     
    IF @strInput IS NULL RETURN @strInput
    IF @strInput = '' RETURN @strInput
    DECLARE @RT NVARCHAR(4000)
    DECLARE @SIGN_CHARS NCHAR(136)
    DECLARE @UNSIGN_CHARS NCHAR (136)

    SET @SIGN_CHARS       = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ'+NCHAR(272)+ NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'

    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
    SET @COUNTER = 1
 
    WHILE (@COUNTER <=LEN(@strInput))
    BEGIN   
      SET @COUNTER1 = 1
      --Tim trong chuoi mau
       WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1)
       BEGIN
     IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
     BEGIN           
          IF @COUNTER=1
              SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)                   
          ELSE
              SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)    
              BREAK         
               END
             SET @COUNTER1 = @COUNTER1 +1
       END
      --Tim tiep
       SET @COUNTER = @COUNTER +1
    END
    RETURN @strInput
END
go



