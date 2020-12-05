using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCuaHang.Model
{
    class ConnectToSQL
    {
        private static SqlConnection conn;

        // Tạo phương thức Connect()
        public static void Connect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = LAPTOP-OFQVM343\SQLEXPRESS01; Initial Catalog = QLCuaHangTienLoi;User Id=sa;Password=123456; Integrated Security = True";

            // Kiểm tra trạng thái
            if (conn.State != ConnectionState.Open) {
                conn.Open();
                MessageBox.Show("Kết nối thành công!");
            } else {
                MessageBox.Show("Kết nối thất bại!");
            }
        }

        // Tạo phương thức Disconnect()
        public static void Disconnect()
        {
            if (conn.State == ConnectionState.Open) {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        // Tạo phương thức lấy dữ liệu
        public static DataTable GetData(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = new SqlCommand();
            sda.SelectCommand.Connection = ConnectToSQL.conn;
            sda.SelectCommand.CommandText = sql;

            DataTable table = new DataTable();
            sda.Fill(table);
            return table;
        }

        // Tạo phương thức thực thi câu lệnh non-query
        public static void RunSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Thực hiện câu lệnh SQL
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
        }

        // Kiểm tra khóa chính
        public static bool CheckPrimaryKey(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            sda.Fill(table);

            if (table.Rows.Count > 0) {
                return true;
            } else {
                return false;
            }
        }
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rea;
            rea = cmd.ExecuteReader();
            while (rea.Read())
                ma = rea.GetValue(0).ToString();
            rea.Close();
            return ma;
        }
    }
}
