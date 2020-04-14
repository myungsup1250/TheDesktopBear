﻿namespace TheDesktopBear
{
    partial class Bear
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bear));
            this.Character = new System.Windows.Forms.PictureBox();
            this.TaskList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.파일전송하기SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.프로세스죽이기KToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.멈추기SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitTimer = new System.Windows.Forms.Timer(this.components);
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.name = new System.Windows.Forms.Label();
            this.ExitTimeDisplay = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            this.TaskList.SuspendLayout();
            this.SuspendLayout();
            // 
            // Character
            // 
            this.Character.ContextMenuStrip = this.TaskList;
            this.Character.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Character.Image = ((System.Drawing.Image)(resources.GetObject("Character.Image")));
            this.Character.Location = new System.Drawing.Point(12, 39);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(80, 88);
            this.Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Character.TabIndex = 0;
            this.Character.TabStop = false;
            this.Character.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Character_MouseDown);
            this.Character.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Character_MouseMove);
            // 
            // TaskList
            // 
            this.TaskList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TaskList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일전송하기SToolStripMenuItem,
            this.프로세스죽이기KToolStripMenuItem,
            this.멈추기SToolStripMenuItem});
            this.TaskList.Name = "TaskList";
            this.TaskList.Size = new System.Drawing.Size(208, 76);
            this.TaskList.TabStop = true;
            // 
            // 파일전송하기SToolStripMenuItem
            // 
            this.파일전송하기SToolStripMenuItem.Name = "파일전송하기SToolStripMenuItem";
            this.파일전송하기SToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.파일전송하기SToolStripMenuItem.Text = "파일 전송하기(&F)";
            // 
            // 프로세스죽이기KToolStripMenuItem
            // 
            this.프로세스죽이기KToolStripMenuItem.Name = "프로세스죽이기KToolStripMenuItem";
            this.프로세스죽이기KToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.프로세스죽이기KToolStripMenuItem.Text = "프로세스 죽이기(&K)";
            // 
            // 멈추기SToolStripMenuItem
            // 
            this.멈추기SToolStripMenuItem.Name = "멈추기SToolStripMenuItem";
            this.멈추기SToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.멈추기SToolStripMenuItem.Text = "멈추기(&S)";
            this.멈추기SToolStripMenuItem.Click += new System.EventHandler(this.멈추기SToolStripMenuItem_Click);
            // 
            // MoveTimer
            // 
            this.MoveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("굴림", 12F);
            this.name.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.name.Location = new System.Drawing.Point(21, 16);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(60, 20);
            this.name.TabIndex = 1;
            this.name.Text = "NAME";
            // 
            // ExitTimeDisplay
            // 
            this.ExitTimeDisplay.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ExitTimeDisplay.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ExitTimeDisplay.Location = new System.Drawing.Point(98, 102);
            this.ExitTimeDisplay.Name = "ExitTimeDisplay";
            this.ExitTimeDisplay.Size = new System.Drawing.Size(35, 25);
            this.ExitTimeDisplay.TabIndex = 2;
            this.ExitTimeDisplay.Text = "0";
            this.ExitTimeDisplay.Visible = false;
            this.ExitTimeDisplay.TextChanged += new System.EventHandler(this.ExitTimeDisplay_TextChanged);
            // 
            // Bear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(268, 139);
            this.Controls.Add(this.ExitTimeDisplay);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Character);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Bear";
            this.Opacity = 0.9D;
            this.Text = "TheDesktopBear";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.HotTrack;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CharacterKeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CharacterKeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CharacterKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Character)).EndInit();
            this.TaskList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Character;
        private System.Windows.Forms.Timer ExitTimer;
        private System.Windows.Forms.Timer MoveTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip TaskList;
        private System.Windows.Forms.ToolStripMenuItem 파일전송하기SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 프로세스죽이기KToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 멈추기SToolStripMenuItem;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox ExitTimeDisplay;
    }
}
