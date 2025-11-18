using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThemCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoTK.Text) ||
                string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtSoTien.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoTien.Text, out decimal soTien))
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soTK = txtSoTK.Text.Trim();
            if (!long.TryParse(soTK, out _))
            {
                MessageBox.Show("Số tài khoản chỉ được chứa các ký tự số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTK.Focus(); // Cho phép người dùng chỉnh sửa ngay
                return;
            }
            bool tonTai = false;

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[1].Text == soTK)
                {
                    item.SubItems[2].Text = txtTenKH.Text;
                    item.SubItems[3].Text = txtDiaChi.Text;
                    item.SubItems[4].Text = soTien.ToString();

                    tonTai = true;
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }

            if (!tonTai)
            {
                ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());
                item.SubItems.Add(soTK);
                item.SubItems.Add(txtTenKH.Text);
                item.SubItems.Add(txtDiaChi.Text);
                item.SubItems.Add(soTien.ToString());

                listView1.Items.Add(item);

                MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            TinhTongTien();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            ListViewItem item = listView1.SelectedItems[0];

            txtSoTK.Text = item.SubItems[1].Text;
            txtTenKH.Text = item.SubItems[2].Text;
            txtDiaChi.Text = item.SubItems[3].Text;
            txtSoTien.Text = item.SubItems[4].Text;
        }

        private void TinhTongTien()
        {
            decimal tong = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                if (decimal.TryParse(item.SubItems[4].Text, out decimal st))
                    tong += st;
            }

            txtTongTien.Text = tong.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string soTK = txtSoTK.Text.Trim();
            if (string.IsNullOrEmpty(soTK))
            {
                MessageBox.Show("Vui lòng nhập số tài khoản cần xóa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[1].Text == soTK)
                {
                    DialogResult dr = MessageBox.Show(
                        "Bạn có chắc muốn xóa tài khoản này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        listView1.Items.Remove(item);
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        for (int i = 0; i < listView1.Items.Count; i++)
                            listView1.Items[i].SubItems[0].Text = (i + 1).ToString();

                        TinhTongTien();
                    }
                    return;
                }
            }

            MessageBox.Show("Không tìm thấy số tài khoản cần xóa!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                                "Bạn có chắc muốn thoát chương trình?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
