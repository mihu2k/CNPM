using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLCuaHang.Model;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLCuaHang.View
{
    public partial class FormTaoHoaDon : Form
    {
        DataTable tbHD; //Bảng chi tiết hoá đơn bán
        public FormTaoHoaDon()
        {
            InitializeComponent();
        }

        private void FormTaoHoaDon_Load(object sender, EventArgs e)
        {
            ConnectToSQL.Connect();
            LoadDataGridView();
            LoadInfoHoaDon();

            txtMaHD.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSdt.ReadOnly = true;
            txtTenMH.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongThanhTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            if (txtTongThanhTien.Text == "")
                txtTongThanhTien.Text = "0";

            btnThemHoaDon.Enabled = true;
            btnLuuHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = true;
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT A.MaSP, B.TenSP, A.SoLuong, B.Gia, A.GiamGia, A.TongTien FROM tbCTHD AS A, tbSanPham AS B WHERE A.MaHD = '" + txtMaHD.Text + "' AND A.MaSP=B.MaSP";
            tbHD = ConnectToSQL.GetData(sql);
            dGVCTHD.DataSource = tbHD;
            dGVCTHD.Columns[0].HeaderText = "Mã hàng";
            dGVCTHD.Columns[1].HeaderText = "Tên hàng";
            dGVCTHD.Columns[2].HeaderText = "Số lượng";
            dGVCTHD.Columns[3].HeaderText = "Đơn giá";
            dGVCTHD.Columns[4].HeaderText = "Giảm giá %";
            dGVCTHD.Columns[5].HeaderText = "Thành tiền";
            dGVCTHD.Columns[0].Width = 80;
            dGVCTHD.Columns[1].Width = 130;
            dGVCTHD.Columns[2].Width = 80;
            dGVCTHD.Columns[3].Width = 90;
            dGVCTHD.Columns[4].Width = 90;
            dGVCTHD.Columns[5].Width = 90;
            dGVCTHD.AllowUserToAddRows = false;
            dGVCTHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT MaNV FROM tbHoaDon WHERE MaHD = '" + txtMaHD.Text + "'";
            coBMaNV.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT MaKH FROM tbHoaDon WHERE MaHD = '" + txtMaHD.Text + "'";
            txtMaKH.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT TenNhanVien FROM tbNhanVien WHERE MaNV = '" + coBMaNV.Text + "'";
            txtTenNV.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT TenKhachHang FROM tbKhachHang WHERE MaKH = '" + txtMaKH.Text + "'";
            txtTenKH.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT DiaChi FROM tbKhachHang WHERE MaKH = '" + txtMaKH.Text + "'";
            txtDiaChi.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT SDT FROM tbKhachHang WHERE MaKH = '" + txtMaKH.Text + "'";
            txtSdt.Text = ConnectToSQL.GetFieldValues(str);
            str = "SELECT TongThanhTien FROM tbHoaDon WHERE MaHD = '" + txtMaHD.Text + "'";
            txtTongThanhTien.Text = ConnectToSQL.GetFieldValues(str);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tbThongTinChung, tbThongTinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z200"].Font.Name = "Times new roman";
            exRange.Range["A2:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:A1"].Font.Size = 20;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Smart";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Lê Văn Lương";
            exRange.Range["C2:E2"].Font.Size = 20;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (+84)456789012";
            

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT A.MaHD, A.NgayLap, A.TongThanhTien, B.TenKhachHang, B.DiaChi, B.SDT, C.TenNhanVien FROM tbHoaDon AS A, tbKhachHang AS B, tbNhanVien AS C WHERE A.MaHD = N'" + txtMaHD.Text + "' AND A.MaKH = B.MaKH AND A.MaNV = C.MaNV";
            tbThongTinChung = ConnectToSQL.GetData(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tbThongTinChung.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tbThongTinChung.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tbThongTinChung.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tbThongTinChung.Rows[0][5].ToString();

            //Lấy thông tin các sản phẩm
            sql = "SELECT B.MaSP,B.TenSP, A.SoLuong, B.Gia, A.GiamGia, A.TongTien " + "FROM tbCTHD AS A , tbSanPham AS B WHERE A.MaHD = N'" + txtMaHD.Text + "' AND A.MaSP = B.MaSP";
            tbThongTinHang = ConnectToSQL.GetData(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Mã SP";
            exRange.Range["C11:C11"].Value = "Tên SP";
            exRange.Range["D11:D11"].Value = "Số lượng";
            exRange.Range["E11:E11"].Value = "Đơn giá";
            exRange.Range["F11:F11"].Value = "Giảm giá";
            exRange.Range["G11:G11"].Value = "Thành tiền";

            for (hang = 0; hang < tbThongTinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tbThongTinHang.Columns.Count; cot++)
                //Điền thông tin sản phẩm
                {
                    exSheet.Cells[cot + 2][hang + 12] = tbThongTinHang.Rows[hang][cot].ToString();
                    if (cot == 4) exSheet.Cells[cot + 2][hang + 12] = tbThongTinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng TT:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tbThongTinChung.Rows[0][2].ToString();

            exRange = exSheet.Cells[4][hang + 17]; 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tbThongTinChung.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Nhà Bè, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tbThongTinChung.Rows[0][6];
            exSheet.Name = "Hóa đơn";
            exApp.Visible = true;
        }
    }
}

