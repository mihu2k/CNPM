using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using QLCuaHang.Model;

namespace QLCuaHang.View
{
    public partial class FormNhanVien : Form
    {
        private DataTable tblNhanVien;

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            ConnectToSQL.Connect();
            LoadData();

            // Không thể thực hiện lưu hoặc xóa khi mới load form
            txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        public void LoadData()
        {
            string sql = "select MaNV, TenNhanVien, GioiTinh, DiaChi, SDT, NgaySinh, MatKhau from tbNhanVien";
            tblNhanVien = ConnectToSQL.GetData(sql);
            dgvNhanVien.DataSource = tblNhanVien;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvNhanVien.Columns["MatKhau"].Visible = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearValues();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaNhanVien.Enabled = false;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false) {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }

            if (tblNhanVien.Rows.Count == 0) {
                MessageBox.Show("Chưa có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiện dữ liệu trên các ô text box khi click vào dgv
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells["SDT"].Value.ToString();
            dtmNgaySinh.Text = dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtMatKhauNV.Text = dgvNhanVien.CurrentRow.Cells["MatKhau"].Value.ToString();

            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") {
                chkGioiTinh.Checked = true;
            } else {
                chkGioiTinh.Checked = false;
            }

            // Kích hoạt nút xóa, hủy, sửa
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
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();

            // clear data
            ClearValues();
        }

        private void ClearValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            chkGioiTinh.Checked = false;
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtMatKhauNV.Text = "";
            dtmNgaySinh.Value = DateTime.Now.Date;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gender;

            // Kiểm tra thông tin
            if (txtMaNhanVien.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }

            if (txtTenNhanVien.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }

            if (txtDiaChi.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (txtDienThoai.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (txtMatKhauNV.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa điền mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauNV.Focus();
                return;
            }

            if (chkGioiTinh.Checked == true) {
                gender = "Nam";
            } else {
                gender = "Nữ";
            }

            // Kiểm tra mã nhân viên nhập vào
            sql = "select MaNV from tbNhanVien where MaNV = '"+txtMaNhanVien.Text.Trim()+"'";

            if (ConnectToSQL.CheckPrimaryKey(sql)) {
                MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNhanVien.Focus();
                txtMaNhanVien.Text = "";
                return;
            }

            // Lưu thông tin nhân viên vừa tạo
            sql = "insert into tbNhanVien values('"+txtMaNhanVien.Text.Trim()+"', N'"+txtTenNhanVien.Text.Trim()+"', N'"+gender+"', N'"+txtDiaChi.Text.Trim()+"', '"+txtDienThoai.Text.Trim()+ "', '" + dtmNgaySinh.Value + "', '" +txtMatKhauNV.Text.Trim()+ "')";
            ConnectToSQL.RunSQL(sql);
            LoadData();
            ClearValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gender;

            // Kiểm tra thông tin
            if (tblNhanVien.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaNhanVien.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa chọn nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }

            if (txtTenNhanVien.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }

            if (txtDiaChi.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (txtDienThoai.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (chkGioiTinh.Checked == true) {
                gender = "Nam";
            }
            else {
                gender = "Nữ";
            }

            if (txtMatKhauNV.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa điền mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauNV.Focus();
                return;
            }

            // Update thông tin nhân viên
            sql = "update tbNhanVien set TenNhanVien = N'" + txtTenNhanVien.Text.Trim() + "', " +
                                        "GioiTinh = N'" + gender + "', " +
                                        "DiaChi = N'" + txtDiaChi.Text.Trim() + "', " +
                                        "SDT = '" + txtDienThoai.Text.Trim() + "', " +
                                        "NgaySinh = '" + dtmNgaySinh.Value + "', " +
                                        "MatKhau = '" + txtMatKhauNV.Text.Trim() + "'" +
                                        "where MaNV = '" + txtMaNhanVien.Text.Trim() + "'";
            ConnectToSQL.RunSQL(sql);
            LoadData();
            ClearValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            // Kiểm tra thông tin
            if (tblNhanVien.Rows.Count == 0) {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaNhanVien.Text.Trim() == "") {
                MessageBox.Show("Bạn chưa chọn nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show dialog
            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                sql = "delete tbNhanVien where MaNV = '" + txtMaNhanVien.Text + "'";
                ConnectToSQL.RunSQL(sql);
                LoadData();
                ClearValues();
            }
        }
    }
}
