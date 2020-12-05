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

namespace QLCuaHang.View
{
    public partial class FormTimKiemKhachHang : Form
    {
        DataTable tbKH;
        public FormTimKiemKhachHang()
        {
            InitializeComponent();
        }

        private void FormTimKiemKhachHang_Load(object sender, EventArgs e)
        {
            ResetValues();
            dtGVKhachHang.DataSource = null;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaKH.Focus();
        }

        private void LoadDataGridView()
        {
            dtGVKhachHang.Columns[0].HeaderText = "Mã KH";
            dtGVKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
            dtGVKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dtGVKhachHang.Columns[3].HeaderText = "Ngày sinh";
            dtGVKhachHang.Columns[4].HeaderText = "CMND";
            dtGVKhachHang.Columns[5].HeaderText = "SĐT";
            dtGVKhachHang.Columns[0].Width = 50;
            dtGVKhachHang.Columns[1].Width = 100;
            dtGVKhachHang.Columns[2].Width = 100;
            dtGVKhachHang.Columns[3].Width = 100;
            dtGVKhachHang.Columns[4].Width = 150;
            dtGVKhachHang.AllowUserToAddRows = false;
            dtGVKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
