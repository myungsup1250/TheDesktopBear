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
    public partial class FriendListForm : Form
    {
        public FriendListForm()
        {
            InitializeComponent();
            foreach(string ip in FindFriends.friendList)
            {
                string temp = ip;
                temp = temp.Replace("Bear-", "");
                temp = temp.Replace("#", "");
                if (cbbIP.Items.Contains(temp))
                    continue;
                cbbIP.Items.Add(temp);
                Console.WriteLine(temp);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            Bear.targetIP = cbbIP.SelectedItem.ToString();
            Console.WriteLine(Bear.targetIP);
            Bear.fileSend(Bear.dragEvent);
            this.Close();
        }

        private void cbbIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(cbbIP.SelectedItem.ToString());
        }
    }
}
