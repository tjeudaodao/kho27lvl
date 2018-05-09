using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace canifa
{
    class data
    {
        public SQLiteConnection con;
        public SQLiteConnection con1;
        ham ham = new ham();
        #region Khởi tạo csdl
        public data()
        {
            con = new SQLiteConnection(@"Data source=datacanifa.db;version=3;new=false");
            con1= new SQLiteConnection(@"Data source=dataluutru.db;version=3;new=false");
        }
        public void modt()
        {
            if (con.State!=ConnectionState.Open)
            {
                con.Open();
            }
        }
        public void dongdt()
        {
            if (con.State!=ConnectionState.Closed)
            {
                con.Close();
            }
        }
        public void modt1()
        {
            if (con1.State != ConnectionState.Open)
            {
                con1.Open();
            }
        }
        public void dongdt1()
        {
            if (con1.State != ConnectionState.Closed)
            {
                con1.Close();
            }
        }
        #endregion
        #region Các hàm thao tác với csdl tab kiem hang
        public string kiemtracondonhangdangkiemkhong()
        {
            string sql = "select ngay,gio,sophieu from bangtam";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string cohaykhong = null;
            while (dtr.Read())
            {
                cohaykhong = dtr[0].ToString();
                if (cohaykhong != null)
                {
                    cohaykhong = "Ngày: " + dtr[0].ToString() + " - Giờ: " + dtr[1].ToString() + " - Số phiếu: " + dtr[2].ToString();
                }

            }
            dongdt();
            return cohaykhong;
        }

        public string laymasp(string barcode)
        {
            string sql = "select masp from data where barcode='" + barcode + "'";
            string masp=null; 
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
               masp = dtr[0].ToString();
            }
           
            dongdt();
            return masp;
        }
        public void insertvaodata(string barcode, string masp, string sl, string sophieu, string matong, string ngay,string gio)
        {
            
            string sql = "insert into bangtam(sophieu,ngay,barcode,masp,matong,soluong,gio) values('" + sophieu + "','" + ngay + "','" + barcode + "','" + masp + "','"+matong+"','" + sl + "','" + gio + "')";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void xoabangtam()
        {
            string sql = "delete from bangtam";
            string sql2 = "delete from sqlite_sequence where name='bangtam'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt();
        }
        public void xoabangtam2()
        {
            string sql = "delete from bangtam1";
            string sql2 = "delete from sqlite_sequence where name='bangtam1'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt();
        }
        public string tongkiemhang()
        {
            string sql = "select sum(soluong) from bangtam";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                 sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
        public string tongsoluongbang1()
        {
            string sql = "select sum(soluong1) from bangtam1";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
        public DataTable locdulieu1()
        {
            string sql = "select matong as 'Mã tổng thực tế', sum(soluong) as 'SL thực tế' from bangtam group by matong";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public void kiemtramamoi(string matong)
        {
            string sql = "select sum(soluong) from bangtam where matong='"+matong+"'";
            modt();
            string soluong = null;
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                soluong = dtr[0].ToString();
            }
            dongdt();
            if (soluong=="1")
            {
                ham.baomamoi();
            }
            else
            {
                return;
            }
        }
        public DataTable sosanhdulieu()
        {

          //  string sql = "select matong as 'Mã thực tế',tongsoluong as 'SL TT',matong1 as 'Mã theo đơn',tongsoluong1 as 'SL TĐ' from (select matong1,sum(soluong1) as tongsoluong1 from bangtam1 group by matong1) left join (select matong,sum(soluong) as tongsoluong from bangtam group by matong) on matong1=matong";
            string sql2 = "select matong as 'Mã thực tế',tongsoluong as 'SL TT',matong1 as 'Mã theo đơn',tongsoluong1 as 'SL TĐ' from	(select matong1,sum(soluong1) as tongsoluong1 from bangtam1 group by matong1) left join (select matong,sum(soluong) as tongsoluong from bangtam group by matong) on matong1=matong union all select matong as 'Mã thực tế',tongsoluong as 'SL TT',matong1 as 'Mã theo đơn',tongsoluong1 as 'SL TĐ' from (select matong,sum(soluong) as tongsoluong from bangtam group by matong) left join (select matong1,sum(soluong1) as tongsoluong1 from bangtam1 group by matong1) on matong1=matong where matong1 is null";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql2, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public void updatebangtam(string soluongmoi, string id)
        {
            string sql = "update bangtam set soluong='" + soluongmoi + "' where idrow='" + id + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void loadgv0(DataGridView dgv)
        {
            string sql = "select idrow as 'STT',barcode as 'Barcode',masp as 'Mã sản phẩm' ,soluong as 'SL' from bangtam";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        public void deletemasp(string id)
        {
            string sql = "delete from bangtam where idrow='" + id + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            modt();
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void laydataexcel(string matong,string sl)
        {
            string sql = "insert into bangtam1(matong1,soluong1) values('" + matong + "','" + sl + "')";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void savevaobangkiemhang()
        {
            string sql = "insert into kiemhang(sophieu,ngay,barcode,masp,matong,soluong,gio) select sophieu,ngay,barcode,masp,matong,soluong,gio from bangtam";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            
            cmd.ExecuteNonQuery();
            dongdt();
        }
        #endregion

        #region cac ham lien quan den tab chuyen hang

        public string kiemtracondonhangdangnhatkhong()
        {
            string sql = "select ngay,gio from bangtamchuyenhang";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string cohaykhong = null;
            while (dtr.Read())
            {
                cohaykhong = dtr[0].ToString();
                if (cohaykhong!=null)
                {
                    cohaykhong = "Ngày: " + dtr[0].ToString() + " - Giờ: " + dtr[1].ToString();
                }
                
            }
            dongdt();
            return cohaykhong;
        }
        public string tinhtrang(string masp)
        {
            string ketqua = null;
            if (kiemtracotmatongbangchuyenhang1()!="0")
            {
                if (kiemtracotrongcotmatong(ham.laymatong(masp))!=null)
                {
                    ketqua = "Thiếu";
                }
                else if (kiemtracotrongcotmatong(ham.laymatong(masp)) == null)
                {
                    double soluongtt = laysoluongthucte(masp);
                    double soluongtd = laysoluongtudon(masp);

                    if (soluongtd == 0)
                    {
                        ketqua = "Ngoài đơn";
                    }
                    else if (soluongtt < soluongtd)
                    {
                        ketqua = "Thiếu";
                    }
                    else if (soluongtt == soluongtd)
                    {
                        ketqua = "Đủ";
                    }
                    else if (soluongtt > soluongtd)
                    {
                        ketqua = "Thừa";
                    }
                }
            }
            else if (kiemtracotmatongbangchuyenhang1() == "0")
            {
                double soluongtt = laysoluongthucte(masp);
                double soluongtd = laysoluongtudon(masp);

                if (soluongtd == 0)
                {
                    ketqua = "Ngoài đơn";
                }
                else if (soluongtt < soluongtd)
                {
                    ketqua = "Thiếu";
                }
                else if (soluongtt == soluongtd)
                {
                    ketqua = "Đủ";
                }
                else if (soluongtt > soluongtd)
                {
                    ketqua = "Thừa";
                }
            }
            
            return ketqua;
        }
        public void baoamthanh(string masp)
        {
            if (tinhtrang(masp)== "Ngoài đơn")
            {
                ham.baongoai();
            }
            else if (tinhtrang(masp) == "Thiếu")
            {
                ham.phatbaothieu();
            }
            else if (tinhtrang(masp) == "Đủ")
            {
                ham.phatbaodu();
            }
            else if (tinhtrang(masp) == "Thừa")
            {
                ham.phatbaothua();
            }
        }
        public void chenvaobangthuathieu(string masp)
        {
            string sl = laysoluongthucte(masp).ToString();
            string sql = null;
            if (kiemtracotrongbangthuathieu(masp)==null)
            {
                sql= "insert into bangthuathieu values('" + masp + "','" + sl + "','" + tinhtrang(masp) + "')";
            }
            else if(kiemtracotrongbangthuathieu(masp)!=null)
            {
                sql= "update bangthuathieu set soluong='" + sl + "',tinhtrang='" + tinhtrang(masp) + "' where masp='" + masp + "'";
            }
           
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void updatebangthuathieu(string masp)
        {
            string sl = laysoluongthucte(masp).ToString();
            string sql = null;
            if (sl=="0")
            {
                sql = "delete from bangthuathieu where masp='" + masp + "'";
            }
            else
            {
                sql= "update bangthuathieu set soluong='" + sl + "',tinhtrang='" + tinhtrang(masp) + "' where masp='" + masp + "'";
            }
         
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void updatebangthuathieukhichendon(DataGridView dtv)
        {
            if (dtv.RowCount < 1)
            {
                return;
            }
            for (int i = 0; i <= dtv.RowCount - 1; i++)
            {
               string masp = dtv.Rows[i].Cells[0].Value.ToString();
               string sql = "update bangthuathieu set tinhtrang='" + tinhtrang(masp) + "' where masp='" + masp + "'";
                modt();
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                dongdt();
            }
            dtv.DataSource = laydulieubangthuathieu();
        }
        public string kiemtracotrongbangthuathieu(string masp)
        {
            string sql = "select masp from bangthuathieu where masp='" + masp + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string cohaykhong = null;
            while (dtr.Read())
            {
                cohaykhong = dtr[0].ToString();
            }
            dongdt();
            return cohaykhong;
        }
        public string kiemtracotrongcotmatong(string matong)
        {
            string sql = "select matong from bangtamchuyenhang1 where matong='" + matong + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string cohaykhong = null;
            while (dtr.Read())
            {
                cohaykhong = dtr[0].ToString();
            }
            dongdt();
            return cohaykhong;
        }
        public string kiemtracotmatongbangchuyenhang1()
        {
            string sql = "select count(matong) from bangtamchuyenhang1";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string sl1 = null;
            while (dtr.Read())
            {
                sl1 = dtr[0].ToString();
            }
            if (sl1 == null)
            {
                sl1 = "0";
            }
            dongdt();
            return sl1;
        }

        public void insertdl1(string barcode,string masp, string sl,string ngay,string gio)
        {
            string sql = "insert into bangtamchuyenhang(barcode,masp,soluong,ngay,gio) values('" + barcode + "','" + masp + "','" + sl + "','" + ngay + "','" + gio + "')";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            cmd.ExecuteNonQuery();
            dongdt();
        }
        public DataTable laydulieubangthuathieu()
        {
            string sql = "select masp as 'Mã thực tế', sum(soluong) as 'SL TT',tinhtrang as 'Tình trạng' from bangthuathieu group by masp";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public DataTable laybangdein()
        {
            string sql = "select masp as 'Mã thực tế', sum(soluong) as 'SL TT' from bangthuathieu group by masp";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public DataTable laybangxuatchuyenhang()
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã thực tế', sum(soluong) as 'Số lượng' from bangtamchuyenhang group by masp";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public string kiemtracotrongdon(string masp)
        {
            string sql = "select masp from bangtamchuyenhang1 where masp='" + masp + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string cohaykhong = null;
            while (dtr.Read())
            {
                cohaykhong = dtr[0].ToString();
            }
            dongdt();
            return cohaykhong;
        }
        public string demsoluongsptrongbangtam1()
        {
            string sql = "select count(*) from bangtamchuyenhang1";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql,con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string sl1 = null;
            while (dtr.Read())
            {
                sl1 = dtr[0].ToString();
            }
            if (sl1==null)
            {
                sl1 = "0";
            }
            dongdt();
            return sl1;
        }
        public double laysoluongthucte(string masp)
        {
            string sql = "select sum(soluong) from bangtamchuyenhang where masp='" + masp + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql,con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string sl1 = null;
            while (dtr.Read())
            {
                sl1 = dtr[0].ToString();
            }
            dongdt();
            return ham.ConvertToDouble(sl1);
        }
        public double laysoluongtudon(string masp)
        {
            string sql = "select sum(soluong1) from bangtamchuyenhang1 where masp='" + masp + "'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql,con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            string sl1 = null;
            while (dtr.Read())
            {
                sl1 = dtr[0].ToString();
            }
            dongdt();
            return ham.ConvertToDouble(sl1);
        }
        public string tongsoluongcannhat()
        {
            string sql = "select sum(soluong1)+sum(soluong2) from bangtamchuyenhang1";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
          
        public void loadvaodatag1(DataGridView dgv)
        {
            string sql = "select idrow as 'STT',barcode as 'Barcode',masp as 'Mã sản phẩm' ,soluong as 'SL' from bangtamchuyenhang";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        public string tongcheckhang()
        {
            string sql = "select sum(soluong) from bangtamchuyenhang";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
        public void xoabangtamchuyenhang()
        {
            string sql = "delete from bangtamchuyenhang";
            string sql2 = "delete from sqlite_sequence where name='bangtamchuyenhang'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt();
        }
        public void xoabangtamchuyenhang1()
        {
            string sql = "delete from bangtamchuyenhang1";
            string sql2 = "delete from sqlite_sequence where name='bangtamchuyenhang1'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt();
        }
        public void xoabangthuathieu()
        {
            string sql = "delete from bangthuathieu";
            string sql2 = "delete from sqlite_sequence where name='bangthuathieu'";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt();
        }
        public void deletemaspchuyenhang(string id)
        {
            string sql = "delete from bangtamchuyenhang where idrow='" + id + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            modt();
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void savevaobangchuyenhang(string ngay,string gio)
        {
            string sqlupdate = "update bangtamchuyenhang set ngay='" + ngay + "',gio='" + gio + "'";
            string sql = "insert into chuyenhang(barcode,masp,soluong,ngay,gio) select barcode,masp,soluong,ngay,gio from bangtamchuyenhang";
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sqlupdate, con);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongdt();
        }
        #endregion


        #region lien quan den tab hang ra vao kho
        public void inserthangra(string barcode, string masp)
        {
            string sql = "insert into hangrakho values('"+ham.layngay()+"','"+ham.laygio()+"','" + barcode + "','" + masp + "','1')";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);

            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public void inserthangvao(string barcode, string masp)
        {
            string sql = "insert into hangvaokho values('" + ham.layngay() + "','" + ham.laygio() + "','" + barcode + "','" + masp + "','1')";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);

            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public DataTable hangra12h(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangrakho where gio<'1200' and gio>'0800' and ngay='"+ngaycanlay+"'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable hangra15h(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangrakho where gio<'1500' and gio>'1200' and ngay='" + ngaycanlay + "'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable hangra18h(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangrakho where gio<'1800'and gio>'1500' and ngay='" + ngaycanlay + "'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable hangra22h(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangrakho where gio<'2300'and gio>'1800'and ngay='" + ngaycanlay + "'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable hangvaokho(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangvaokho where ngay='" + ngaycanlay + "'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable hangrakho(string ngaycanlay)
        {
            string sql = "select barcode as 'Barcode',masp as 'Mã SP',soluong as 'SL' from hangrakho where ngay='" + ngaycanlay + "'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public string tonghangratrongngay(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangrakho where ngay='" + ngaycanlay + "'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string tonghangvaotrongngay(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangvaokho where ngay='" + ngaycanlay + "'";
            string sl=null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string tongra12h(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangrakho where ngay='" + ngaycanlay + "' and gio >'0800' and gio <'1200'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string tongra15h(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangrakho where ngay='" + ngaycanlay + "' and gio >'1200' and gio <'1500'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string tongra18h(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangrakho where ngay='" + ngaycanlay + "' and gio >'1500' and gio <'1800'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string tongra22h(string ngaycanlay)
        {
            string sql = "select sum(soluong) from hangrakho where ngay='" + ngaycanlay + "' and gio >'1800' and gio <'2300'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        #endregion

        #region tab lien quan den hang khuyen mai
        public string laygiagoc(string matong)
        {
            string sql = "select giagoc from khuyenmai where matong='" + matong + "'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string laygiagiam(string matong)
        {
            string sql = "select giagiam from khuyenmai where matong='" + matong + "'";
            string sl = null ;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt1();
            return sl;
        }
        public string laymotasanpham(string matong)
        {
            string sql = "select mota1,mota2,bst from mota where matong='" + matong + "'";
            string sl = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString()+" - "+ dtr[1].ToString()+" - "+dtr[2].ToString();
            }
            dongdt1();
            return sl;
        }
        public DataTable bangkhuyenmai()
        {
            string sql = "select matong as 'Mã tổng',giagoc as 'Giá gốc',giagiam as 'Số giảm' from khuyenmai";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        public DataTable loctheomatong(string matong)
        {
            string sql = "select matong as 'Mã tổng',giagoc as 'Giá gốc',giagiam as 'Số giảm' from khuyenmai where matong like '" + matong+"%'";
            modt1();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con1);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt1();
            return dt;
        }
        #endregion

        #region lien quan den tab tim kiem
        public DataTable loadbangkiemhang(string ngay)
        {
            string sql = "select sophieu as 'Số phiếu',ngay as 'Ngày',gio as 'Giờ',barcode as 'Barcode',masp as 'Mã chi tiết',matong as 'Mã tổng',soluong as 'Số lượng' from kiemhang where ngay ='" + ngay + "'";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public DataTable loadbangchuyenhanghang(string ngay)
        {
            string sql = "select ngay as 'Ngày',gio as 'Giờ',barcode as 'Barcode',masp as 'Mã chi tiết',soluong as 'Số lượng' from chuyenhang where ngay ='" + ngay + "'";
            modt();
            SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, con);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            dongdt();
            return dt;
        }
        public string laysoluongngaykiem(string ngay)
        {
            string sql = "select sum(soluong) from kiemhang where ngay='"+ngay+"'";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
        public string laysoluongngaychuyen(string ngay)
        {
            string sql = "select sum(soluong) from chuyenhang where ngay='" + ngay + "'";
            string sl = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sl = dtr[0].ToString();
            }
            dongdt();
            return sl;
        }
        #endregion
        /*
        #region tab cai dat
        public void xoabanglayngaycapnhat()
        {
            string sql = "delete from ngaycapnhat";
            string sql2 = "delete from sqlite_sequence where name='ngaycapnhat'";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con1);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt1();
        }
        public string kiemtramabarcode(string barcode)
        {
            string sql = "select barcode from data where barcode='" + barcode + "'";
            string masp = null;
            modt();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                masp = dtr[0].ToString();
            }
            dongdt();
            return masp;
        }
        public void insertvaodata(string barcode, string masp)
        {
            modt();
            string sql = " insert into data values('" + barcode + "','" + masp + "')";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
            dongdt();
        }
        public void capnhatngaychinhsua()
        {
            string ngay = DateTime.Now.ToString("dd/MM/yyyy");
            string gio = DateTime.Now.ToString("HH:mm");
            string sql = "insert into ngaycapnhat values('" + ngay + "','" + gio + "')";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public string layngaycapnhatgannhat()
        {
            string sql = "select ngay,gio from ngaycapnhat";
            string masp = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                masp = dtr[0].ToString()+"-"+dtr[1].ToString();
            }
            dongdt1();
            return masp;
        }
        // phan cap nhat gia khuen mai

        public void xoabanglayngaycapnhat1()
        {
            string sql = "delete from ngaycapnhat1";
            string sql2 = "delete from sqlite_sequence where name='ngaycapnhat1'";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteCommand cmd2 = new SQLiteCommand(sql2, con1);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            dongdt1();
        }
        public string kiemtramatong(string matong)
        {
            string sql = "select matong from khuyenmai where matong='"+matong+"'";
            string masp = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                masp = dtr[0].ToString();
            }
            dongdt1();
            return masp;
        }
        public void capnhatngaychinhsua1()
        {
            string ngay = DateTime.Now.ToString("dd/MM/yyyy");
            string gio = DateTime.Now.ToString("HH:mm");
            string sql = "insert into ngaycapnhat1 values('" + ngay + "','" + gio + "')";
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public void insertvaokhuyenmai(string matong, string giagoc,string sogiam)
        {
            modt1();
            string sql = " insert into khuyenmai values('" + matong + "','" + giagoc + "','" + sogiam + "')";
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public void capnhatbangkhuyenmai(string matong,string giagoc,string sogiam)
        {
            modt1();
            string sql = "update khuyenmai set giagoc='" + giagoc + "',giagiam='" + sogiam + "' where matong='" + matong + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            cmd.ExecuteNonQuery();
            dongdt1();
        }
        public string layngaycapnhatgannhat1()
        {
            string sql = "select ngay,gio from ngaycapnhat1";
            string masp = null;
            modt1();
            SQLiteCommand cmd = new SQLiteCommand(sql, con1);
            SQLiteDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                masp = dtr[0].ToString() + "-" + dtr[1].ToString();
            }
            dongdt1();
            return masp;
        }
        
        #endregion 
    */
    }

}


