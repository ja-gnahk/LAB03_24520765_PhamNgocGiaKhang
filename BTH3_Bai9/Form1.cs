using System;
using System.Windows.Forms;

namespace Bai9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstMonHoc.SelectedItem == null)
                return;

            string mh = lstMonHoc.SelectedItem.ToString();
            lstDaChon.Items.Add(mh);
            lstMonHoc.Items.Remove(mh);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstDaChon.SelectedItem == null)
                return;

            string mh = lstDaChon.SelectedItem.ToString();
            lstMonHoc.Items.Add(mh);
            lstDaChon.Items.Remove(mh);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Validate dữ liệu (Giữ nguyên)
            if (txtMSSV.Text.Trim() == "" ||
                txtHoTen.Text.Trim() == "" ||
                cboChuyenNganh.SelectedIndex < 0 ||
                (!ckNam.Checked && !ckNu.Checked))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ckNam.Checked && ckNu.Checked)
            {
                MessageBox.Show("Chỉ được chọn một giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mssv = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string chuyenNganh = cboChuyenNganh.Text;
            string gioiTinh = ckNam.Checked ? "Nam" : "Nữ";
            int soMon = lstDaChon.Items.Count; // Lấy số lượng môn

            // 2. Kiểm tra trùng MSSV để Cập nhật (Sửa logic duyệt DataGridView)
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                // Bỏ qua dòng trống cuối cùng nếu có
                if (row.IsNewRow) continue;

                // Kiểm tra cột MSSV (Giả sử là cột đầu tiên - Index 0)
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == mssv)
                {
                    // Cập nhật lại các ô
                    row.Cells[1].Value = hoTen;
                    row.Cells[2].Value = chuyenNganh;
                    row.Cells[3].Value = gioiTinh;
                    row.Cells[4].Value = soMon;

                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo");
                    return; // Thoát luôn sau khi cập nhật
                }
            }

            // 3. Nếu không trùng thì Thêm mới vào DataGridView
            // Thứ tự thêm phải khớp với thứ tự cột bạn tạo: MSSV, Họ Tên, Chuyên Ngành, Giới Tính, Số Môn
            dgvSinhVien.Rows.Add(mssv, hoTen, chuyenNganh, gioiTinh, soMon);

            MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            cboChuyenNganh.SelectedIndex = -1;
            ckNam.Checked = false;
            ckNu.Checked = false;

            foreach (var item in lstDaChon.Items)
                lstMonHoc.Items.Add(item);

            lstDaChon.Items.Clear();
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgvSinhVien.CurrentRow.IsNewRow) return;

            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMSSV.Text = row.Cells["colMSSV"].Value.ToString();
            txtHoTen.Text = row.Cells["colHoTen"].Value.ToString();
            cboChuyenNganh.Text = row.Cells["colChuyenNganh"].Value.ToString();

            string gt = row.Cells["colGioiTinh"].Value.ToString();
            ckNam.Checked = (gt == "Nam");
            ckNu.Checked = (gt == "Nữ");
        }
    }
}