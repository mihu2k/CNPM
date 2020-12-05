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
        DataTable tbHDB; //Bảng chi tiết hoá đơn bán
        public FormTaoHoaDon()
        {
            InitializeComponent();
        }

        private void FormTaoHoaDon_Load(object sender, EventArgs e)
        {
            ConnectToSQL.Connect();
            LoadDataGridView();
            LoadInfoHoaDon();
            btnThemHoaDon.Enabled = true;
            btnLuuHoaDon.Enabled = false;
            btnHuyHoaDon.Enabled = false;
            btnInHoaDon.Enabled = false;
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
        }

        private void dGVCTHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT A.MaSP, B.TenSP, A.SoLuong, B.Gia, A.GiamGia, A.TongTien FROM tbCTHD AS A, tbSanPham AS B WHERE A.MaHD = '" + txtMaHD.Text + "' AND A.MaSP=B.MaSP";
            tbHDB = ConnectToSQL.GetData(sql);
            dGVCTHD.DataSource = tbHDB;
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
    }
}

