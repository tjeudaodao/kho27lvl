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
    public partial class khuyenmai : UserControl
    {
        data dulieu = new data();
        ham ham = new ham();
        DataTable bangtam = new DataTable();
        
        public khuyenmai()
        {
            InitializeComponent();
            bangtam = taobangtam();

        }
        public void loadbarcode()
        {
            txtbarcode.Clear();
            txtmatong.Clear();
            txtbarcode.Focus();
        }
        public DataTable taobangtam()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã tổng", typeof(String));
            dt.Columns.Add("Giá gốc", typeof(String));
            dt.Columns.Add("Giá sau cùng", typeof(String));
            dt.Columns.Add("Số giảm", typeof(String));
            dt.AcceptChanges();
            return dt;
        }
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtbarcode.Text))
                {
                    try
                    {
                        string machitiet = dulieu.laymasp(txtbarcode.Text);
                        lbmatong.Text = ham.laymatong(machitiet);
                        string matong = lbmatong.Text;
                        double giagoc = ham.ConvertToDouble(dulieu.laygiagoc(matong));
                        double giagiam= ham.ConvertToDouble(dulieu.laygiagiam(matong));
                        txtmatong.Clear();
                        datag1.DataSource = null;
                        datag1.Refresh();
                        if (giagiam==0)
                        {
                            lbgiacuoicung.Text = ham.doisangdonvitien(giagoc);
                            lbphantramgiam.Text = "Không giảm";
                            lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                            datag1.DataSource = ham.themvaobangtam(bangtam, matong, giagoc.ToString(),giagoc.ToString(), giagiam.ToString());
                            loadbarcode();
                            return;
                        }
                        if (giagiam>0 && giagiam<1)
                        {
                            double giacuoicung = (giagoc * (1 - giagiam));
                            giagiam = giagiam * 100;
                            lbgiacuoicung.Text = ham.doisangdonvitien(giacuoicung);
                            lbphantramgiam.Text = ham.doisangphantramgiam(giagiam);
                            lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                            datag1.DataSource = ham.themvaobangtam(bangtam, matong,giagoc.ToString(), giacuoicung.ToString(), lbphantramgiam.Text);
                            loadbarcode();
                            return;
                        }
                        if (giagiam>1 && giagiam<100)
                        {
                            double giacuoicung = (giagoc * (100 - giagiam));
                            lbgiacuoicung.Text = ham.doisangdonvitien(giacuoicung);
                            lbphantramgiam.Text = ham.doisangphantramgiam(giagiam);
                            lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                            datag1.DataSource = ham.themvaobangtam(bangtam, matong,giagoc.ToString(), giacuoicung.ToString(), lbphantramgiam.Text);
                            loadbarcode();
                            return;
                        }
                        if (giagiam>100)
                        {
                            double sophantramgiam = ((1 - giagiam / giagoc) * 100);
                            lbgiacuoicung.Text = ham.doisangdonvitien(giagiam);
                            lbphantramgiam.Text = ham.doisangphantramgiam(sophantramgiam);
                            lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                            datag1.DataSource = ham.themvaobangtam(bangtam, matong,giagoc.ToString(), giagiam.ToString(), lbphantramgiam.Text);
                            loadbarcode();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        txtbarcode.Clear();
                        txtbarcode.Focus();
                        MessageBox.Show("Có vấn đề, xem lại nha");
                    }
                    

                }
            }
        }

        private void txtmatong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                datag1.DataSource = dulieu.loctheomatong(txtmatong.Text);
            }
            catch (Exception)
            {
                loadbarcode();
                return;
            }
            
        }

        private void khuyenmai_Load(object sender, EventArgs e)
        {
            try
            {
                txtbarcode.Focus();
                datag1.DataSource = dulieu.bangkhuyenmai();
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private void datag1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = datag1.Rows[e.RowIndex];
                string matong = row.Cells[0].Value.ToString();
                double giagoc = ham.ConvertToDouble(row.Cells[1].Value.ToString());
                double giagiam = ham.ConvertToDouble(row.Cells[2].Value.ToString());

                if (giagiam == 0)
                {
                    lbgiacuoicung.Text = ham.doisangdonvitien(giagoc);
                    lbphantramgiam.Text = "Không giảm";
                    lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                    txtmatong.SelectAll();
                    return;
                }
                if (giagiam > 0 && giagiam < 1)
                {
                    double giacuoicung = (giagoc * (1 - giagiam));
                    giagiam = giagiam * 100;
                    lbgiacuoicung.Text = ham.doisangdonvitien(giacuoicung);
                    lbphantramgiam.Text = ham.doisangphantramgiam(giagiam);
                    lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                    txtmatong.SelectAll();
                    return;
                }
                if (giagiam > 1 && giagiam < 100)
                {
                    double giacuoicung = (giagoc * (100 - giagiam));
                    lbgiacuoicung.Text = ham.doisangdonvitien(giacuoicung);
                    lbphantramgiam.Text = ham.doisangphantramgiam(giagiam);
                    lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                    txtmatong.SelectAll();
                    return;
                }
                if (giagiam > 100)
                {
                    double sophantramgiam = ((1 - giagiam / giagoc) * 100);
                    lbgiacuoicung.Text = ham.doisangdonvitien(giagiam);
                    lbphantramgiam.Text = ham.doisangphantramgiam(sophantramgiam);
                    lbmotasanpham.Text = dulieu.laymotasanpham(matong);
                    txtmatong.SelectAll();
                    return;
                }
            }
            catch (Exception)
            {

                return;
            }
        }

        private void pbdelete_Click(object sender, EventArgs e)
        {
            txtmatong.Clear();
            txtmatong.Focus();
        }
    }
}
