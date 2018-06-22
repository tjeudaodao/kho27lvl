using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using Microsoft.Office.Interop;
using excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace canifa
{
    class ham
    {
        SoundPlayer chay = new SoundPlayer(@"baoloi.wav");
        SoundPlayer phat = new SoundPlayer(@"mamoi.wav");
        SoundPlayer baothieu = new SoundPlayer(@"thieu.wav");
        SoundPlayer baothua = new SoundPlayer(@"thua.wav");
        SoundPlayer baodu = new SoundPlayer(@"du.wav");
        SoundPlayer ngoai = new SoundPlayer(@"ngoai.wav");

        public string laymatong(string machitiet)
        {

            string chuoi = null;
            int vitrikitu = machitiet.IndexOf("-");
            chuoi = machitiet.Substring(0, vitrikitu);
            return chuoi;


        }
        public ListViewItem addvaolv(string masp, string sl, string ketqua)
        {
            ListViewItem item = new ListViewItem(masp);
            item.SubItems.Add(sl);
            item.SubItems.Add(ketqua);
            return item;
        }
        public void phatloi()
        {
            chay.PlayLooping();
        }
        public void dungphat()
        {
            chay.Stop();
        }
        public void baomamoi()
        {
            phat.Play();
        }
        public void phatbaothieu()
        {
            baothieu.Play();
        }
        public void phatbaothua()
        {
            baothua.Play();
        }
        public void phatbaodu()
        {
            baodu.Play();
        }
        public void baongoai()
        {
            ngoai.Play();
        }

        public double ConvertToDouble(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return 0;
                }
                return OutVal;
            }
        }
        public string layngaygiohientai()
        {
            return DateTime.Now.ToString("dd/MM/yy-HH:mm");
        }
        public void thongbaogocmanhinh(NotifyIcon notifi,string tieude,string noidung,int thoigianhienthibanggiay)
        {
            
            notifi.BalloonTipTitle = tieude;
            notifi.BalloonTipText = noidung;
            notifi.ShowBalloonTip(thoigianhienthibanggiay*1000);
            
        }
        #region tab kiem hang
      /*  public void tientrinh(bool chay,ProgressBar probar,int somax)
        {
            
            if (chay)
            {
                probar.Increment(1);
            }
            else if (!chay)
            {
                probar.Maximum = somax;
                probar.Value = somax;
            }
        } */
        public DataTable bangdasosanh(DataTable dt)
        {
            double chechlech = 0;
            dt.Columns.Add("Status");
            dt.AcceptChanges();
            foreach (DataRow  row in dt.Rows)
            {
                for (int i = 0; i <= 4; i++)
                {
                    if (row[3].ToString()=="")
                    {
                        row[4] = "Ngoài đơn";
                        continue;
                    }
                    chechlech = ConvertToDouble(row[1].ToString()) - ConvertToDouble(row[3].ToString());
                    if (chechlech>0)
                    {
                        row[4] = "Thừa: "+chechlech+"";
                    }
                    else if (chechlech==0)
                    {
                        row[4] = "OK";
                    }
                    else if (chechlech<0)
                    {
                        row[4] = "Thiếu: " + chechlech + "";
                    }
                }
            }
            return dt;
        }
        #endregion

        #region tab chuyen hang

        public DataTable copyvungchontuexcel()
        {
            DataTable dt = new DataTable();
            DataObject o = (DataObject)Clipboard.GetDataObject();
            if (o.GetDataPresent(DataFormats.Text))
            {
                
                dt.Columns.Add("Mã sản phẩm");
                dt.Columns.Add("SL");
                dt.AcceptChanges();

                string[] pastedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
               
                foreach (string pastedRow in pastedRows)
                {
                    string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

                    DataRow rowadd = dt.NewRow();
                    for (int i = 0; i < pastedRowCells.Length; i++)
                    {
                        
                        rowadd[i] = pastedRowCells[i];
                        
                    }
                    dt.Rows.Add(rowadd);
                }
            }
            return dt;
        }
        public DataTable layvungcopy()
        {
            DataTable dt = new DataTable();
            DataObject o = (DataObject)Clipboard.GetDataObject();
            if (o.GetDataPresent(DataFormats.Text))
            {

                dt.Columns.Add("Mã SP: '"+layngaygiohientai()+"'");
                dt.Columns.Add("SL");
                dt.AcceptChanges();
                string goc = o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray());
                string mau = @"\d\w{2}\d{2}[SWAC]\d{3}-\w{2}\d{3}-\w+\s+\d+";
                string mau1 = @"\d\w{2}\d{2}[SWAC]\d{3}\s+\w+";
                string mau2 = @"\s+";

                MatchCollection matchhts = Regex.Matches(goc, mau);
                MatchCollection matchmatong = Regex.Matches(goc, mau1);
                foreach (Match h in matchhts)
                {

                    string[] hang = Regex.Split(h.Value.ToString(), mau2);
                    
                    DataRow rowadd = dt.NewRow();
                    for (int i = 0; i < hang.Length; i++)
                    {

                        rowadd[i] = hang[i];

                    }
                    dt.Rows.Add(rowadd);
                }
                foreach (Match h in matchmatong)
                {

                    string[] hang = Regex.Split(h.Value.ToString(), mau2);

                    DataRow rowadd = dt.NewRow();
                    for (int i = 0; i < hang.Length; i++)
                    {

                        rowadd[i] = hang[i];

                    }
                    dt.Rows.Add(rowadd);
                }
            }
            return dt;
        }
        public DataTable tachdon(DataTable dtmasp,DataTable dtmatong)
        {
            foreach (DataRow row in dtmatong.Rows)
            {
                DataRow newrow = dtmasp.NewRow();
                newrow[0] = row[0];
                newrow[1] = row[1];
                dtmasp.Rows.Add(newrow);
            }
            return dtmasp;
        }
        public void tudongnhaydenmasp(DataGridView dtv,string masp)
        {
            if (dtv.RowCount<1)
            {
                return;
            }
            for (int i = 0; i <= dtv.RowCount-1; i++)
            {
                if (masp==dtv.Rows[i].Cells[0].Value.ToString())
                {
                    dtv.FirstDisplayedScrollingRowIndex = i;
                    dtv.Rows[i].Selected = true;
                }
            }
        }

        public void tudongchontronglistview(ListView listViewRamos, int i)
        {
           // listViewRamos.FocusedItem = listViewRamos.Items[0];
            listViewRamos.Items[i].Selected = true;
           // listViewRamos.Select();
            listViewRamos.EnsureVisible(i);
        }
        //tao bien string luu duong dan file;
        public string duongdanfileexcel = "";
        
        public void xuatfileexcel(DataTable dt)
        {
            string tenfile = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                saveDialog.FileName = "Điều chuyển-"+tenfile+"";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    duongdanfileexcel = Path.GetFullPath(saveDialog.FileName);
                    string exportFilePath = saveDialog.FileName;
                    var newFile = new FileInfo(exportFilePath);
                    using (var package = new ExcelPackage(newFile))
                    {
                       
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("27LVL_Điều chuyển");
                       
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Column(1).AutoFit();
                        worksheet.Column(2).AutoFit();
                        
                        worksheet.PrinterSettings.LeftMargin = 0.2M/2.54M;
                        worksheet.PrinterSettings.RightMargin= 0.2M / 2.54M;
                        package.Save();
                      
                    }
                }
            }
        }
        public void mofileexcelvualuu()
        {
            if (duongdanfileexcel!=null)
            {
                var app = new excel.Application();
                
                excel.Workbooks book = app.Workbooks;
                excel.Workbook sh = book.Open(duongdanfileexcel);
                app.Visible = true;
                
                sh.PrintOutEx();
            }
            
        }
        public void taovainfileexcel(string tongsp)
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet worksheet = ExcelPkg.Workbook.Worksheets.Add("hts");
            worksheet.Cells["A1"].LoadFromDataTable(layvungcopy(), true);

            worksheet.Column(1).Width = 28;
            worksheet.Column(2).Width = 4;

            
            
            worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = "Tổng sản phẩm:";
            worksheet.Cells[worksheet.Dimension.End.Row, 2].Value = tongsp;

            var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
            var cellFont = allCells.Style.Font;
            cellFont.SetFromFont(new Font("Calibri", 12));

            worksheet.PrinterSettings.LeftMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.RightMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.TopMargin= 0.2M / 2.54M;
            worksheet.Protection.IsProtected = false;
            worksheet.Protection.AllowSelectLockedCells = false;
            if (File.Exists("hts.xlsx"))
            {
                File.Delete("hts.xlsx");

            }
            ExcelPkg.SaveAs(new FileInfo("hts.xlsx"));
            var app = new excel.Application();
            
            excel.Workbooks book = app.Workbooks;
            excel.Workbook sh = book.Open(Path.GetFullPath("hts.xlsx"));
            //app.Visible = true;
            sh.PrintOutEx();
            app.Quit();
        }
        public void taovainfileexcelchuyenhang(DataTable dt, string tongsp)
        {
            ExcelPackage ExcelPkg = new ExcelPackage();
            ExcelWorksheet worksheet = ExcelPkg.Workbook.Worksheets.Add("hts");
            worksheet.Cells["A1"].Value = "27 Lê Văn Lương _ Điều chuyển";
            worksheet.Cells["A3"].LoadFromDataTable(dt, true);

            worksheet.Column(1).Width = 28;
            worksheet.Column(2).Width = 4;



            worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = "Tổng sản phẩm:";
            worksheet.Cells[worksheet.Dimension.End.Row, 2].Value = tongsp;

            var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
            var cellFont = allCells.Style.Font;
            cellFont.SetFromFont(new Font("Calibri", 10));

            worksheet.PrinterSettings.LeftMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.RightMargin = 0.2M / 2.54M;
            worksheet.PrinterSettings.TopMargin = 0.2M / 2.54M;
            worksheet.Protection.IsProtected = false;
            worksheet.Protection.AllowSelectLockedCells = false;
            if (File.Exists("hts.xlsx"))
            {
                File.Delete("hts.xlsx");

            }
            ExcelPkg.SaveAs(new FileInfo("hts.xlsx"));
            var app = new excel.Application();

            excel.Workbooks book = app.Workbooks;
            excel.Workbook sh = book.Open(Path.GetFullPath("hts.xlsx"));
            //app.Visible = true;
            sh.PrintOutEx();
            app.Quit();
        }
        public string layduongdan()
        {
            return duongdanfileexcel;
        }
        public void xuatfile(DataTable dt,string tongsp)
        {
            string tenfile = DateTime.Now.ToString("dd-MM HH-mm");
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                saveDialog.FileName = "27LVL-ĐC " + tenfile + "-"+tongsp+"sp";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    duongdanfileexcel = Path.GetFullPath(saveDialog.FileName);
                    string exportFilePath = saveDialog.FileName;
                    var newFile = new FileInfo(exportFilePath);
                    using (var package = new ExcelPackage(newFile))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("27LVL_Điều chuyển");

                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                       
                        worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = "Tổng sản phẩm:";
                        worksheet.Cells[worksheet.Dimension.End.Row, 3].Value =Int32.Parse(tongsp);
                        worksheet.Column(1).AutoFit();
                        worksheet.Column(2).AutoFit();
                        worksheet.PrinterSettings.LeftMargin = 0.2M / 2.54M;
                        worksheet.PrinterSettings.RightMargin = 0.2M / 2.54M;
                        package.Save();

                    }
                }
            }
        }
        #endregion

        #region hang ra vao
        public string layngay()
        {
            return DateTime.Now.ToString("ddMMyyyy");
        }
        public string laygio()
        {
            return DateTime.Now.ToString("HHmm");
        }
        public void xuatfileexcelhangravao(DataTable dt, string tenfile)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                saveDialog.FileName = tenfile;
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    duongdanfileexcel = Path.GetFullPath(saveDialog.FileName);
                    string exportFilePath = saveDialog.FileName;
                    var newFile = new FileInfo(exportFilePath);
                    using (var package = new ExcelPackage(newFile))
                    {
                       
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(tenfile);
                        
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Column(1).AutoFit();
                        worksheet.Column(2).AutoFit();
                        worksheet.Column(3).AutoFit();
                        package.Save();

                    }
                }
            }
        }
        public void mofileexcelhangravao()
        {
            if (duongdanfileexcel != null)
            {
                var app = new excel.Application();
                app.Visible = true;
                excel.Workbooks book = app.Workbooks;
                excel.Workbook sh = book.Open(duongdanfileexcel);
                //sh.PrintOutEx();
            }

        }
        #endregion
        #region tab khuyenmai
        public string doisangdonvitien(double sotien)
        {
            return string.Format("{0:0,0 đ}", sotien);
        }
        public string doisangphantramgiam(double giagiam)
        {
            return string.Format("Giảm {0:0.0}%", giagiam);
        }
        public DataTable themvaobangtam(DataTable dt ,string matong,string giagoc,string giacuoicung, string giagiam)
        {
            DataRow row = dt.NewRow();
            row[0] = matong;
            row[1] = giagoc;
            row[2] = giacuoicung;
            row[3] = giagiam;
            dt.Rows.Add(row);
            dt.AcceptChanges();
            return dt;
        }

        #endregion
        #region tab tim kiem
        public void xuatfileexceltabtimkiem(DataTable dt, string tenfile)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (.xlsx)|*.xlsx";
                saveDialog.FileName = tenfile;
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    duongdanfileexcel = Path.GetFullPath(saveDialog.FileName);
                    string exportFilePath = saveDialog.FileName;
                    var newFile = new FileInfo(exportFilePath);
                    using (var package = new ExcelPackage(newFile))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(tenfile);

                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Column(1).AutoFit();
                        worksheet.Column(2).AutoFit();
                        worksheet.Column(3).AutoFit();
                        package.Save();

                    }
                }
            }
        }
        #endregion
    }
}
