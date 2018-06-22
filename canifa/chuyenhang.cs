using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace canifa
{
    public partial class chuyenhang : UserControl
    {
        data dulieu = new data();
        ham ham = new ham();
        
        static string idrows = "";
        static string ngay = "";
        static string gio = "";

        public chuyenhang()
        {
            InitializeComponent();
            txtbarcode.Enabled = false;
            btnbatdau.Focus();
            pbdelete.Visible = false;
            pbpause.Visible = false;
            lbthongbaoloi.Text = "-Đầu tiên copy mã sản phẩm và số lượng cần nhặt\n-Sau đó Nhấn vào nút 'Bắt đầu' để nhặt hàng ĐC";
        }
        public void updatetatca()
        {
            dulieu.loadvaodatag1(datagrid1);
            datag2.DataSource = dulieu.laydulieubangthuathieu();
            lbtongsoluong.Text = dulieu.tongcheckhang();
        }
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtbarcode.Text))
                {
                    if (dulieu.laymasp(txtbarcode.Text) == null)
                    {
                        ham.phatloi();
                        pbpause.Visible = true;
                        txtbarcode.Enabled = false;
                        txtmasp.Enabled = false;
                        pbpause.Focus();
                        lbthongbaoloi.Text = "Có lỗi scan barcode rồi. Ấn biểu tượng tạm dừng để dừng âm thanh !";
                        ham.thongbaogocmanhinh(ctroNotifi, "Nhắc nhở", "Ấn nút cách (Space) để tiếp tục", 1);
                    }
                    else
                    {
                      //  try
                        {
                            txtmasp.Text = dulieu.laymasp(txtbarcode.Text);
                            lbsoluong.Text = "1";
                            lbthongbao.Text = dulieu.tinhtrang(txtmasp.Text);
                            dulieu.insertdl1(txtbarcode.Text, txtmasp.Text, lbsoluong.Text, ngay, gio);
                            dulieu.loadvaodatag1(datagrid1);
                            dulieu.baoamthanh(txtmasp.Text);
                            datagrid1.FirstDisplayedScrollingRowIndex = datagrid1.RowCount - 1;
                            dulieu.chenvaobangthuathieu(txtmasp.Text);
                            datag2.DataSource = dulieu.laydulieubangthuathieu();
                            ham.tudongnhaydenmasp(datag2, txtmasp.Text);
                            ham.tudongnhaydenmasp(datagrid3, txtmasp.Text);
                            txtbarcode.Clear();
                            txtbarcode.Focus();

                                lbtongsoluong.Text = dulieu.tongcheckhang();
                           
                                lbthongbaoloi.Text = "Ok, tiếp tục triển thôi";
                            
                        }
                       // catch (Exception)
                        {

                      //      ham.thongbaogocmanhinh(ctroNotifi, "Nhắc nhở", "Xem lại đi lỗi rồi", 1);
                        }
                    }
                }

            }
        }

        private void btnbatdau_Click(object sender, EventArgs e)
        {
            if (dulieu.kiemtracondonhangdangnhatkhong()!=null)
            {
                DialogResult hoi = MessageBox.Show("Còn đơn hàng đang nhặt từ đợt trước\n- '" + dulieu.kiemtracondonhangdangnhatkhong() + "'\n\nCó muốn nhặt tiếp không?", "Vẫn còn đơn cũ", MessageBoxButtons.YesNo);
                if (hoi==DialogResult.Yes)
                {
                    updatetatca();
                    ngay = DateTime.Now.ToString("dd/MM/yyyy");
                    gio = DateTime.Now.ToString("HH:mm");
                    txtbarcode.Enabled = true;
                    txtbarcode.Focus();
                    btnbatdau.Enabled = false;
                }
                else if (hoi==DialogResult.No)
                {
                    try
                    {
                        dulieu.xoabangtamchuyenhang();
                        dulieu.xoabangthuathieu();
                        ngay = DateTime.Now.ToString("dd/MM/yyyy");
                        gio = DateTime.Now.ToString("HH:mm");
                        txtbarcode.Enabled = true;
                        txtbarcode.Focus();
                        btnbatdau.Enabled = false;

                    }
                    catch (Exception)
                    {

                        return;
                    }
                }
            }
            else
            {
                try
                {
                    dulieu.xoabangtamchuyenhang();
                    dulieu.xoabangthuathieu();
                    ngay = DateTime.Now.ToString("dd/MM/yyyy");
                    gio = DateTime.Now.ToString("HH:mm");
                    txtbarcode.Enabled = true;
                    txtbarcode.Focus();
                    btnbatdau.Enabled = false;

                }
                catch (Exception)
                {

                    return;
                }
            }

        }

        public void chonhangcuoicung()
        {
            try
            {
                int RowIndex = datagrid1.RowCount - 1;
                DataGridViewRow row = datagrid1.Rows[RowIndex];
                idrows = row.Cells[0].Value.ToString();
                txtbarcode.Text = row.Cells[1].Value.ToString();
                txtmasp.Text = row.Cells[2].Value.ToString();
                pbdelete.Visible = true;
            }
            catch (Exception)
            {

                ham.thongbaogocmanhinh(ctroNotifi, "Báo", "Chưa có dữ liệu", 1);
            }
           
        }
        public void datagrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = datagrid1.Rows[e.RowIndex];
                idrows = row.Cells[0].Value.ToString();
                txtbarcode.Text = row.Cells[1].Value.ToString();
                txtmasp.Text = row.Cells[2].Value.ToString();
                pbdelete.Visible = true;
            }
            catch (Exception)
            {

                return;
            }

        }

        public void pbdelete_Click(object sender, EventArgs e)
        {

            DialogResult hoi = MessageBox.Show("Có chắc muốn xóa mã này không ?\n" + txtmasp.Text, "Hỏi cho chắc cú", MessageBoxButtons.YesNo);
            if (hoi == DialogResult.Yes)
            {
                try
                {
                    dulieu.deletemaspchuyenhang(idrows);
                    dulieu.updatebangthuathieu(txtmasp.Text);
                    updatetatca();
                
                    txtbarcode.Clear();
                    txtmasp.Clear();
                    lbsoluong.Text = "0";
                    pbdelete.Visible = false;
                    txtbarcode.Focus();
                    lbthongbaoloi.Text = " Đã xóa mã sản phẩm thành công.";
                }
                catch (Exception)
                {

                    return;
                }

            }
        }

        private void btnthemdon_Click(object sender, EventArgs e)
        {
            try
            { 
                datagrid3.DataSource = ham.layvungcopy();
            
                dulieu.xoabangtamchuyenhang1();
                string StrQuery="";
                string mau = @"\d\w{2}\d{2}[SWAC]\d{3}-\w{2}\d{3}-\w+";
                string mau1 = @"\d\w{2}\d{2}[SWAC]\d{3}";

                dulieu.modt();

                for (int i = 0; i < datagrid3.Rows.Count ; i++)
                {
                    string magoc = datagrid3.Rows[i].Cells[0].Value.ToString().Trim();
                    if (Regex.IsMatch(magoc,mau))
                    {
                        StrQuery = "INSERT INTO bangtamchuyenhang1(masp,soluong1) VALUES ('" + magoc + "', '" + datagrid3.Rows[i].Cells[1].Value.ToString() + "')";
                    }
                    else if (Regex.IsMatch(magoc,mau1))
                    {
                        StrQuery = "INSERT INTO bangtamchuyenhang1(matong,soluong2) VALUES ('" + magoc + "', '" + datagrid3.Rows[i].Cells[1].Value.ToString() + "')";
                    }
                    SQLiteCommand cmd = new SQLiteCommand(StrQuery, dulieu.con);
                    cmd.ExecuteNonQuery();
                }

                dulieu.dongdt();
            }

            catch (Exception)
            {
                lbthongbaoloi.Text = "Lỗi rồi:\n-Chỉ được chọn 2 cột (2xn) n bao nhiêu cũng được";
                ham.thongbaogocmanhinh(ctroNotifi, "Lỗi rồi", "Chỉ chọn vùng cần copy với kiểu (2xN)", 2);
                return;
            }
            lbsoluongcannhat.Text = dulieu.tongsoluongcannhat();
            dulieu.updatebangthuathieukhichendon(datag2);
            //ham.thongbaogocmanhinh(ctroNotifi, "OK", "Nuột", 1);
        }
        public PictureBox laytuchuyenhang
        {
            get { return this.pbpause; }
        }
        public PictureBox laychuyenhangxoama
        {
            get { return this.pbdelete; }
        }
        public void pbpause_Click(object sender, EventArgs e)
        {
            try
            {
                ham.dungphat();
                pbpause.Visible = false;
                txtbarcode.Enabled = true;
                txtbarcode.Clear();
                txtbarcode.Focus();
                //txtsoluong.Enabled = true;
                txtmasp.Enabled = true;
                lbthongbaoloi.Text = "Ok, giờ tiếp tục kiểm.";
            }
            catch (Exception)
            {

                return;
            }

        }

        private void btnxuatexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (datag2.RowCount > 0)
                {
                    ham.xuatfile(dulieu.laybangxuatchuyenhang(), lbtongsoluong.Text);
                    ham.taovainfileexcelchuyenhang(dulieu.laybangdein(),lbtongsoluong.Text);
                    lbthongbaoloi.Text = "Đã xuất ra file excel\n-Đường dẫn:\n'" + ham.layduongdan() + "'";
                    ham.thongbaogocmanhinh(ctroNotifi, "OK", "Để ý đường dẫn vừa lưu\nVà lấy phiếu vừa in", 1);
                    return;
                }
                lbthongbaoloi.Text = "Chưa có gì mà xuất :)";
            }
            catch (Exception)
            {

                return;
            }
            
        }
        public void clearvungnhap()
        {
            txtbarcode.Clear();
            txtmasp.Clear();

            lbsoluong.Text = "Số lượng";
        }
        public void lammoitatca()
        {
            try
            {
                datagrid1.DataSource = null;
                datagrid1.Refresh();
                lbsoluongcannhat.Text = "0";
                datag2.DataSource = null;
                datag2.Refresh();
                lbtongsoluong.Text = "0";
                clearvungnhap();
                btnbatdau.Enabled = true;
                txtbarcode.Enabled = false;
                //lbthongbao.Text = "Kiểm hàng từ đầu nào.";
            }
            catch (Exception)
            {

                return;
            }

        }

        private void pbsave_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Lưu lại đơn đã kiểm và làm mới\n\n-Sau đó Tiếp hay nghỉ thì tùy !", "CHốt", MessageBoxButtons.OKCancel);
            if (hoi == DialogResult.OK)
            {
                try
                {
                    dulieu.savevaobangchuyenhang(ngay,gio);
                    dulieu.xoabangtamchuyenhang();
                    dulieu.xoabangtamchuyenhang1();
                    dulieu.xoabangthuathieu();
                    lammoitatca();
                    datagrid3.DataSource = null;
                    datagrid3.Refresh();
                    lbthongbao.Text = "OK tiếp tục thôi";
                    ham.thongbaogocmanhinh(ctroNotifi, "OK", "Triển chiêu", 1);
                }
                catch (Exception)
                {

                    lbthongbaoloi.Text = "Có vấn đề";
                }
            }
        }

        private void btnindenhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (datagrid3.RowCount<1)
                {
                    lbthongbaoloi.Text = "Có vấn đề - Xem lại";
                    return;
                }
                ham.taovainfileexcel(lbsoluongcannhat.Text);
                ham.thongbaogocmanhinh(ctroNotifi, "OK", "In rồi giờ nhặt thôi", 1);
            }
            catch (Exception)
            {

                lbthongbaoloi.Text="Có vấn đề - Xem lại";
            }
        }

        private void btntachdon_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Tạo 1 đơn mới từ dữ liệu gốc đã trừ đi số lượng vừa nhặt", "Hỏi", MessageBoxButtons.YesNo);
            if (hoi==DialogResult.Yes)
            {
                try
                {
                    dulieu.savevaobangchuyenhang(ngay, gio);
                    dulieu.xoabangtamchuyenhang();
                    datagrid3.DataSource = dulieu.tachdonmoi(datag2);
                   
                    dulieu.xoabangthuathieu();
                    lammoitatca();
                    lbsoluongcannhat.Text = dulieu.tongsoluongcannhat();
                    lbthongbao.Text = "OK tiếp tục thôi";
                    ham.thongbaogocmanhinh(ctroNotifi, "OK", "Triển chiêu", 1);
                }
                catch (Exception)
                {

                    lbthongbaoloi.Text = "Có vấn đề";
                }
            }
        }

        
    }
}
