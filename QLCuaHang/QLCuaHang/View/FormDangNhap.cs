using QLCuaHang.View;
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

namespace QLCuaHang
{
    public partial class FormDangNhap : Form
    {
        ConnectToSQL con = new ConnectToSQL();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            Model.ConnectToSQL.Connect();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Model.ConnectToSQL.Disconnect();
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = ConnectToSQL.GetData("select * from tbNhanVien where MaNV = '" +txtUsername.Text+ "' and MatKhau = '" +txtPassword.Text+ "'");
            
            if (table.Rows.Count > 0) {
                MessageBox.Show("Đăng nhập thành công!", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang FormMain
                FormMain frmMain = new FormMain(table.Rows[0][0].ToString(), table.Rows[0][6].ToString());
                this.Hide();
                frmMain.ShowDialog();
            } else {
                MessageBox.Show("Đăng nhập thất bại!", "Xin lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
