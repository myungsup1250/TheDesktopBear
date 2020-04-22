namespace TheDesktopBear
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
            this.마우스따라가기MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitTimer = new System.Windows.Forms.Timer(this.components);
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.name = new System.Windows.Forms.Label();
            this.ExitTimeDisplay = new System.Windows.Forms.TextBox();
            this.분신술CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.광고AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            this.TaskList.SuspendLayout();
            this.SuspendLayout();
            // 
            // Character
            // 
            this.Character.ContextMenuStrip = this.TaskList;
            this.Character.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Character.Image = ((System.Drawing.Image)(resources.GetObject("Character.Image")));
            this.Character.Location = new System.Drawing.Point(10, 31);
            this.Character.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(70, 70);
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
            this.멈추기SToolStripMenuItem,
            this.마우스따라가기MToolStripMenuItem,
            this.분신술CToolStripMenuItem,
            this.광고AToolStripMenuItem});
            this.TaskList.Name = "TaskList";
            this.TaskList.Size = new System.Drawing.Size(232, 188);
            this.TaskList.TabStop = true;
            // 
            // 파일전송하기SToolStripMenuItem
            // 
            this.파일전송하기SToolStripMenuItem.Name = "파일전송하기SToolStripMenuItem";
            this.파일전송하기SToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.파일전송하기SToolStripMenuItem.Text = "파일 전송하기(&F)";
            this.파일전송하기SToolStripMenuItem.Click += new System.EventHandler(this.파일전송하기SToolStripMenuItem_Click);
            // 
            // 프로세스죽이기KToolStripMenuItem
            // 
            this.프로세스죽이기KToolStripMenuItem.Name = "프로세스죽이기KToolStripMenuItem";
            this.프로세스죽이기KToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.프로세스죽이기KToolStripMenuItem.Text = "프로세스 죽이기(&K)";
            this.프로세스죽이기KToolStripMenuItem.Click += new System.EventHandler(this.프로세스죽이기KToolStripMenuItem_Click);
            // 
            // 멈추기SToolStripMenuItem
            // 
            this.멈추기SToolStripMenuItem.Name = "멈추기SToolStripMenuItem";
            this.멈추기SToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.멈추기SToolStripMenuItem.Text = "멈추기(&S)";
            this.멈추기SToolStripMenuItem.Click += new System.EventHandler(this.멈추기SToolStripMenuItem_Click);
            // 
            // 마우스따라가기MToolStripMenuItem
            // 
            this.마우스따라가기MToolStripMenuItem.Name = "마우스따라가기MToolStripMenuItem";
            this.마우스따라가기MToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.마우스따라가기MToolStripMenuItem.Text = "마우스 따라가기(&M)";
            this.마우스따라가기MToolStripMenuItem.Click += new System.EventHandler(this.마우스따라가기MToolStripMenuItem_Click);
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
            this.name.Location = new System.Drawing.Point(18, 13);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(52, 16);
            this.name.TabIndex = 1;
            this.name.Text = "NAME";
            // 
            // ExitTimeDisplay
            // 
            this.ExitTimeDisplay.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ExitTimeDisplay.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ExitTimeDisplay.Location = new System.Drawing.Point(86, 82);
            this.ExitTimeDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExitTimeDisplay.Name = "ExitTimeDisplay";
            this.ExitTimeDisplay.Size = new System.Drawing.Size(27, 21);
            this.ExitTimeDisplay.TabIndex = 2;
            this.ExitTimeDisplay.Text = "0";
            this.ExitTimeDisplay.Visible = false;
            this.ExitTimeDisplay.TextChanged += new System.EventHandler(this.ExitTimeDisplay_TextChanged);
            // 
            // 분신술CToolStripMenuItem
            // 
            this.분신술CToolStripMenuItem.Name = "분신술CToolStripMenuItem";
            this.분신술CToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.분신술CToolStripMenuItem.Text = "분신술(&C)";
            this.분신술CToolStripMenuItem.Click += new System.EventHandler(this.분신술CToolStripMenuItem_Click);
            // 
            // 광고AToolStripMenuItem
            // 
            this.광고AToolStripMenuItem.Name = "광고AToolStripMenuItem";
            this.광고AToolStripMenuItem.Size = new System.Drawing.Size(231, 28);
            this.광고AToolStripMenuItem.Text = "광고(&A)";
            this.광고AToolStripMenuItem.Click += new System.EventHandler(this.광고AToolStripMenuItem_Click);
            // 
            // Bear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(140, 118);
            this.Controls.Add(this.ExitTimeDisplay);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Character);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Bear";
            this.Opacity = 0.85D;
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
        private System.Windows.Forms.ToolStripMenuItem 마우스따라가기MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 분신술CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 광고AToolStripMenuItem;
    }
}

