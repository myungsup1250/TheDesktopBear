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
using System.Runtime.CompilerServices;

namespace TheDesktopBear
{

    public partial class Bear : Form
    {
        public static int childNum = 0;
        private Point mousePoint;
        Image[,] images = new Image[4, 4];

        static Socket recievemySocket;
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
        public Bear(int cnt) {
            InitializeComponent();
            LoadImage();
            MoveTimer.Interval = 500;
            MoveTimer.Start();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            name.Text = (++cnt).ToString();
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
        public static string targetIP = "";
        public static DragEventArgs dragEvent;

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            dragEvent = e;

            if (FindFriends.friendList.Count == 0)
            {
                Console.WriteLine("no friend!");
                return;
            }

            FriendListForm flf = new FriendListForm();
            flf.Show();
        }

        public static void fileSend(DragEventArgs e)
        {
            //파일경로, 파일 확장자
            string filePath = getFilePath((string[])e.Data.GetData(DataFormats.FileDrop, false));

            //소켓 연결
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse(targetIP), 7000);


            //드래그 이벤트가 fileSend로 전달되며, 이벤트 안에 파일에 대한 정보가 들어있음.
            //파일에 대한 정보로 FileInformation 객체를 만들고, FIleInformation 객체를 송신하자.
            FileInfo fileInfo = new FileInfo(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int fileLength = (int)fs.Length;
            byte[] buffer = BitConverter.GetBytes(fileLength);

            //파일크기 송신
            socket.Send(buffer);

            //파일이름 길이 송신
            buffer = BitConverter.GetBytes(fileInfo.Name.Length);
            socket.Send(buffer);

            //파일이름 송신
            byte[] nameBuffer = Encoding.UTF8.GetBytes(fileInfo.Name + '\x01');
            MessageBox.Show("send : " + fileInfo.Name);
            socket.Send(nameBuffer);

            //파일 송신
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
                Program.t.Abort();
                recievemySocket.Close();
                this.Close();
            }
        }
        #endregion

        private void 멈추기SToolStripMenuItem_Click(object sender, EventArgs e)    {
            if (MoveTimer.Enabled)
            {
                toolStop.Text = "움직이기(&S)";
                MoveTimer.Stop();
            }
            else
            {
                toolStop.Text = "멈추기(&S)";
                MoveTimer.Start();
            }
                

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
            if (toolMouse.Text.Equals("마우스 가져가기(&M)"))
                toolMouse.Text = "마우스 놓기(&M)";
            else
                toolMouse.Text = "마우스 가져가기(&M)";
        }

        public static void receive()
        {
            recievemySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint point = new IPEndPoint(IPAddress.Parse(getLocalIP()), 7000);

            recievemySocket.Bind(point);

            recievemySocket.Listen(100);
            try
            {
                recievemySocket = recievemySocket.Accept();
            }
            catch(Exception ex)
            {
                return;
            }
            //파일 크기를 저장할 버퍼
            byte[] buffer = new byte[4];

            //파일 크기 수신
            recievemySocket.Receive(buffer);

            //파일 크기를 정수로 변환, fileLength에 저장
            int fileLength = BitConverter.ToInt32(buffer, 0);



            //파일 이름 길이 수신
            recievemySocket.Receive(buffer);
            int fileNameLength = BitConverter.ToInt32(buffer, 0);

            buffer = new byte[1024*4];
            //파일 이름 수신
            recievemySocket.Receive(buffer);

            string fileName = Encoding.UTF8.GetString(buffer);
            fileName = fileName.Split('\x01')[0];
            MessageBox.Show(fileName);
            string filePath = "";



            if(MessageBox.Show("파일을 수신하시겠습니까?", "파일수신", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                filePath = getPathByDialog();
            }
            else
            {

            }

            Console.WriteLine("fileName : " + fileName);
            Console.WriteLine("filePath : " + filePath);
            Console.WriteLine("fullPath : " + filePath + "\\" + fileName);

            //버퍼 크기 새로 지정
            buffer = new byte[1024];

            int totalLength = 0;
            FileStream fileStr = new FileStream(filePath + "\\" + fileName, FileMode.Create, FileAccess.Write);

            //받을 데이터를 파일에 쓰기 위해 BinaryWriter 객체 생성
            BinaryWriter bnryWriter = new BinaryWriter(fileStr);

            //받은 파일을 string으로 변환
            string context;

            //파일 수신
            while (totalLength < fileLength)
            {
                //받을 데이터 길이 저장
                int receiveLength = recievemySocket.Receive(buffer);
                //받은 데이터를 fileStr에 씀
                bnryWriter.Write(buffer, 0, receiveLength);

                totalLength += receiveLength;
            }

            bnryWriter.Close();
            recievemySocket.Close();
        }

        public static string getPathByDialog()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            return folderBrowserDialog.SelectedPath;
        }

