using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace canifa
{
    public partial class hangravao : UserControl
    {
        ham ham = new ham();
        data dulieu = new data();
        static string ngaymuontim = "";
        string ngayhomnay = DateTime.Now.ToString("ddMMyyyy");
        public hangravao()
        {
            InitializeComponent();
            lbthongbao.Text = "Hello, scan bình thường, muốn xem dữ liệu các ngày trước thì chọn ngày tại bảng lịch bên trên";
            txtbarcodera.Focus();
        }

        
        private void pbpauseamthanh_Click(object sender, EventArgs e)
        {
            try
            {
                ham.dungphat();
                pbpauseamthanh.Visible = false;
                txtbarcodera.Enabled = true;
                txtbarcodevao.Enabled = true;
                txtbarcodera.Clear();
                txtbarcodevao.Clear();
                lbthongbao.Text = "Tiếp tục";
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
            
        }

        public void loadtatca()
        {
            try
            {
                datag12h.DataSource = dulieu.hangra12h(ngaymuontim);
                datag15h.DataSource = dulieu.hangra15h(ngaymuontim);
                datag18h.DataSource = dulieu.hangra18h(ngaymuontim);
                datag22h.DataSource = dulieu.hangra22h(ngaymuontim);
                datagvaokho.DataSource = dulieu.hangvaokho(ngaymuontim);
                lbsoluong12h.Text = dulieu.tongra12h(ngaymuontim);
                lbsoluong15h.Text = dulieu.tongra15h(ngaymuontim);
                lbsoluong18h.Text = dulieu.tongra18h(ngaymuontim);
                lbsoluong22h.Text = dulieu.tongra22h(ngaymuontim);
                lbsoluongvaokho.Text = dulieu.tonghangvaotrongngay(ngaymuontim);
                lbtongsoluongvao.Text = lbsoluongvaokho.Text;
                lbtongsoluongra.Text = dulieu.tonghangratrongngay(ngaymuontim);
                txtbarcodevao.Clear();
                txtbarcodera.Clear();
                datagvaokho.FirstDisplayedScrollingRowIndex = datagvaokho.RowCount - 1;
                datag12h.FirstDisplayedScrollingRowIndex = datag12h.RowCount - 1;
                datag15h.FirstDisplayedScrollingRowIndex = datag15h.RowCount - 1;
                datag18h.FirstDisplayedScrollingRowIndex = datag18h.RowCount - 1;
                datag22h.FirstDisplayedScrollingRowIndex = datag22h.RowCount - 1;
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
           
        }
        private void txtbarcodera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtbarcodera.Text))
                {
                    try
                    {
                        if (dulieu.laymasp(txtbarcodera.Text) == null)
                        {
                            ham.phatloi();
                            txtbarcodera.Enabled = false;
                            pbpauseamthanh.Visible = true;
                            lbthongbao.Text = "Có lỗi scan - xem lại đi.";
                        }
                        else
                        {
                            ngaymuontim = ngayhomnay;
                            lbmasp.Text = dulieu.laymasp(txtbarcodera.Text);
                            dulieu.inserthangra(txtbarcodera.Text, lbmasp.Text);
                            loadtatca();
                            txtbarcodera.Focus();
                            lbthongbao.Text = "Go on";
                        }
                    }
                    catch (Exception)
                    {

                        lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
                    }
                    
                }
            }
        }

        private void txtbarcodevao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtbarcodevao.Text))
                {
                    try
                    {
                        if (dulieu.laymasp(txtbarcodevao.Text) == null)
                        {
                            ham.phatloi();
                            txtbarcodevao.Enabled = false;
                            pbpauseamthanh.Visible = true;
                            lbthongbao.Text = "Có lỗi scan - xem lại đi.";
                        }
                        else
                        {
                            ngaymuontim = ngayhomnay;
                            lbmasp.Text = dulieu.laymasp(txtbarcodevao.Text);
                            dulieu.inserthangvao(txtbarcodevao.Text, lbmasp.Text);
                            loadtatca();
                            txtbarcodevao.Focus();
                            lbthongbao.Text = "Go on";
                        }
                    }
                    catch (Exception)
                    {

                        lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
                    }
                    
                }
            }
        }

        private void hangravao_Load(object sender, EventArgs e)
        {
            txtbarcodera.Focus();
        }

        private void btnxuat12h_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag12h.RowCount == 0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày-" + ngaymuontim + "- 12h";
                    ham.xuatfileexcelhangravao(dulieu.hangra12h(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
            
        }

        private void btnxuat22h_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag22h.RowCount == 0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày-" + ngaymuontim + "- 22h";
                    ham.xuatfileexcelhangravao(dulieu.hangra22h(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
            
        }

        private void btnxuat15h_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag15h.RowCount == 0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày-" + ngaymuontim + "- 15h";
                    ham.xuatfileexcelhangravao(dulieu.hangra15h(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
        
            
        }

        private void btnxuat18h_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag18h.RowCount == 0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày-" + ngaymuontim + "- 18h";
                    ham.xuatfileexcelhangravao(dulieu.hangra18h(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
           
        }

        private void btnxuatfilehangvao_Click(object sender, EventArgs e)
        {
            try
            {
                if (datagvaokho.RowCount == 0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày vào-" + ngaymuontim;
                    ham.xuatfileexcelhangravao(dulieu.hangvaokho(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
           
        }

        private void ctrMonth_DateSelected_1(object sender, DateRangeEventArgs e)
        {
            try
            {
                var month = sender as MonthCalendar;
                ngaymuontim = month.SelectionStart.ToString("ddMMyyyy");
                lbthongbao.Text = "Đã chọn ngày:\n- " + month.SelectionStart.ToString("dd/MM/yyyy");
                loadtatca();
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề \n- Xem lại đi";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag12h.RowCount == 0 && datag15h.RowCount==0 && datag18h.RowCount==0 && datag22h.RowCount==0)
                {
                    lbthongbao.Text = "Có gì đâu mà xuất";
                    return;
                }
                else
                {
                    string tenfilexuat = "Ngày ra-" + ngaymuontim;
                    ham.xuatfileexcelhangravao(dulieu.hangrakho(ngaymuontim), tenfilexuat);
                    ham.mofileexcelhangravao();
                    lbthongbao.Text = "Vừa xuất 1 em tại đường dẫn:\n--> '" + ham.layduongdan() + "'";
                }
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vấn đề rồi\n-Xem lại đi !";
            }
        }
    }
}
