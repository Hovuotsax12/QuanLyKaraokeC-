﻿using QL_KARAOKE.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_KARAOKE
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau(NhanVien nv)
        {
            this.nv = nv;
            InitializeComponent();
        }
        private NhanVien nv;
        private KARAOKE_DatabaseDataContext db;
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            db = new KARAOKE_DatabaseDataContext();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatKhauHienTai.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauHienTai.Select();
                return;
            }
            var tk = db.NhanViens.SingleOrDefault(x => x.Username == nv.Username && x.Password == txtMatKhauHienTai.Text);

            if (tk == null)
            {
                MessageBox.Show("Mật khẩu hiện tại không đúng", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauHienTai.Select();
                return;
            }

            if (string.IsNullOrEmpty(txtMatKhauMoi.Text) || string.IsNullOrEmpty(txtXacNhanMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lablel.Select();
                return;
            }
            if (!txtMatKhauMoi.Text.Equals(txtXacNhanMatKhauMoi.Text))
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lablel.Select();
                return;
            }

            tk.Password = txtMatKhauMoi.Text;
            tk.NguoiCapNhat = nv.Username;
            tk.NgayCapNhat = DateTime.Now;
            db.SubmitChanges();
            MessageBox.Show("Đổi mật khẩu thành công", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
