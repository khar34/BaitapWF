using System;
using System.Windows.Forms;

namespace BaitapWF
{
    public partial class frmNhanVien : Form
    {
        public string MSNV { get; set; }
        public string HoTen { get; set; }
        public string LuongCanBan { get; set; }
        public bool isEditMode { get; set; } = false;

        private frmDanhSachNhanVien frmForm1;
        private int rowIndex;
        private string originalMsnv;

        public frmNhanVien(frmDanhSachNhanVien frmForm1)
        {
            InitializeComponent();
            this.frmForm1 = frmForm1;
        }

        public frmNhanVien(string MSNV, string HoTen, double LuongCanBan, frmDanhSachNhanVien frmForm1, int rowIndex)
        {
            InitializeComponent();
            this.MSNV = MSNV;
            this.HoTen = HoTen;
            this.LuongCanBan = LuongCanBan.ToString();
            this.frmForm1 = frmForm1;
            this.rowIndex = rowIndex;
            this.originalMsnv = MSNV;

            txtMSNV.Text = MSNV;
            txtTenNV.Text = HoTen;
            txtLuongCB.Text = LuongCanBan.ToString();
        }

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidSalary(string salary)
        {
            return double.TryParse(salary, out _);
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSNV.Text))
            {
                MessageBox.Show("Mã số nhân viên không được để trống. Vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMSNV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Tên nhân viên không được để trống. Vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLuongCB.Text))
            {
                MessageBox.Show("Lương cơ bản không được để trống. Vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLuongCB.Focus();
                return;
            }

            if (!IsValidName(txtTenNV.Text))
            {
                MessageBox.Show("Tên chỉ được chứa các ký tự chữ. Vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNV.Clear();
                txtTenNV.Focus();
                return;
            }

            if (!IsValidSalary(txtLuongCB.Text))
            {
                MessageBox.Show("Lương cơ bản phải là số. Vui lòng nhập lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLuongCB.Clear();
                txtLuongCB.Focus();
                return;
            }

            // Kiểm tra nếu mã số nhân viên đã tồn tại và không phải là mã gốc
            if (txtMSNV.Text != originalMsnv && frmForm1.IsMsnvExists(txtMSNV.Text, rowIndex))
            {
                MessageBox.Show("Mã số nhân viên đã tồn tại. Vui lòng nhập mã số khác.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMSNV.Text = originalMsnv; // Khôi phục mã gốc
                txtMSNV.Focus(); // Đặt lại con trỏ vào ô MSNV
                return; // Không đóng form
            }

            // Nếu không có lỗi, gán giá trị
            MSNV = txtMSNV.Text;
            HoTen = txtTenNV.Text;
            LuongCanBan = txtLuongCB.Text;
            DialogResult = DialogResult.OK; // Chỉ đặt DialogResult nếu không có lỗi
            Close(); // Đóng form
        }
        private void btnBoQua_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }


}
