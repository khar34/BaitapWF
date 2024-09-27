using System;
using System.Windows.Forms;

namespace BaitapWF
{
    public partial class frmDanhSachNhanVien : Form
    {
        public frmDanhSachNhanVien()
        {
            InitializeComponent();
        }
        public bool IsMsnvExists(string msNV, int excludeRowIndex)
        {
            if (string.IsNullOrEmpty(msNV))
            {
                return false;
            }

            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (row.Index != excludeRowIndex && row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString() == msNV)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmNhanVien frm2 = new frmNhanVien(this);
            if (frm2.ShowDialog() == DialogResult.OK)
            {
                if (IsMsnvExists(frm2.MSNV, -1)) 
                {
                    MessageBox.Show("Mã số nhân viên đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string msNV = frm2.MSNV;
                string hoTen = frm2.HoTen;
                string luongCanBan = frm2.LuongCanBan;
                dgvNhanVien.Rows.Add(msNV, hoTen, luongCanBan);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                string msNV = selectedRow.Cells[0].Value.ToString();
                string hoTen = selectedRow.Cells[1].Value.ToString();
                double luongCanBan = double.Parse(selectedRow.Cells[2].Value.ToString());

                frmNhanVien frm2 = new frmNhanVien(msNV, hoTen, luongCanBan, this, selectedRow.Index);
                frm2.isEditMode = true;

                if (frm2.ShowDialog() == DialogResult.OK)
                {
                    if (frm2.MSNV != msNV && IsMsnvExists(frm2.MSNV, selectedRow.Index))
                    {
                        MessageBox.Show("Mã số nhân viên đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    selectedRow.Cells[0].Value = frm2.MSNV;
                    selectedRow.Cells[1].Value = frm2.HoTen;
                    selectedRow.Cells[2].Value = frm2.LuongCanBan;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
                    {
                        dgvNhanVien.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmDanhSachNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}