        #region 자신의 localIP 리턴함수
        private static string getLocalIP()
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

        private static string getFilePath(string[] fileArg)
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
            Bear f = new Bear(childNum);
            childNum++;
            f.Show();
        }

        private void 광고AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] url = new String[3];
            url[0] = ("https://youtu.be/viJ6ONGOT_M");
            url[1] = ("https://github.com/501Pb/DJ_Keyboard");
            url[2] = ("https://github.com/Team-TDB/TheDesktopBear");

            Random r = new Random();

            System.Diagnostics.Process.Start(url[r.Next(0,3)]);
        }

        #region 친구찾기 위한 전역변수
        private static List<Ping> pingers = new List<Ping>();

        private static int instances = 0;

        private static object @lock = new object();

        private static int result = 0;
        private static int timeOut = 250;

        private static int ttl = 5;
        #endregion

        private void 친구찾기FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = GetARPResult();
            temp = temp.Replace("\r\n", "/");
            temp = temp.Replace(" ", "/");
            string[] words = temp.Split('/');
            //ip : local ip를 저장하는 list
            List<string> ip = new List<string>();

            foreach (string word in words)
            {
                if (word.Contains('.'))
                {
                    if (word == getLocalIP())
                        continue;

                    ip.Add(word);
                }
            }
            CreatePingers(ip.Count);
            PingOptions po = new PingOptions(ttl, true);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            byte[] data = enc.GetBytes(allignPingMsg("Bear-" + getLocalIP()));

            SpinWait wait = new SpinWait();
            int cnt = 1;

            Stopwatch watch = Stopwatch.StartNew();

            foreach (Ping p in pingers)
            {
                lock (@lock)
                {
                    instances += 1;
                }
                p.SendAsync(ip[0], timeOut, data, po);
                ip.RemoveAt(0);

                cnt += 1;
            }

            while (instances > 0)
            {
                wait.SpinOnce();
                instances--;
            }
            watch.Stop();
            DestroyPingers();
        }
        public static void Ping_completed(object s, PingCompletedEventArgs e)
        {
            lock (@lock)
            {
                instances -= 1;
            }

            if (e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(string.Concat("Active IP: ", e.Reply.Address.ToString()));
                result += 1;
            }
            else
            {
                //Console.WriteLine(String.Concat("Non-active IP: ", e.Reply.Address.ToString()));
            }
        }


        private static void CreatePingers(int cnt)
        {
            for (int i = 1; i <= cnt; i++)
            {
                Ping p = new Ping();
                p.PingCompleted += Ping_completed;
                pingers.Add(p);
            }
        }

        private static void DestroyPingers()
        {
            foreach (Ping p in pingers)
            {
                p.PingCompleted -= Ping_completed;
                p.Dispose();
            }

            pingers.Clear();

        }
        private static string GetARPResult()
        {
            Process p = null;
            string output = string.Empty;

            try
            {
                p = Process.Start(new ProcessStartInfo("arp", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                output = p.StandardOutput.ReadToEnd();

                p.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }

            return output;
        }

        //Message의 길이를 32byte로 맞춰 줍니다
        public static string allignPingMsg(string msg)
        {
            if (msg.Length > 32)
                return msg;

            int temp = msg.Length;

            for (int i = 0; i < (32 - temp); i++)
            {
                msg += "#";
            }
            return msg;
        }

        private void Bear_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (name.Text == "0")
            {
                recievemySocket.Close();
                notifyBear.Visible = false;
                Program.t.Abort();
            }
        }

        private void cmsNoti_Click(object sender, EventArgs e)
        {
            this.notifyBear.Visible = false;
            Application.Exit();
        }

        private void 숨기기HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void notifyBear_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;

        }
    }
}
