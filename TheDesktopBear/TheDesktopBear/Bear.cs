using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace TheDesktopBear
{

    public partial class Bear : Form
    {

        private Point mousePoint;
        Image[,] images = new Image[4, 4];
        int speed = 8;
        bool mouse_control = false;
        int moving = 0;

        int width = Screen.PrimaryScreen.Bounds.Width;
        int height = Screen.PrimaryScreen.Bounds.Height;

        enum BearMove { FRONT, RIGHT, BACK, LEFT, STAND };

        private int move_num = -1; //이미지갱신을 위한 tick
        private int dir = (int)BearMove.FRONT; //방향

        public Bear()
        {
            InitializeComponent();
            LoadImage();
            MoveTimer.Interval = 500;
            MoveTimer.Start();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

        }
        void LoadImage()
        {
            #region Front Image

            images[0, 0] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Front1.png");
            images[0, 1] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Front2.png");
            images[0, 2] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Front3.png");
            images[0, 3] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Front4.png");
            #endregion
            #region Right Image
            images[1, 0] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Right1.png");
            images[1, 1] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Right2.png");
            images[1, 2] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Right3.png");
            images[1, 3] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Right4.png");
            #endregion
            #region Back Image
            images[2, 0] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Back1.png");
            images[2, 1] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Back2.png");
            images[2, 2] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Back3.png");
            images[2, 3] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Back4.png");
            #endregion
            #region Left Image
            images[3, 0] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Left1.png");
            images[3, 1] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Left2.png");
            images[3, 2] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Left3.png");
            images[3, 3] = Image.FromFile(Application.StartupPath + "\\..\\..\\resource\\img\\Left4.png");
            #endregion
        }

        #region 캐릭터로 폼움직이기

        private void Character_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }
        // 마우스 클릭시 먼저 선언된 mousePoint변수에 현재 마우스 위치값이 들어갑니다.

        private void Character_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }
        #endregion

        #region 파일 드래그앤 드롭
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        //데이터를 보낼 대상이 되는 곰의 ip입니다.
        public static string serverIP = "223.194.44.37";

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string filePath = getFilePath((string[])e.Data.GetData(DataFormats.FileDrop, false));

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse(serverIP), 7000);

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int fileLength = (int)fs.Length;
            byte[] buffer = BitConverter.GetBytes(fileLength);
            socket.Send(buffer);

            int count = fileLength / 1024 + 1;
            BinaryReader reader = new BinaryReader(fs);
            for (int i = 0; i < count; i++)
            {
                buffer = reader.ReadBytes(1024);
                socket.Send(buffer);
            }
            reader.Close();
            socket.Close();
        }
        #endregion

        #region 움직임
        private void Moving(int direction, int idx)
        {
            switch (direction)
            {
                case (int)BearMove.FRONT:
                    Location = new Point(this.Location.X, this.Location.Y + speed);
                    Character.Image = images[dir, idx];
                    break;
                case (int)BearMove.RIGHT:
                    Location = new Point(this.Location.X + speed, this.Location.Y);
                    Character.Image = images[dir, idx];
                    break;
                case (int)BearMove.BACK:
                    Location = new Point(this.Location.X, this.Location.Y - speed);
                    Character.Image = images[dir, idx];
                    break;
                case (int)BearMove.LEFT:
                    Location = new Point(this.Location.X - speed, this.Location.Y);
                    Character.Image = images[dir, idx];
                    break;
                default:
                    break;
            }
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            Image now = Character.Image;

            move_num++; move_num %= 4;

            if (moving == 0)
            {
                Random r = new Random();
                moving = r.Next(10, 20);
                dir = r.Next(0, 4);
            }
            else
            {
                Moving(dir, move_num);
                moving--;
            }
            if (mouse_control)
            {
                Cursor.Position = new Point(this.Location.X + 45, this.Location.Y + 79);
            }
        }
        #endregion

        #region 행동
        private void CharacterKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                ExitTimer.Start();
            }
            else if (e.KeyCode == Keys.A)// 울음소리
            {
                Thread t = new Thread(new ThreadStart(Howling));
                t.Start();
            }
            else if (e.KeyCode == Keys.S)// 창 최소화
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }
        void Howling()
        {
            SoundPlayer wp = new SoundPlayer("../../resource/sound/growl.wav");
            wp.PlaySync();
        }

        private void CharacterKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Escape:
                    ExitTimeDisplay.Text = (int.Parse(ExitTimeDisplay.Text) + 1).ToString();
                    break;
                default:
                    ExitTimeDisplay.Text = "0";
                    break;
            }
        }
        private void CharacterKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ExitTimer.Stop();
                ExitTimeDisplay.Text = "0";

                ExitTimer.Dispose();
            }
        }
        private void ExitTimeDisplay_TextChanged(object sender, EventArgs e)
        {
            if (ExitTimeDisplay.Text.Equals("10"))
            {
                this.Dispose();
                this.Close();
            }

        }
        #endregion

        private void 멈추기SToolStripMenuItem_Click(object sender, EventArgs e)    {
            if (dir != 4) dir = 4;
            else dir = 0;
        }

        private void 파일전송하기SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //해당 User에게 수락/거절 메세지 보내기
            //수락시 파일전송창 띄워주기
            MessageBox.Show("파일을 전송하겠습니까");
        }

        private void 프로세스죽이기KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 마우스따라가기MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mouse_control = !mouse_control;
        }

        private void 파일수신하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint point = new IPEndPoint(IPAddress.Parse(getLocalIP()), 7000);

            mySocket.Bind(point);

            mySocket.Listen(100);

            mySocket = mySocket.Accept();

            //파일 크기를 저장할 버퍼
            byte[] buffer = new byte[4];

            //클라이언트로부터 파일 크기 수신
            mySocket.Receive(buffer);

            //파일 크기를 정수로 변환, fileLength에 저장
            int fileLength = BitConverter.ToInt32(buffer, 0);

            //버퍼 크기 새로 지정
            buffer = new byte[1024];

            int totalLength = 0;

            FileStream fileStr = new FileStream("123.txt", FileMode.Create, FileAccess.Write);

            //받을 데이터를 파일에 쓰기 위해 BinaryWriter 객체 생성
            BinaryWriter bnryWriter = new BinaryWriter(fileStr);

            //파일 수신
            while (totalLength < fileLength)
            {
                //받을 데이터 길이 저장
                int receiveLength = mySocket.Receive(buffer);

                //받은 데이터를 fileStr에 씀
                bnryWriter.Write(buffer, 0, receiveLength);

                totalLength += receiveLength;
            }
            bnryWriter.Close();
            mySocket.Close();
        }

        #region 자신의 localIP 리턴함수
        private string getLocalIP()
        {
            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        #endregion

        private string getFilePath(string[] fileArg)
        {
            string[] filePathsArray = fileArg;
            string filePath = "";
            foreach (string temp in filePathsArray)
            {
                filePath += temp;
            }
            return filePath;
        }

        private void 분신술CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bear f = new Bear();
            f.Show();
        }

        private void 광고AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] url = new String[3];
            url[0] = ("https://www.youtube.com/watch?v=od6DsQfD9qM&feature=youtu.be");
            url[1] = ("https://github.com/501Pb/DJ_Keyboard");
            url[2] = ("https://github.com/Team-TDB/TheDesktopBear");

            Random r = new Random();

            System.Diagnostics.Process.Start(url[r.Next(0,3)]);
        }
    }
}
