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
    public partial class FormTimKiemHoaDon : Form
    {
        DataTable tbHD;
        public FormTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHD.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNV.Text == "") && (txtMaKH.Text == "") &&
               (txtTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tbHoaDon WHERE 1=1";
            if (txtMaHD.Text != "")
                sql = sql + " AND MaHD Like N'%" + txtMaHD.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayLap) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayLap) =" + txtNam.Text;
            if (txtMaNV.Text != "")
                sql = sql + " AND MaNV Like N'%" + txtMaNV.Text + "%'";
            if (txtMaKH.Text != "")
                sql = sql + " AND MaKH Like N'%" + txtMaKH.Text + "%'";
            if (txtTongTien.Text != "")
                sql = sql + " AND TongThanhTien <=" + txtTongTien.Text;
            tbHD = ConnectToSQL.GetData(sql);
            if (tbHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tbHD.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dtGVHoaDon.DataSource = tbHD;
            LoadDataGridView();
        }

        private void FormTimKiemHoaDon_Load(object sender, EventArgs e)
        {
            ResetValues();
            dtGVHoaDon.DataSource = null;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHD.Focus();
        }

        private void LoadDataGridView()
        {
            dtGVHoaDon.Columns[0].HeaderText = "Mã HĐ";
            dtGVHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dtGVHoaDon.Columns[2].HeaderText = "Mã Khách";
            dtGVHoaDon.Columns[3].HeaderText = "Ngày bán";
            dtGVHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dtGVHoaDon.Columns[0].Width = 50;
            dtGVHoaDon.Columns[1].Width = 100;
            dtGVHoaDon.Columns[2].Width = 100;
            dtGVHoaDon.Columns[3].Width = 100;
            dtGVHoaDon.Columns[4].Width = 150;
            dtGVHoaDon.AllowUserToAddRows = false;
            dtGVHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dtGVHoaDon.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtGVHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dtGVHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();
                FormTaoHoaDon frm = new FormTaoHoaDon();
                frm.txtMaHD.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }
    }
}
