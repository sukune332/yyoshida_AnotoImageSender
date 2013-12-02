﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using AForge.Video.FFMPEG;
using System.Drawing.Imaging;

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
        int xx = 20;
        int yy = 20;
        int ii = 0;
        List<List<double>> listx;
        List<List<double>> listy;
        VideoFileWriter writer;

        public Form1()
        {
            // GUIを初期化してくれる公式のメソッド。ありがたい。
            InitializeComponent();

            // 一番のベースとなるGraphicを初期化する
            g = this.CreateGraphics();
            g.PageUnit = GraphicsUnit.Inch;
            //g.PageScale = 0.01f;

            // グリッド表示用の1点を作成
            bmp = new Bitmap(1, 1);
            // 色を設定
            bmp.SetPixel(0, 0, Color.Black);
            
            // ペンのためのBrushを初期化（ペンのインク）
            br = new SolidBrush(Color.Blue);
            // 筆跡を描くためのペンを初期化（ペン）
            p = new Pen(br,5);

            writer = new VideoFileWriter();
        }

        void drawGrid()
        {
            // グリッド表示
            for (int i = x1; i < pictureBox1.Width; i += x1)
            {
                for (int j = y1; j < pictureBox1.Height; j += y1)
                {
                    g.DrawImageUnscaled(bmp, i, j);
                }
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // 動的に描くものなくなっちゃった。

            // グリッド表示
            for (int i = x1; i < pictureBox1.Width; i += x1)
            {
                for (int j = y1; j < pictureBox1.Height; j += y1)
                {
                    e.Graphics.DrawImageUnscaled(bmp, i, j);
                }
            }
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
            // XMLパースのためのDataSetを作成
            DataSet ds = new DataSet();
            // DataSetに用意されているXMLパースのメソッドを使って読み込む
            ds.ReadXml(path);

            // spタグの部分を全部抜き出す
            for (int i = 0; i < ds.Tables["sp"].Rows.Count; i++)
            {
                Console.WriteLine("sp["+i+"] x:"+ds.Tables["sp"].Rows[i][2]+"y:"+ds.Tables["sp"].Rows[i][3]);
            }

            // strokeタグの部分を全部抜き出す。strokeは1画の座標数。
            for (int i = 0; i < ds.Tables["stroke"].Rows.Count; i++)
            {
                Console.WriteLine("stroke[" + i + "]:" + ds.Tables["stroke"].Rows[i][0]);
            }

            // 格納するベースのListを作成
            listx = new List<List<double>>();
            listy = new List<List<double>>();
            int strokeCount = 0;

            // ストローク数だけループ
            for (int i = 0; i < ds.Tables["stroke"].Rows.Count; i++)
            {
                // 1ストローク用Listの中身を初期化
                List<double> listtmpx = new List<double>();
                List<double> listtmpy = new List<double>();

                // 座標情報数だけループ
                for (int j = 0; j < int.Parse(ds.Tables["stroke"].Rows[i][0].ToString()); j++)
                {
                    // spを取得
                    listtmpx.Add(double.Parse(ds.Tables["sp"].Rows[strokeCount][2].ToString()));
                    listtmpy.Add(double.Parse(ds.Tables["sp"].Rows[strokeCount][3].ToString())-800);
                    Console.WriteLine("x:" + listtmpx[j]+" y:"+listtmpy[j]);
                    strokeCount++;
                }
                // ベースのListに追加
                listx.Add(listtmpx);
                listy.Add(listtmpy);
            }
        }

        private void draw()
        {
            writer.Open("anoto.avi", 900, 600, 10, VideoCodec.MPEG4);

            // お絵かき
            for (int i = 0; i < listy.Count; i++)
            {
                for (int j = 0; j < listy[i].Count - 1; j++)
                {
                    g.DrawLine(p, (int)listx[i][j] * 4, (int)listy[i][j] * 4, (int)listx[i][j + 1] * 4, (int)listy[i][j + 1] * 4);
                    Bitmap image = new Bitmap(900, 600, PixelFormat.Format24bppRgb);
                    image = imgbmp;
                    writer.WriteVideoFrame(image);
                    System.Threading.Thread.Sleep(100);
                    pictureBox1.Refresh();
                }
            }

            writer.Close();
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
                "すべてのファイル(*.*)|*.*";
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
                draw(); // グラフィックに画像を表示した後に描く！
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

                // ダイアログで作ったパス・ファイル名のファイルストリームを作成
                System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();
                // ファイルストリームに画像形式(JPEG)で流し込む
                this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                // ファイルストリームを閉じてファイルを作成
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            
        }
    }
}
