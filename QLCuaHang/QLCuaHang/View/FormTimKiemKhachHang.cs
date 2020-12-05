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
            dtGVKhachHang.Columns[2].HeaderText = "Số điện thoại";
            dtGVKhachHang.Columns[3].HeaderText = "Địa chỉ";
            dtGVKhachHang.Columns[4].HeaderText = "CNMD";
            dtGVKhachHang.Columns[0].Width = 50;
            dtGVKhachHang.Columns[1].Width = 150;
            dtGVKhachHang.Columns[2].Width = 100;
            dtGVKhachHang.Columns[3].Width = 150;
            dtGVKhachHang.Columns[4].Width = 100;
            dtGVKhachHang.AllowUserToAddRows = false;
            dtGVKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaKH.Text == "") && (txtTenKH.Text == "") &&
               (txtSDT.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tbKhachHang WHERE 1=1";
            if (txtMaKH.Text != "")
                sql = sql + " AND MaKH Like N'%" + txtMaKH.Text + "%'";
            if (txtTenKH.Text != "")
                sql = sql + " AND TenKhachHang Like N'%" + txtTenKH.Text+ "%'";
            if (txtSDT.Text != "")
                sql = sql + " AND SDT Like N'%" + txtSDT.Text + "%'";
            tbKH = ConnectToSQL.GetData(sql);
            if (tbKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tbKH.Rows.Count + " dữ liệu thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtGVKhachHang.DataSource = tbKH;
            LoadDataGridView();
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dtGVKhachHang.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
