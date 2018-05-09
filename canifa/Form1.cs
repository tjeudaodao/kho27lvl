using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Media;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;

namespace canifa
{
    public partial class Form1 : Form
    {
        data dulieu = new data();
        ham ham = new ham();
        chuyenhang uschuyenhang = new chuyenhang();
       // hangravao ushangravao = new hangravao();
       // khuyenmai uskhuyenmai = new khuyenmai();
        timkiemcaidat ustimkiem = new timkiemcaidat();
        
        static string ngay = "";
        static string gio = "";
        static string idrows = "";
        static bool cochuthaydoi = false;
        static int tabnao =1;

        string thongbaobandau = "Ấn vào nút 'Kiểm hàng' để bắt đầu \n- Nếu có số phiếu thì scan số phiếu. \n -Sau đó muốn chính sửa mã nào thì nhấn trực tiếp vào mã đó tại bảng ngoài cùng bên trái";
        public Form1()
        {
            InitializeComponent();
            dulieu.xoabangtam();
            ham.thongbaogocmanhinh(ctrNotifi, "Keep on loving you !", "Một ngày bạn và tôi - hts", 2);
            uschuyenhang.Location = new Point(0, 70);
            uschuyenhang.Name = "tabchuyenhang";
            this.Controls.Add(uschuyenhang);
           // ushangravao.Location = new Point(0, 70);
           // ushangravao.Name = "tabhangravao";
          //  this.Controls.Add(ushangravao);
          //  uskhuyenmai.Location = new Point(0, 70);
          //  uskhuyenmai.Name = "tabkhuyenmai";
          //  this.Controls.Add(uskhuyenmai);
            ustimkiem.Location = new Point(0, 70);
            ustimkiem.Name = "tabtimkiem";
            this.Controls.Add(ustimkiem);

            uschuyenhang.Hide();
          //  ushangravao.Hide();
          //  uskhuyenmai.Hide();
            ustimkiem.Hide();

            txtsophieu.Focus();
            txtbarcode.Enabled = false;
            dulieu.xoabangtam2();
            picbox1.Visible = false;
            pbdelete.Visible = false;
            pbedit.Visible = false;
           
            lbthongbao.Text = "- Nếu có số phiếu thì scan số phiếu trước.\n- Sau đó Ấn vào nút 'Kiểm hàng' để bắt đầu \n \n -Sau đó muốn chính sửa mã nào thì nhấn trực tiếp vào mã đó tại bảng ngoài cùng bên trái\n-Cuối cùng ấn vào nút 'Lưu' để lưu và kiểm tiếp đơn khác";
        }
        
        #region lien quan den form
        private void btnchuyenhang_Click(object sender, EventArgs e)
        {
            btnkiemhang.BackColor = Color.DimGray;
            btntabkhuyemmai.BackColor = Color.DimGray;
            btnhangravao.BackColor = Color.DimGray;
            btntabtimkiem.BackColor = Color.DimGray;
            btnchuyenhang.BackColor = Color.Tomato;
            //  btntabcaidat.BackColor = Color.DimGray;
            tabnao = 2;
            pantieude2.BackColor = Color.Tomato;
            panduoicung.BackColor = Color.Tomato;
            uschuyenhang.Show();
            uschuyenhang.BringToFront();
            /*
            chuyenhang uschuyenhang = new chuyenhang();
            uschuyenhang.Location = new Point(0, 70);
            uschuyenhang.Name = "tabchuyenhang";
                     
            foreach (Control c in this.Controls)
            {
                if (c.Name == "tabchuyenhang")
                {
                    this.Controls["tabchuyenhang"].BringToFront();
                    
                    return;
                }
                
            }
            this.Controls.Add(uschuyenhang);
            uschuyenhang.BringToFront();
            //MessageBox.Show("da co");
            */
        }

