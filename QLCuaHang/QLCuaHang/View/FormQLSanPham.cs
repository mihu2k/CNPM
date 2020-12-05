using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHang.Model;

namespace QLCuaHang.View
{
    public partial class FormQLSanPham : Form
    {
        private DataTable tblSanPham;

        BindingSource menuList = new BindingSource();

        private string connectionSTR = @"Data Source = .\SQLEXPRESS; Initial Catalog = QLCuaHangTienLoi; Integrated Security = True";
        public FormQLSanPham()
        {
            InitializeComponent();
        }

        private void FormQLSanPham_Load(object sender, EventArgs e)
        {
            ConnectToSQL.Connect();
            dtgvSanPham.DataSource = menuList;
            LoadData();
            // Dùng Data Binding để hiện dữ liệu trên cái textbox
            AddMenuBinding();
        }
        #region events
        private void btnXemSP_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            string id = txbIDSP.Text;
            string name = txbTenSP.Text;
            string unit = txbDVT.Text;
            int amount = (int)nmrSoLuong.Value;
            int price = (int)nmrGia.Value;

            if (InsertMenu(id, name, unit, amount, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món");
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            string id = txbIDSP.Text;
            string name = txbTenSP.Text;
            string unit = txbDVT.Text;
            int amount = (int)nmrSoLuong.Value;
            int price = (int)nmrGia.Value;

            if (UpdateMenu(id, name, unit, amount, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            string id = txbIDSP.Text;

            if (DeleteMenu(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa");
            }
        }

        private void btnTimSP_Click(object sender, EventArgs e)
        {
            SearchFoodByName(txbTimTenSP.Text);
        }
        #endregion
        #region methods
        public void SearchFoodByName(string name)
        {
            string query = string.Format("SELECT * FROM tbSanPham WHERE dbo.convertToUnsign(TenSP) LIKE N'%' + dbo.GetUnsignString(N'{0}') +'%'", name);
            tblSanPham = ConnectToSQL.GetData(query);
            menuList.DataSource = tblSanPham;
            dtgvSanPham.AllowUserToAddRows = false;
            dtgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        public void LoadData()
        {
            string query = "SELECT * FROM tbSanPham";
            tblSanPham = ConnectToSQL.GetData(query);
            menuList.DataSource = tblSanPham;
            dtgvSanPham.AllowUserToAddRows = false;
            dtgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        void AddMenuBinding()
        {
            txbTenSP.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "TenSP", true, DataSourceUpdateMode.Never));
            txbIDSP.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "MaSP", true, DataSourceUpdateMode.Never));
            txbDVT.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "DonViTinh", true, DataSourceUpdateMode.Never));
            nmrSoLuong.DataBindings.Add(new Binding("Text", dtgvSanPham.DataSource, "SoLuong", true, DataSourceUpdateMode.Never));
            nmrGia.DataBindings.Add(new Binding("Value", dtgvSanPham.DataSource, "Gia", true, DataSourceUpdateMode.Never));
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {

            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
        public bool InsertMenu(string id, string name, string unit, int amount, int price)
        {
            string query = string.Format("insert tbSanPham(MaSP, TenSP, DonViTinh, SoLuong, Gia)values ('{0}', N'{1}', N'{2}', {3}, {4})", id, name, unit, amount, price);
            int result = ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateMenu(string id, string name, string unit, int amount, int price)
        {
            string query = string.Format("update tbSanPham set TenSP = N'{0}', DonViTinh = N'{1}', SoLuong = {2}, Gia = {3} where MaSP = '{4}'", name, unit, amount, price, id);
            int result = ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteMenu(string id)
        {
            string query = string.Format("delete from tbSanPham where MaSP = '{0}'", id);
            int result = ExecuteNonQuery(query);
            return result > 0;
        }


        #endregion

        
    }
}
