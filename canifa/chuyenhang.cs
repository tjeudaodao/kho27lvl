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
            // datagrid2.DataSource = dulieu.locdulieu2();
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
                        try
                        {
                            txtmasp.Text = dulieu.laymasp(txtbarcode.Text);
                            lbsoluong.Text = "1";

                            string ketqua = "";
                            dulieu.insertdl1(txtbarcode.Text, txtmasp.Text, lbsoluong.Text, ngay, gio);
                            dulieu.loadvaodatag1(datagrid1);
                            datagrid1.FirstDisplayedScrollingRowIndex = datagrid1.RowCount - 1;
                            txtbarcode.Clear();
                            txtbarcode.Focus();

                            if (dulieu.kiemtracotrongdon(txtmasp.Text) == null)
                            {
                                ketqua = "Ngoài đơn";
                                for (int i = 0; i < lv1.Items.Count; i++)
                                {
                                    if (txtmasp.Text == lv1.Items[i].SubItems[0].Text.ToString())
                                    {
                                        lv1.Items[i].SubItems[1].Text = dulieu.laysoluongthucte(txtmasp.Text).ToString();
                                        lv1.Items[i].SubItems[2].Text = ketqua;
                                        lbtongsoluong.Text = dulieu.tongcheckhang();
                                        lbthongbao.Text = "Ngoài đơn";
                                        lbthongbaoloi.Text = "Ok, Cứ triển thôi";
                                        ham.tudongchontronglistview(lv1, i);
                                        return;
                                    }

                                }
                                lv1.Items.Add(ham.addvaolv(txtmasp.Text, lbsoluong.Text, ketqua));
                                lbtongsoluong.Text = dulieu.tongcheckhang();
                                lbthongbao.Text = "Ngoài đơn";
                                ham.tudongchontronglistview(lv1, lv1.Items.Count - 1);
                                return;
                            }
                            /*
                            if (dulieu.demsoluongsptrongbangtam1() == "0")
                            {
                                ketqua = "Chưa ADD đơn";
                                lbthongbao.Text = "Chưa ADD đơn";
                                if (lv1.Items.Count == 0)
                                {
                                    lv1.Items.Add(ham.addvaolv(txtmasp.Text, lbsoluong.Text, ketqua));
                                }
                                else
                                {
                                    for (int i = 0; i < lv1.Items.Count; i++)
                                    {
                                        if (txtmasp.Text == lv1.Items[i].SubItems[0].Text.ToString())
                                        {
                                            lv1.Items[i].SubItems[1].Text = dulieu.laysoluongthucte(txtmasp.Text).ToString();
                                            lv1.Items[i].SubItems[2].Text = ketqua;
                                            lbtongsoluong.Text = dulieu.tongcheckhang();

                                            lbthongbaoloi.Text = "Ok, tiếp tục triển thôi";
                                            return;
                                        }

                                    }
                                    lv1.Items.Add(ham.addvaolv(txtmasp.Text, lbsoluong.Text, ketqua));
                                }
                            } */
                            else if (dulieu.demsoluongsptrongbangtam1() != "0")
                            {
                                if (dulieu.laysoluongthucte(txtmasp.Text) < dulieu.laysoluongtudon(txtmasp.Text))
                                {
                                    ketqua = "Chưa đủ";
                                    ham.phatbaothieu();
                                }
                                else if (dulieu.laysoluongthucte(txtmasp.Text) == dulieu.laysoluongtudon(txtmasp.Text))
                                {
                                    ketqua = "Đã đủ";
                                    ham.phatbaodu();
                                }
                                else if (dulieu.laysoluongthucte(txtmasp.Text) > dulieu.laysoluongtudon(txtmasp.Text))
                                {
                                    ketqua = "Thừa rồi";
                                    ham.phatbaothua();
                                }
                                if (lv1.Items.Count == 0)
                                {
                                    lv1.Items.Add(ham.addvaolv(txtmasp.Text, lbsoluong.Text, ketqua));
                                }
                                else
                                {
                                    for (int i = 0; i < lv1.Items.Count; i++)
                                    {
                                        if (txtmasp.Text == lv1.Items[i].SubItems[0].Text.ToString())
                                        {
                                            lv1.Items[i].SubItems[1].Text = dulieu.laysoluongthucte(txtmasp.Text).ToString();
                                            lv1.Items[i].SubItems[2].Text = ketqua;
                                            lbtongsoluong.Text = dulieu.tongcheckhang();
                                            txtbarcode.Clear();
                                            txtbarcode.Focus();
                                            lbthongbao.Text = ketqua;
                                            ham.tudongnhaydenmasp(datagrid3, txtmasp.Text);
                                            ham.tudongchontronglistview(lv1, i);
                                            lbthongbaoloi.Text = "Ok, tiếp tục triển thôi";
                                            return;
                                        }

                                    }
                                    lv1.Items.Add(ham.addvaolv(txtmasp.Text, lbsoluong.Text, ketqua));
                                    lbtongsoluong.Text = dulieu.tongcheckhang();
                                    ham.tudongnhaydenmasp(datagrid3, txtmasp.Text);
                                    ham.tudongchontronglistview(lv1, lv1.Items.Count - 1);
                                    lbthongbao.Text = ketqua;
                                }
                            }

                            lbtongsoluong.Text = dulieu.tongcheckhang();
                            txtbarcode.Clear();
                            txtbarcode.Focus();
                            lbthongbaoloi.Text = "Ok, tiếp tục triển thôi";

                        }
                        catch (Exception)
                        {

                            ham.thongbaogocmanhinh(ctroNotifi, "Nhắc nhở", "Xem lại đi lỗi rồi", 1);
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
                }
            }
            try
            {
                dulieu.xoabangtamchuyenhang();
                ngay = DateTime.Now.ToString("ddMMyyyy");
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

        private void datagrid1_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void pbdelete_Click(object sender, EventArgs e)
        {

            DialogResult hoi = MessageBox.Show("Có chắc muốn xóa mã này không ?\n" + txtmasp.Text, "Hỏi cho chắc cú", MessageBoxButtons.YesNo);
            if (hoi == DialogResult.Yes)
            {
                try
                {
                    dulieu.deletemaspchuyenhang(idrows);
                    dulieu.updatebanglistview(lv1, txtmasp.Text);
                    updatetatca();
                   // dulieu.updatebangsosanh(lv1);
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
            
            
                dulieu.modt();

                for (int i = 0; i < datagrid3.Rows.Count ; i++)
                {
                    StrQuery = "INSERT INTO bangtamchuyenhang1(masp,soluong1) VALUES ('" + datagrid3.Rows[i].Cells[0].Value.ToString().Trim() + "', '" + datagrid3.Rows[i].Cells[1].Value.ToString() + "')";
                    SQLiteCommand cmd = new SQLiteCommand(StrQuery, dulieu.con);
                    cmd.ExecuteNonQuery();
                }

                dulieu.dongdt();
            }

            catch (Exception)
            {
                lbthongbaoloi.Text = "Lỗi rồi:\n-Chỉ được chọn 2 cột (2xn) n bao nhiêu cũng được";
                ham.thongbaogocmanhinh(ctroNotifi, "Lỗi rồi", "Chỉ chọn vùng cần copy với kiểu (2xN)", 2);
            }
            lbsoluongcannhat.Text = dulieu.tongsoluongcannhat();
            dulieu.updatebangsosanh(lv1);
            ham.thongbaogocmanhinh(ctroNotifi, "OK", "Nuột", 1);
        }
        public PictureBox laytuchuyenhang
        {
            get { return this.pbpause; }
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
                if (lv1.Items.Count != 0)
                {
                    ham.xuatfile(lv1, lbtongsoluong.Text);
                    ham.mofileexcelvualuu();
                    lbthongbaoloi.Text = "Đã xuất ra file excel\n-Đường dẫn:\n'" + ham.layduongdan() + "'";
                    ham.thongbaogocmanhinh(ctroNotifi, "OK", "Để ý đường dẫn vừa lưu", 1);
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
                lv1.Items.Clear();
                lv1.Refresh();
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
                    dulieu.savevaobangchuyenhang();
                    dulieu.xoabangtamchuyenhang();
                    dulieu.xoabangtamchuyenhang1();
                    lammoitatca();
                    datagrid3.DataSource = null;
                    datagrid3.Refresh();
                    lbthongbao.Text = "OK tiếp tục thôi";
                    ham.thongbaogocmanhinh(ctroNotifi, "OK", "Triển chiêu", 1);
                }
                catch (Exception)
                {

                    lbthongbao.Text = "Có vấn đề";
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

    }
}
