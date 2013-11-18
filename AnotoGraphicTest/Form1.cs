using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AnotoGraphicTest
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp,imgbmp;
        Brush br;
        Pen p;
        int x1 = 20;
        int y1 = 20;
        int x = 500;
        int y = 500;

        public Form1()
        {
            InitializeComponent();

            g = this.CreateGraphics();
            bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.Black);
            br = new SolidBrush(Color.Blue);
            p = new Pen(br,5);
        }

        void drawText()
        {
            for (int i = x1; i < x; i += x1)
            {
                for (int j = y1; j < y; j += y1)
                {
                    g.DrawImageUnscaled(bmp, i, j);
                }
            }

            double[] xx = new double[]{
                09.875,
                10.375,
                10.375,
                11.125,
                14.875,
                17.25,
                16.75,
                16.875,
                17.375,
                17.625,
                18.25,
                18.125,
                18.125,

                08.375,
                08.5,
                10.125,
                13.625,
                23.375,
                26.0,
                25.625


            };
            double[] yy = new double[]{
                10.625,
                10.375,
                10.5,
                09.875,
                05.125,
                00.875,
                04.875,
                13.75,
                16.675,
                28.375,
                34.25,
                33.875,
                33.0,

                34.875,
                34.875,
                35.0,
                34.875,
                32.875,
                33.0,
                32.875
            };

            // List版
            List<double> listtmpx = new List<double>();
            List<List<double>> listx = new List<List<double>>();
            List<double> listtmpy = new List<double>();
            List<List<double>> listy = new List<List<double>>();

            listtmpx.Add(09.875);
            listtmpx.Add(10.375);
            listtmpx.Add(10.375);
            listtmpx.Add(11.125);
            listtmpx.Add(14.875);
            listtmpx.Add(17.25);
            listtmpx.Add(16.75);
            listtmpx.Add(16.875);
            listtmpx.Add(17.375);
            listtmpx.Add(17.625);
            listtmpx.Add(18.25);
            listtmpx.Add(18.125);
            listtmpx.Add(18.125);
            listx.Add(listtmpx);
            
            // もう一回初期化
            listtmpx = new List<double>();
            listtmpx.Add(08.375);
            listtmpx.Add(08.5);
            listtmpx.Add(10.125);
            listtmpx.Add(13.625);
            listtmpx.Add(23.375);
            listtmpx.Add(26.0);
            listtmpx.Add(25.625);
            listx.Add(listtmpx);

            listtmpy.Add(10.625);
            listtmpy.Add(10.375);
            listtmpy.Add(10.5);
            listtmpy.Add(09.875);
            listtmpy.Add(05.125);
            listtmpy.Add(00.875);
            listtmpy.Add(04.875);
            listtmpy.Add(13.75);
            listtmpy.Add(16.675);
            listtmpy.Add(28.375);
            listtmpy.Add(34.25);
            listtmpy.Add(33.875);
            listtmpy.Add(33.0);
            listy.Add(listtmpy);

            listtmpy = new List<double>();
            listtmpy.Add(34.875);
            listtmpy.Add(34.875);
            listtmpy.Add(35.0);
            listtmpy.Add(34.875);
            listtmpy.Add(32.875);
            listtmpy.Add(33.0);
            listtmpy.Add(32.875);
            listy.Add(listtmpy);


            /*
            for (int i = 0; i < xx.Length-1; i++)
            {
                //e.Graphics.DrawImageUnscaled(b, (int)xx[i]*8, (int)yy[i]*8);
                //e.Graphics.FillRectangle(br, (int)xx[i]*8, (int)yy[i]*8, 5, 5);
                e.Graphics.DrawLine(p, (int)xx[i] * 8, (int)yy[i] * 8, (int)xx[i + 1] * 8, (int)yy[i+1] * 8);
            }
             */

            for (int i = 0; i < listy.Count; i++)
            {
                for (int j = 0; j < listy[i].Count - 1; j++)
                {
                    g.DrawLine(p, (int)listx[i][j] * 8, (int)listy[i][j] * 8, (int)listx[i][j + 1] * 8, (int)listy[i][j + 1] * 8);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // 動的に書くものなくなっちゃった。
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            //ofd.FileName = "default.html";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "アノトストロークファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            //ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            //ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            //ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            //ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                XmlSerialize(ofd.FileName);
            }
        }


        public void XmlSerialize(String path)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            for (int i = 0; i < ds.Tables["sp"].Rows.Count; i++)
            {
                Console.WriteLine("sp["+i+"] x:"+ds.Tables["sp"].Rows[i][2]+"y:"+ds.Tables["sp"].Rows[i][3]);
            }

            // 1画の座標数
            for (int i = 0; i < ds.Tables["stroke"].Rows.Count; i++)
            {
                Console.WriteLine("stroke[" + i + "]:" + ds.Tables["stroke"].Rows[i][0]);
            }

            
            List<List<double>> listx = new List<List<double>>();
            List<List<double>> listy = new List<List<double>>();
            int strokeCount = 0;

            for (int i = 0; i < ds.Tables["stroke"].Rows.Count; i++)
            {
                List<double> listtmpx = new List<double>();
                List<double> listtmpy = new List<double>();

                for (int j = 0; j < int.Parse(ds.Tables["stroke"].Rows[i][0].ToString()); j++)
                {
                    listtmpx.Add(double.Parse(ds.Tables["sp"].Rows[strokeCount][2].ToString()));
                    listtmpy.Add(double.Parse(ds.Tables["sp"].Rows[strokeCount][3].ToString())-800);
                    Console.WriteLine("x:" + listtmpx[j]+" y:"+listtmpy[j]);
                    strokeCount++;
                }
                listx.Add(listtmpx);
                listy.Add(listtmpy);
            }

            for (int i = 0; i < listy.Count; i++)
            {
                for (int j = 0; j < listy[i].Count - 1; j++)
                {
                    g.DrawLine(p, (int)listx[i][j]*4 , (int)listy[i][j]*4 , (int)listx[i][j + 1]*4 , (int)listy[i][j + 1]*4 );
                }
            }



            /*
            XmlReader xmlReader = XmlReader.Create(path);

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.LocalName)
                    {
                        case "sample":
                            System.Console.WriteLine("type" + xmlReader.ReadString());
                            break;
                        case "name":
                            System.Console.WriteLine("name" + xmlReader.ReadString());
                            break;
                        case "locality":
                            System.Console.WriteLine("locality" + xmlReader.ReadString());
                            break;
                        case "weight":
                            System.Console.WriteLine("weight" + xmlReader.ReadString());
                            break;
                    }
                }
            }
            */

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            //ofd.FileName = "default.html";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "アノトストロークファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            //ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            //ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            //ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            //ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgbmp = new Bitmap(ofd.FileName);  // ファイルパスから格納
                pictureBox1.Image = imgbmp; // ピクチャーボックスに表示
                g = Graphics.FromImage(imgbmp);
                drawText();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                //Console.WriteLine(sfd.FileName);

                System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();
      
         this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
         fs.Close();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //送信者
            msg.From = new System.Net.Mail.MailAddress("sender@xxx.xxx");
            //宛先
            msg.To.Add(new System.Net.Mail.MailAddress("recipient@xxx.xxx"));
            //.NET Framework 3.5以前では、以下のようにする
            //msg.ReplyTo = new System.Net.Mail.MailAddress("replyto@xxx.xxx");
            //Sender
            msg.Sender = new System.Net.Mail.MailAddress("master@xxx.xxx");

            //件名
            msg.Subject = "こんにちは";
            //本文
            msg.Body = "こんにちは。\r\n\r\nそれではまた。";

            //メールの配達が遅れたとき、失敗したとき、正常に配達されたときに通知する
            msg.DeliveryNotificationOptions =
                System.Net.Mail.DeliveryNotificationOptions.Delay |
                System.Net.Mail.DeliveryNotificationOptions.OnFailure |
                System.Net.Mail.DeliveryNotificationOptions.OnSuccess;

            //"C:\test\1.gif"を添付する
            System.Net.Mail.Attachment attach1 =
                new System.Net.Mail.Attachment("C:\\test\\1.gif");
            msg.Attachments.Add(attach1);

            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
            //SMTPサーバーなどを設定する
            sc.Host = "localhost";
            sc.Port = 25;
            sc.Host = "smtp.gmail.com";
            sc.Port = 587;
            //GMail認証
            sc.Credentials = new System.Net.NetworkCredential("ID", "pass");
            //SSL
            sc.EnableSsl = true;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //メッセージを送信する
            sc.Send(msg);

            //後始末
            msg.Dispose();
            //後始末（.NET Framework 4.0以降）
            sc.Dispose();
        }
        
    }
}
