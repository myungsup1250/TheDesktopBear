using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDesktopBear
{
    public partial class FileSender : Form
    {
        //List<File> files;
        //List<PictureBox> Imgs;

        int FileNum=0;
        public FileSender()
        {
            InitializeComponent();
        }

        private void FileSender_DragDrop(object sender, DragEventArgs e)
        {
            ++FileNum;
            FileImg1.BackColor = Color.White;

            //files.Add();
        }
        void FileDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void FileDragDrop(object sender, DragEventArgs e)
        {
            //여기에 통신부분들어가면 좋을 것 같음
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                //consoleeee.Text = file.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileSelector = new FolderBrowserDialog();
            fileSelector.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("파일을 전송하시겠습니까?", "파일전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("파일을 전송하였습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("파일 전송이 취소되었습니다");
            }
        }
    }
}