        private void btnkiemhang_Click(object sender, EventArgs e)
        {
            btnchuyenhang.BackColor = Color.DimGray;
            btnhangravao.BackColor = Color.DimGray;
            btntabkhuyemmai.BackColor = Color.DimGray;
            btnkiemhang.BackColor = Color.MediumSpringGreen;
            btntabtimkiem.BackColor = Color.DimGray;
            // btntabcaidat.BackColor = Color.DimGray;
            tabnao = 1;
            pantieude2.BackColor = Color.MediumSpringGreen;
            panduoicung.BackColor = Color.MediumSpringGreen;
            ustimkiem.Hide();
            uschuyenhang.Hide();
          //  ushangravao.Hide();
          //  uskhuyenmai.Hide();
            //  uscaidat.Hide();
        }
        private void pbmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbclose_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Thoát thật à ?", "Hỏi lần cuối", MessageBoxButtons.OKCancel);
            if (hoi == DialogResult.OK)
            {
                dulieu.xoabangtam();
                dulieu.xoabangtam2();
                dulieu.xoabangtamchuyenhang1();
                this.Close();
            }
        }

        private void btnhangravao_Click(object sender, EventArgs e)
        {
            btnhangravao.BackColor = Color.CornflowerBlue;
            btnkiemhang.BackColor = Color.DimGray;
            btntabkhuyemmai.BackColor = Color.DimGray;
            btnchuyenhang.BackColor = Color.DimGray;
            btntabtimkiem.BackColor = Color.DimGray;
            //btntabcaidat.BackColor = Color.DimGray;

            pantieude2.BackColor = Color.CornflowerBlue;
            panduoicung.BackColor = Color.CornflowerBlue;
          //  ushangravao.Show();
           // ushangravao.BringToFront();
        }

        private void btntabkhuyemmai_Click(object sender, EventArgs e)
        {
            btntabkhuyemmai.BackColor = Color.Indigo;
            pantieude2.BackColor = Color.Indigo;
            btnkiemhang.BackColor = Color.DimGray;
            btnhangravao.BackColor = Color.DimGray;
            btnchuyenhang.BackColor = Color.DimGray;
            panduoicung.BackColor = Color.Indigo;
            btntabtimkiem.BackColor = Color.DimGray;
            // btntabcaidat.BackColor = Color.DimGray;

           // uskhuyenmai.Show();
           // uskhuyenmai.BringToFront();
        }
        private void btntabtimkiem_Click(object sender, EventArgs e)
        {
            btntabtimkiem.BackColor = Color.Navy;
            pantieude2.BackColor = Color.Navy;
            panduoicung.BackColor = Color.Navy;
            btnkiemhang.BackColor = Color.DimGray;
            btnchuyenhang.BackColor = Color.DimGray;
            btnhangravao.BackColor = Color.DimGray;
            btntabkhuyemmai.BackColor = Color.DimGray;
            // btntabcaidat.BackColor = Color.DimGray;
            tabnao = 3;
            ustimkiem.Show();
            ustimkiem.BringToFront();
        }

        #endregion

