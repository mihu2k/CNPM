using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHang.Model;

namespace QLCuaHang.View
{
    public partial class FormQLKhachHang : Form
    {
        private DataTable tblKhachHang;

        public FormQLKhachHang()
        {
            InitializeComponent();
        }

        private void FormQLKhachHang_Load(object sender, EventArgs e)
        {
            ConnectToSQL.Connect();
            LoadData();

            // Không thể thực hiện lưu hoặc xóa khi mới load form
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        public void LoadData()
        {
            string sql = "select * from tbKhachHang";
            tblKhachHang = ConnectToSQL.GetData(sql);
            dgvKhachHang.DataSource = tblKhachHang;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearValues();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaKH.Enabled = false;
        }

        private void ClearValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtDienThoaiKH.Text = "";
            txtCMNDKH.Text = "";
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false) {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }

            if (tblKhachHang.Rows.Count == 0) {
                MessageBox.Show("Chưa có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiện dữ liệu trên các ô text box khi click vào dgv
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKhachHang"].Value.ToString();
            txtDiaChiKH.Text = dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoaiKH.Text = dgvKhachHang.CurrentRow.Cells["SDT"].Value.ToString();
            txtCMNDKH.Text = dgvKhachHang.CurrentRow.Cells["CMND"].Value.ToString();

            // Kích hoạt nút xóa, hủy sửa
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaKH.Enabled = true;
            txtMaKH.Focus();

            // clear data
            ClearValues();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;

            // Kiểm tra thông tin
            if (tblKhachHang.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa chọn khách hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (txtTenKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }

            if (txtDiaChiKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiKH.Focus();
                return;
            }

            if (txtDienThoaiKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoaiKH.Focus();
                return;
            }

            if (txtCMNDKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập CMND!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMNDKH.Focus();
                return;
            }

            // Update thông tin khách hàng
            sql = "update tbKhachHang set TenKhachHang = N'" + txtTenKH.Text.Trim() + "'," +
                                        "SDT = '" + txtDienThoaiKH.Text.Trim() + "'," +
                                        "DiaChi = N'" + txtDiaChiKH.Text.Trim() + "'," +
                                        "CMND = '" + txtCMNDKH.Text.Trim() + "'" +
                                        "where MaKH = '" + txtMaKH.Text.Trim() + "'";
            ConnectToSQL.RunSQL(sql);
            LoadData();
            ClearValues();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            // Kiểm tra thông tin
            if (txtMaKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (txtTenKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }

            if (txtDiaChiKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiKH.Focus();
                return;
            }

            if (txtDienThoaiKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoaiKH.Focus();
                return;
            }

            if (txtCMNDKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập CMND!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMNDKH.Focus();
                return;
            }

            // Kiểm tra mã khách hàng nhập vào
            sql = "select MaKH from tbKhachHang where MaKH = '" + txtMaKH.Text.Trim() + "'";

            if (ConnectToSQL.CheckPrimaryKey(sql)) {
                MessageBox.Show("Mã khách hàng đã tồn tại, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                txtMaKH.Text = "";
                return;
            }

            // Lưu thông tin khách hàng vừa tạo
            sql = "insert into tbKhachHang values('" + txtMaKH.Text.Trim() + "', N'" + txtTenKH.Text.Trim() + "', '" + txtDienThoaiKH.Text.Trim() + "', N'" + txtDiaChiKH.Text.Trim() + "', '" + txtCMNDKH.Text + "')";
            ConnectToSQL.RunSQL(sql);
            LoadData();
            ClearValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtMaKH.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            // Kiểm tra thông tin
            if (tblKhachHang.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaKH.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa chọn khách hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show dialog
            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                sql = "delete tbKhachHang where MaKH = '" + txtMaKH.Text + "'";
                ConnectToSQL.RunSQL(sql);
                LoadData();
                ClearValues();
            }
        }
    }
}