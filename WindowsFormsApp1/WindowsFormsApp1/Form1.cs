using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class textBoxName : Form
    {
        public textBoxName()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void l(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string hoVaTen = form1.Text;
            string ngaySinh = textBox1.Text;
            string queQuan = textBox2.Text;
            string lop = textBox3.Text;

            // Tạo thông báo để hiển thị dữ liệu
            string message = $"Họ và tên: {hoVaTen}\n" +
                             $"Ngày sinh: {ngaySinh}\n" +
                             $"Quê quán: {queQuan}\n" +
                             $"Lớp: {lop}";

            // Hiển thị dữ liệu ra hộp thoại MessageBox
            MessageBox.Show(message, "Thông tin đã nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxHoTen_TextChanged(object sender, EventArgs e)
        {
            string fullName = form1.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Họ và tên";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void form1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
