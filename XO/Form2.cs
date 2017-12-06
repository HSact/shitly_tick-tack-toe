using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XO
{
    public partial class Form2 : Form
    {
        public bool isServer=false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isServer = true;
            Hide();
            Form1 frm = new Form1(isServer, null);
            frm.ShowDialog();
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //isServer = false;
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            TextBox textIP = textBox1;
            String cntIP = textIP.Text;
            try
            {
                IPAddress.Parse(cntIP);
                //MessageBox.Show("Пропарсил!");
                Hide();
                Form1 frm = new Form1(isServer, cntIP);
                frm.ShowDialog();
                Close();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Неверный формат IP адреса!");
            }
            catch (System.ObjectDisposedException)
            {
                Close();
            }
            
        }
    }
}
