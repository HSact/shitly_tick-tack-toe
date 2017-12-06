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
using System.IO;

namespace XO
{
    public partial class Form1 : Form
    {
        bool turn = true; //true = ход X, false = ход О
        int turn_count = 0;
        private bool isServer;
        public int IntField { get; set; }
        public decimal DecimalField { get; set; }
        
        private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private static IPAddress cntAddr;
        private static int port = 201;
        TcpListener server = new TcpListener(localAddr, port);
        TcpClient tcpClient = new TcpClient();
        private string cntIP;
        struct turnSendT
        {
            public string point;
            public string vlue;
            public int cnt;
        }
        private turnSendT turnSend;
        
        public Form1(bool isServer, string cntIP)
        {
            this.isServer = isServer;
            InitializeComponent();
            turnSend.cnt = 0;
            if (isServer)
            {
                //MessageBox.Show("Ya server");
                server.Start();

                //turnSend.cnt = 1;
            }
            else
            {
                this.cntIP = cntIP;
                cntAddr = IPAddress.Parse(cntIP);
                
                //MessageBox.Show("Ya klient!");
                
                NetworkStream netStream = tcpClient.GetStream();
                //tcpClient.Close();
                //tcpClient.Connect(cntAddr, port);
                try
                {
                    tcpClient.Connect(cntAddr, port);
                }
                catch (System.Net.Sockets.SocketException) 
                {
                    MessageBox.Show("Неудалось подключиться!");
                    GoToMenu();
                }
               
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Слеплено студентами НТ-201: Алышевым Антоном и Китаевым Александром", "Игра Крестики-Нолики");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void newGameToolStripMenuItem_Click (object sender, EventArgs e)
        {
            //MessageBox.Show("Вы уверены в том, что хотите начать новую игру!");
            GoToMenu();
        }
        private void GoToMenu ()
        {
            server.Stop();
            tcpClient.Close();
            Hide();
            Form2 frm = new Form2();
            frm.ShowDialog();
            Close();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
                b.Text = "X";
            else
                b.Text = "O";

            turn = !turn;
            b.Enabled = false;
            turn_count++;
            turnSend.cnt++;
            turnSend.point = b.Name;
            turnSend.vlue = b.Text;
            if (!isServer)
            {
                //byte[] data = System.Text.Encoding.UTF8.ToBytes(turnSend);
                //tcpClient.Write(data, 0, data.Length);
            }
            else
            {

            }
            check();
        }

        private void check()
        {
            bool victory = false;

            //победа по горизонтали
            if ((a1.Text == a2.Text) && (a2.Text == a3.Text) && (!a1.Enabled))
            {
                victory = true;
            }
            else if ((b1.Text == b2.Text) && (b2.Text == b3.Text) && (!b1.Enabled))
            {
                victory = true;
            }
            else if ((c1.Text == c2.Text) && (c2.Text == c3.Text) && (!c1.Enabled))
            {
                victory = true;
            }

            //победа по вертикали
            if ((a1.Text == b1.Text) && (b1.Text == c1.Text) && (!a1.Enabled))
            {
                victory = true;
            }
            else if ((a2.Text == b2.Text) && (b2.Text == c2.Text) && (!a2.Enabled))
            {
                victory = true;
            }
            else if ((a3.Text == b3.Text) && (b3.Text == c3.Text) && (!a3.Enabled))
            {
                victory = true;
            }

            //победа по диагонали
            if ((a1.Text == b2.Text) && (b2.Text == c3.Text) && (!a1.Enabled))
            {
                victory = true;
            }
            else if ((a3.Text == b2.Text) && (b2.Text == c1.Text) && (!a3.Enabled))
            {
                victory = true;
            }

            if (victory == true) //объявление победителя
            {
               // DisableButtons();

                string winner = "";
                if (turn)
                    winner = "O ";
                else
                    winner = "X ";
                MessageBox.Show(winner + "Победил!", "Ура!");
                GoToMenu();
            }
            else
            {
                if (turn_count == 9)
                {
                    MessageBox.Show("Ничья", "Попробуйте еще");
                    GoToMenu();
                }
                        
            }
        }

        /*private void DisableButtons() //выключение кнопок при победе
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }*/
        /*public override byte[] ToBytes()
        {
            //вычисляем длину команды
            const int messageLenght = sizeof(int) + sizeof(decimal);

            //инициализируем массив байт в который будут сохраняться данные
            var messageData = new byte[messageLenght];
            using (var stream = new MemoryStream(messageData))
            {
                //записываем по очереди наши свойства
                var writer = new BinaryWriter(stream);
                writer.Write(IntField);
                writer.Write(DecimalField);
                return messageData;
            }
        }*/
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
