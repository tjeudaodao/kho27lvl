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
    public partial class timkiemcaidat : UserControl
    {
        data dulieu = new data();
        ham ham = new ham();
        static string ngaymuontim = "";

        public timkiemcaidat()
        {
            InitializeComponent();
            lbghichu.Text = "Muốn xem thông tin ngày nào thì click chọn vào ngày đó tại bảng bên trên";
            ngaymuontim = DateTime.Now.ToString("dd/MM/yyyy");
            datag2.DataSource = dulieu.loadbangchuyenhanghang(ngaymuontim);
            datag1.DataSource = dulieu.loadbangkiemhang(ngaymuontim);
            lbsoluongkiemhang.Text = dulieu.laysoluongngaykiem(ngaymuontim);
            lbsoluongchuyenhang.Text = dulieu.laysoluongngaychuyen(ngaymuontim);
        }

        private void monthctrol_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                var month = sender as MonthCalendar;
                ngaymuontim = month.SelectionStart.ToString("dd/MM/yyyy");
                lbghichu.Text = "Đã chọn ngày:\n- " + month.SelectionStart.ToString("dd/MM/yyyy");
                datag2.DataSource = dulieu.loadbangchuyenhanghang(ngaymuontim);
                datag1.DataSource = dulieu.loadbangkiemhang(ngaymuontim);
                lbsoluongkiemhang.Text = dulieu.laysoluongngaykiem(ngaymuontim);
                lbsoluongchuyenhang.Text = dulieu.laysoluongngaychuyen(ngaymuontim);
            }
            catch (Exception)
            {

                lbghichu.Text = "Có vấn đề \n- Xem lại đi";
            }
        }

        private void btnxuatkiemhang_Click(object sender, EventArgs e)
        {
            try
            {
                lbghichu.Text = "-";
                ham.xuatfileexceltabtimkiem(dulieu.loadbangkiemhang(ngaymuontim), ngaymuontim);
                lbghichu.Text = "Đã xuất file tại:\n-->" + ham.layduongdan();
            }
            catch (Exception)
            {

                lbghichu.Text = "Có vấn đề xem lại đi";
            }
        }

        private void btnxuatchuyenhang_Click(object sender, EventArgs e)
        {
            try
            {
                lbghichu.Text = "-";
                ham.xuatfileexceltabtimkiem(dulieu.loadbangchuyenhanghang(ngaymuontim), ngaymuontim);
                lbghichu.Text = "Đã xuất file tại:\n-->"+ham.layduongdan();
            }
            catch (Exception)
            {

                lbghichu.Text = "Có vấn đề xem lại đi";
            }
        }
    }
}