        #region tab kiem hang
        public void updatetatca()
        {
            dulieu.loadgv0(datagird0);
            datagriv1.DataSource = dulieu.locdulieu1();
            lbtongsoluong.Text = dulieu.tongkiemhang();
        }
        public void clearvungnhap()
        {
            txtbarcode.Clear();
            txtsoluong.Clear();
            txtmasp.Clear();
            lbmatong.Text = "Mã tổng";
        }
        public void lammoitatca()
        {
            datagird0.DataSource = null;
            datagird0.Refresh();
            datagriv1.DataSource = null;
            datagriv1.Refresh();

            lbtongsoluong.Text = "0";
            clearvungnhap();
            btnbatdaukiemhang.Enabled = true;
            txtbarcode.Enabled = false;
            //lbthongbao.Text = "Kiểm hàng từ đầu nào.";
        }
        
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtbarcode.Text))
                {
                    try
                    {
                        if (dulieu.laymasp(txtbarcode.Text) == null)
                        {
                            ham.phatloi();
                            picbox1.Visible = true;
                            txtbarcode.Enabled = false;
                            txtmasp.Enabled = false;
                            txtsoluong.Enabled = false;
                            picbox1.Focus();
                            lbthongbao.Text = "Có lỗi scan barcode rồi. Ấn biểu tượng tạm dừng để dừng âm thanh !";
                            ham.thongbaogocmanhinh(ctrNotifi, "Lỗi rồi", "Ấn nút cách (Space) để tiếp tục", 2);
                        }
                        else
                        {

                            txtmasp.Text = dulieu.laymasp(txtbarcode.Text);
                            txtsoluong.Text = "1";
                            lbmatong.Text = ham.laymatong(txtmasp.Text);

                            if (string.IsNullOrEmpty(txtsophieu.Text))
                            {
                                txtsophieu.Text = "Không có số phiếu";
                            }
                            dulieu.insertvaodata(txtbarcode.Text, txtmasp.Text, txtsoluong.Text, txtsophieu.Text, lbmatong.Text, ngay, gio);
                            dulieu.loadgv0(datagird0);
                            if (cochuthaydoi!=false)
                            {
                                datagriv1.DefaultCellStyle.Font = new Font("Comic Sans MS", 30f);
                                cochuthaydoi = false;
                            }
                            datagriv1.DataSource = dulieu.locdulieu1();
                            dulieu.kiemtramamoi(lbmatong.Text);
                            datagird0.FirstDisplayedScrollingRowIndex = datagird0.RowCount - 1;
                            lbtongsoluong.Text = dulieu.tongkiemhang();
                            txtbarcode.Clear();
                            txtbarcode.Focus();
                            ham.tudongnhaydenmasp(datagriv1, lbmatong.Text);
                        }
                    }
                   catch (Exception)
                    {

                        lbthongbao.Text = "Có vân đề \n Xem lại đi";
                    }

                }

            }
        }

        private void picbox1_Click(object sender, EventArgs e)
        {
            try
            {
                ham.dungphat();
                picbox1.Visible = false;
                txtbarcode.Enabled = true;
                txtbarcode.Clear();
                txtbarcode.Focus();
                txtsoluong.Enabled = true;
                txtmasp.Enabled = true;
                lbthongbao.Text = "Ok, giờ tiếp tục kiểm.";
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vân đề \n Xem lại đi";
            }

        }


        private void btnbatdaukiemhang_Click(object sender, EventArgs e)
        {
            try
            {
                dulieu.xoabangtam();
                dulieu.xoabangtam2();
                txtbarcode.Enabled = true;
                ngay = DateTime.Now.ToString("ddMMyyyy");
                gio = DateTime.Now.ToString("HH:mm");
                btnbatdaukiemhang.Enabled = false;
                txtbarcode.Focus();
                lbthongbao.Text = "OK, giờ bắt đầu kiểm hàng.";
                ham.thongbaogocmanhinh(ctrNotifi, "OK", "Triển chiêu", 1);
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vân đề \n Xem lại đi";
            }

        }

        private void pbedit_Click(object sender, EventArgs e)
        {
            try
            {
                string masp = txtmasp.Text;
                dulieu.updatebangtam(txtsoluong.Text, idrows);
                clearvungnhap();
                updatetatca();
                txtbarcode.Focus();
                pbedit.Visible = false;
                pbdelete.Visible = false;
                pbtieptuc.Visible = false;
                lbthongbao.Text = " Vừa sửa mã\n- '" + masp + "'";
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vân đề \n Xem lại đi";
            }

        }
        public void chonhangcuoicung()
        {
            try
            {
                int RowIndex = datagird0.RowCount - 1;
                DataGridViewRow row = datagird0.Rows[RowIndex];
                idrows = row.Cells[0].Value.ToString();
                txtbarcode.Text = row.Cells[1].Value.ToString();
                txtmasp.Text = row.Cells[2].Value.ToString();
                pbdelete.Visible = true;
            }
            catch (Exception)
            {

                ham.thongbaogocmanhinh(ctrNotifi, "Báo", "Chưa có dữ liệu", 1);
            }

        }
        private void datagird0_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = datagird0.Rows[e.RowIndex];
                idrows = row.Cells[0].Value.ToString();
                txtbarcode.Text = row.Cells[1].Value.ToString();
                txtmasp.Text = row.Cells[2].Value.ToString();
                txtsoluong.Text = row.Cells[3].Value.ToString();
                lbmatong.Text = ham.laymatong(txtmasp.Text);
                pbedit.Visible = true;
                pbdelete.Visible = true;
                pbtieptuc.Visible = true;
                txtsoluong.Focus();
                txtsoluong.SelectAll();
                lbthongbao.Text = "Sửa số lượng trực tiếp. Sau đó nhấn ENTER hoặc nhấn vào nút chỉnh sửa\n-Hoặc muốn xóa mã nào thì nhấn vào nút xóa";
            }
            catch (Exception)
            {

                return;
            }

        }

        private void txtsoluong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pbedit.Visible == true)
                {
                    try
                    {
                        string masp = txtmasp.Text;
                        dulieu.updatebangtam(txtsoluong.Text, idrows);
                        clearvungnhap();
                        updatetatca();
                        txtbarcode.Focus();
                        pbedit.Visible = false;
                        pbdelete.Visible = false;
                        pbtieptuc.Visible = false;
                        lbthongbao.Text = " Vừa sửa mã\n- '" + masp + "'";
                    }
                    catch (Exception)
                    {

                        lbthongbao.Text = "Có vân đề \n Xem lại đi";
                    }

                }
            }
        }

        private void pbdelete_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Có chắc muốn xóa mã này không ?\n" + txtmasp.Text, "Hỏi cho chắc cú", MessageBoxButtons.YesNo);
            if (hoi == DialogResult.Yes)
            {
                try
                {
                    dulieu.deletemasp(idrows);
                    lbthongbao.Text = " Vừa xóa mã\n- '" + txtmasp.Text + "'";
                    ham.thongbaogocmanhinh(ctrNotifi, "Thông báo", "Vừa xóa mã\n- '" + txtmasp.Text + "'", 1);
                    updatetatca();
                    clearvungnhap();
                    pbedit.Visible = false;
                    pbdelete.Visible = false;
                    pbtieptuc.Visible = false;
                    txtbarcode.Focus();
                }
                catch (Exception)
                {

                    lbthongbao.Text = "Có vân đề \n Xem lại đi";
                }


            }
        }

        private void btnsosanh_Click(object sender, EventArgs e)
        {
            probartientrinh.Value = 0;
            probartientrinh.Maximum = 100;
            OpenFileDialog chonfile = new OpenFileDialog();
            chonfile.Filter = "Mời các anh chọn file excel (*.xlsx)|*.xlsx";
            chonfile.Multiselect = true;
            if (chonfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    dulieu.xoabangtam2();
                    string[] cacfiledachon = chonfile.FileNames;
                    foreach (string file in cacfiledachon)
                    {
                        probartientrinh.Value = 30;
                        string matong = null;
                        string soluong = null;
                        ExcelPackage filechon = new ExcelPackage(new FileInfo(file));
                        ExcelWorksheet ws = filechon.Workbook.Worksheets[1];
                        var sodong = ws.Dimension.End.Row;
                        if (sodong < 2)
                        {
                            MessageBox.Show("Lỗi rồi. File đã chọn chưa có dữ liệu");
                            lbthongbao.Text = "File này ko được rồi. Thôi đếm tay vậy :)";
                        }
                        else
                        {
                            probartientrinh.Value = 50;
                          
                            try
                            {
                                dulieu.modt();
                                for (int i = 3; i < sodong; i++)
                                {
                                    matong = ws.Cells[i, 10].Value.ToString();

                                    soluong = ws.Cells[i, 14].Value.ToString();
                                    dulieu.laydataexcel(matong, soluong);

                                }
                                probartientrinh.Value = 70;
                               
                                datagriv1.DataSource = ham.bangdasosanh(dulieu.sosanhdulieu());
                                dulieu.dongdt();
                                lbthongbao.Text = "ok, giờ xem xem có chuẩn không.\n-Chuẩn rồi thì lưu lại và tiếp đơn khác thôi.";
                            }
                            catch (Exception)
                            {

                                lbthongbao.Text = "Có vấn đề với file đã chọn rồi";
                            }


                        }

                        filechon.Dispose();
                    }
                    
                    probartientrinh.Value = 100;
                    lbtongsoluongtheodon.Text = dulieu.tongsoluongbang1();
                    datagriv1.DefaultCellStyle.Font = new Font("Comic Sans MS", 13.5F);
                    cochuthaydoi = true;
                    ham.thongbaogocmanhinh(ctrNotifi, "Thông báo", "Đã xong - Đối chiếu xem sao!\n Nếu OK thì ấn vào nút lưu.", 1);
                }
                catch (Exception)
                {

                    lbthongbao.Text = "Có vân đề \n Xem lại đi";
                    ham.thongbaogocmanhinh(ctrNotifi, "Lỗi rồi", "Xem lại", 1);
                }

            }
        }

        private void pbsave_Click(object sender, EventArgs e)
        {
            DialogResult hoi = MessageBox.Show("Lưu lại đơn đã kiểm\n-Số phiếu:'" + txtsophieu.Text + "'\n-Tiếp hay nghỉ thì tùy !", "CHốt", MessageBoxButtons.OKCancel);
            if (hoi == DialogResult.OK)
            {
                try
                {
                    dulieu.savevaobangkiemhang();
                    dulieu.xoabangtam();
                    dulieu.xoabangtam2();
                    lammoitatca();
                    lbthongbao.Text = thongbaobandau;
                    txtsophieu.Clear();
                    txtsophieu.Focus();
                    probartientrinh.Value = 0;
                }
                catch (Exception)
                {

                    lbthongbao.Text = "Có vấn đề";
                }
            }
        }
        private void pbtieptuc_Click(object sender, EventArgs e)
        {
            try
            {
                txtbarcode.Clear();
                txtbarcode.Focus();
                pbedit.Visible = false;
                pbdelete.Visible = false;
                pbtieptuc.Visible = false;
            }
            catch (Exception)
            {

                lbthongbao.Text = "Có vân đề \n Xem lại đi";
            }

        }






        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (picbox1.Visible==true)
            {
                if (e.KeyCode==Keys.Space)
                {
                    try
                    {
                        ham.dungphat();
                        picbox1.Visible = false;
                        txtbarcode.Enabled = true;
                        txtbarcode.Clear();
                        txtbarcode.Focus();
                        txtsoluong.Enabled = true;
                        txtmasp.Enabled = true;
                        lbthongbao.Text = "Ok, giờ tiếp tục kiểm.";
                    }
                    catch (Exception)
                    {

                        lbthongbao.Text = "Có vân đề \n Xem lại đi";
                    }
                }
            }
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData==Keys.Space)
            {
                if (uschuyenhang.laytuchuyenhang.Visible==true)
                {
                    uschuyenhang.pbpause_Click(uschuyenhang.laytuchuyenhang, new KeyEventArgs(keyData));
                }
                
            }
            if (keyData==Keys.Down)
            {
                if (tabnao==2)
                {
                    uschuyenhang.chonhangcuoicung();
                    ham.thongbaogocmanhinh(ctrNotifi, "Thông báo", "Vừa chọn mã cuối chít gần nhất", 1);
                }
                else if (tabnao == 1)
                {
                    chonhangcuoicung();
                    ham.thongbaogocmanhinh(ctrNotifi, "Thông báo", "Vừa chọn mã cuối chít gần nhất", 1);
                }
            }
            if (keyData==Keys.Delete)
            {
                if (tabnao==2)
                {
                    if (uschuyenhang.laychuyenhangxoama.Visible == true)
                    {
                        uschuyenhang.pbdelete_Click(uschuyenhang.laychuyenhangxoama, new KeyEventArgs(keyData));
                    }
                }
                else if (tabnao==1)
                {
                    if (pbdelete.Visible==true)
                    {
                        pbdelete_Click(pbdelete, new KeyEventArgs(keyData));
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);    
        }

        private void txtsophieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnbatdaukiemhang.Focus();
            }
           
        }
    }
}
