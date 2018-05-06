using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;

namespace canifa
{
    public partial class caidat : UserControl
    {
        data dulieu = new data();
        // ham ham = new ham();

        public caidat()
        {
            InitializeComponent();
            lbngaycapnhat.Text = dulieu.layngaycapnhatgannhat();
            lbngaycapnhat1.Text = dulieu.layngaycapnhatgannhat1();
        }

        public void tatbatcacnut(bool chay)
        {

            btnchonfilechinhbarcode.Enabled = chay;
            btnchonfilechinhgiasp.Enabled = chay;
        }

        private void btnchonfilechinhbarcode_Click(object sender, EventArgs e)
        {
            ctrProbar2.Value = 0;
            tatbatcacnut(false);
            OpenFileDialog chonfile = new OpenFileDialog();
            chonfile.Filter = "Mời các anh chọn file excel (*.xlsx)|*.xlsx";
            chonfile.Multiselect = false;
            if (chonfile.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    //dulieu.xoabanglayngaycapnhat();

                    string barcode = null;

                    ExcelPackage filechon = new ExcelPackage(new FileInfo(chonfile.FileName));
                    ExcelWorksheet ws = filechon.Workbook.Worksheets[1];
                    var sodong = ws.Dimension.End.Row;
                    var socot = ws.Dimension.End.Column;
                    ctrProbar2.Maximum = sodong;
                    if (socot > 2 || sodong < 50000)
                    {
                        MessageBox.Show("Xem lại File excel.\n Định dạng giống ảnh minh họa");

                    }
                    else
                    {
                        int j = 0;
                        try
                        {
                            dulieu.modt();
                            for (int i = 1; i < sodong; i++)
                            {
                                barcode = ws.Cells[i, 1].Value.ToString();

                                if (dulieu.kiemtramabarcode(barcode) == null && !string.IsNullOrWhiteSpace(barcode))
                                {
                                    dulieu.insertvaodata(barcode, ws.Cells[i, 2].Value.ToString());
                                    j = j + 1;
                                }
                                ctrProbar2.Increment(1);
                            }
                            ctrProbar2.Value = sodong;
                            dulieu.dongdt();
                            MessageBox.Show("Thành công\n--> Đã cập nhật được '" + j.ToString() + "' mã mới.");
                            dulieu.capnhatngaychinhsua();
                            tatbatcacnut(true);
                        }
                        catch (Exception)
                        {
                            tatbatcacnut(true);
                            return;
                        }


                    }

                    filechon.Dispose();

                }

                catch (Exception)
                {
                    tatbatcacnut(true);
                    return;
                }
            }
        }

        private void btnchonfilechinhgiasp_Click(object sender, EventArgs e)
        {
            ctrProbar2.Value = 0;
            tatbatcacnut(false);
            OpenFileDialog chonfile = new OpenFileDialog();
            chonfile.Filter = "Mời các anh chọn file excel (*.xlsx)|*.xlsx";
            chonfile.Multiselect = false;
            if (chonfile.ShowDialog() == DialogResult.OK)
            {
                //try
                // {

                // dulieu.xoabanglayngaycapnhat1();
                string sogiam = null;
                string matong = null;
                string giagoc = null;
                ExcelPackage filechon = new ExcelPackage(new FileInfo(chonfile.FileName));
                ExcelWorksheet ws = filechon.Workbook.Worksheets[1];
                var sodong = ws.Dimension.End.Row;
                var socot = ws.Dimension.End.Column;
                ctrProbar2.Maximum = sodong;
                if (socot > 4)
                {
                    MessageBox.Show("Xem lại File excel.\n Định dạng giống ảnh minh họa");

                }
                else
                {
                    int j = 0;
                    //try
                    {
                        //dulieu.modt1();
                        for (int i = 1; i < sodong; i++)
                        {
                            matong = ws.Cells[i, 1].Value.ToString();
                            giagoc = ws.Cells[i, 2].Value.ToString();
                            try
                            {
                                sogiam = ws.Cells[i, 3].Value.ToString();
                            }
                            catch (Exception)
                            {

                                sogiam = "0";
                            }
                            
                            if (dulieu.kiemtramatong(matong) == null && !string.IsNullOrWhiteSpace(matong))
                            {
                                dulieu.insertvaokhuyenmai(matong, giagoc, sogiam);
                                j = j + 1;
                                //continue;
                            }
                            else if (dulieu.kiemtramatong(matong) != null && !string.IsNullOrWhiteSpace(matong) && dulieu.laygiagoc(matong) != giagoc)
                            {
                                dulieu.capnhatbangkhuyenmai(matong, giagoc, sogiam);
                                j = j + 1;
                            }
                            else if (dulieu.kiemtramatong(matong) != null && !string.IsNullOrWhiteSpace(matong) && dulieu.laygiagiam(matong) != sogiam)
                            {
                                dulieu.capnhatbangkhuyenmai(matong, giagoc, sogiam);
                                j = j + 1;
                            }
                            else if (dulieu.kiemtramatong(matong) != null && !string.IsNullOrWhiteSpace(matong) && dulieu.laygiagiam(matong) == sogiam && dulieu.laygiagoc(matong) == giagoc)
                            {
                                continue;
                            }
                            ctrProbar2.Increment(1);
                        }
                        ctrProbar2.Value = sodong;
                       // dulieu.dongdt1();
                        MessageBox.Show("Thành công\n--> Đã cập nhật được '" + j.ToString() + "' mã.");
                        dulieu.capnhatngaychinhsua1();
                        tatbatcacnut(true);
                    }
                    //catch (Exception)
                    //{
                    //    tatbatcacnut(true);
                    //    return;
                    //}


                }

                filechon.Dispose();

                //}

                //catch (Exception)
                //{
                //    tatbatcacnut(true);
                //    return;
                //}
            }
            tatbatcacnut(true);
        }




    }
}
