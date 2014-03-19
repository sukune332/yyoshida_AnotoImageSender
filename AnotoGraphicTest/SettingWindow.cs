using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnotoGraphicTest
{
    public partial class SettingWindow : Form
    {
        Form1 f1;

        public SettingWindow()
        {
            InitializeComponent();
        }

        public SettingWindow(Form1 fm1)
        {
            InitializeComponent();
            f1 = fm1;

            textBox_Mail.Text = f1.myMailAddress;
            textBox_ID.Text = f1.myMailID;
            textBox_Password.Text = f1.myMailPassword;
            textBox_SMTP.Text = f1.myMailServer;
            textBox_Mail2.Text = f1.toMailAddress;
            textBox_Message.Text = f1.toMailMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.myMailAddress = textBox_Mail.Text;
            f1.myMailID = textBox_ID.Text;
            f1.myMailPassword = textBox_Password.Text;
            f1.myMailServer = textBox_SMTP.Text;
            f1.toMailAddress = textBox_Message.Text;
            f1.toMailMessage = textBox_Message.Text;

            Properties.Settings.Default.myMailAddress = textBox_Mail.Text;
            Properties.Settings.Default.myMailID = textBox_ID.Text;
            Properties.Settings.Default.myMailPassword = textBox_Password.Text;
            Properties.Settings.Default.myMailServer = textBox_SMTP.Text;
            Properties.Settings.Default.toMailAddress = textBox_Message.Text;
            Properties.Settings.Default.toMailMessage = textBox_Message.Text;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
