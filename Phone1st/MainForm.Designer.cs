namespace Phone1st
{
    partial class MainForm
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Main1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Main1_Sub2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2_Sub1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2_Sub2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2_Sub3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2_Sub4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu3_Sub1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu3_Sub2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu3_Sub3 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Tan;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Main1,
            this.Menu2,
            this.Menu3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Main1
            // 
            this.Main1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Main1_Sub2});
            this.Main1.Name = "Main1";
            this.Main1.Size = new System.Drawing.Size(57, 20);
            this.Main1.Text = "파일(&F)";
            // 
            // Main1_Sub2
            // 
            this.Main1_Sub2.Name = "Main1_Sub2";
            this.Main1_Sub2.Size = new System.Drawing.Size(113, 22);
            this.Main1_Sub2.Text = "종료(&X)";
            this.Main1_Sub2.Click += new System.EventHandler(this.Main1_Sub2_Click);
            // 
            // Menu2
            // 
            this.Menu2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu2_Sub1,
            this.Menu2_Sub2,
            this.Menu2_Sub3,
            this.Menu2_Sub4});
            this.Menu2.Name = "Menu2";
            this.Menu2.Size = new System.Drawing.Size(86, 20);
            this.Menu2.Text = "매입/매도(&L)";
            // 
            // Menu2_Sub1
            // 
            this.Menu2_Sub1.Name = "Menu2_Sub1";
            this.Menu2_Sub1.Size = new System.Drawing.Size(152, 22);
            this.Menu2_Sub1.Text = "매입관리(&B)";
            this.Menu2_Sub1.Click += new System.EventHandler(this.Menu2_Sub1_Click);
            // 
            // Menu2_Sub2
            // 
            this.Menu2_Sub2.Name = "Menu2_Sub2";
            this.Menu2_Sub2.Size = new System.Drawing.Size(152, 22);
            this.Menu2_Sub2.Text = "매도관리(&S)";
            this.Menu2_Sub2.Click += new System.EventHandler(this.Menu2_Sub2_Click);
            // 
            // Menu2_Sub3
            // 
            this.Menu2_Sub3.Name = "Menu2_Sub3";
            this.Menu2_Sub3.Size = new System.Drawing.Size(152, 22);
            this.Menu2_Sub3.Text = "재고관리(&T)";
            this.Menu2_Sub3.Click += new System.EventHandler(this.Menu2_Sub3_Click);
            // 
            // Menu2_Sub4
            // 
            this.Menu2_Sub4.Name = "Menu2_Sub4";
            this.Menu2_Sub4.Size = new System.Drawing.Size(152, 22);
            this.Menu2_Sub4.Text = "매출관리";
            this.Menu2_Sub4.Click += new System.EventHandler(this.Menu2_Sub4_Click);
            // 
            // Menu3
            // 
            this.Menu3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu3_Sub1,
            this.Menu3_Sub2,
            this.Menu3_Sub3});
            this.Menu3.Name = "Menu3";
            this.Menu3.Size = new System.Drawing.Size(43, 20);
            this.Menu3.Text = "기타";
            // 
            // Menu3_Sub1
            // 
            this.Menu3_Sub1.Name = "Menu3_Sub1";
            this.Menu3_Sub1.Size = new System.Drawing.Size(166, 22);
            this.Menu3_Sub1.Text = "거래처 관리";
            this.Menu3_Sub1.Click += new System.EventHandler(this.Menu3_Sub1_Click);
            // 
            // Menu3_Sub2
            // 
            this.Menu3_Sub2.Name = "Menu3_Sub2";
            this.Menu3_Sub2.Size = new System.Drawing.Size(166, 22);
            this.Menu3_Sub2.Text = "핸드폰 모델 관리";
            this.Menu3_Sub2.Click += new System.EventHandler(this.Menu3_Sub2_Click);
            // 
            // Menu3_Sub3
            // 
            this.Menu3_Sub3.Name = "Menu3_Sub3";
            this.Menu3_Sub3.Size = new System.Drawing.Size(166, 22);
            this.Menu3_Sub3.Text = "아이폰 리퍼 조회";
            this.Menu3_Sub3.Click += new System.EventHandler(this.Menu3_Sub3_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSize = true;
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1008, 666);
            this.MainPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 690);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Phone1St Beta Version";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Main1;
        private System.Windows.Forms.ToolStripMenuItem Main1_Sub2;
        private System.Windows.Forms.ToolStripMenuItem Menu2;
        private System.Windows.Forms.ToolStripMenuItem Menu2_Sub1;
        private System.Windows.Forms.ToolStripMenuItem Menu2_Sub2;
        private System.Windows.Forms.ToolStripMenuItem Menu2_Sub3;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripMenuItem Menu3;
        private System.Windows.Forms.ToolStripMenuItem Menu3_Sub1;
        private System.Windows.Forms.ToolStripMenuItem Menu3_Sub2;
        private System.Windows.Forms.ToolStripMenuItem Menu3_Sub3;
        private System.Windows.Forms.ToolStripMenuItem Menu2_Sub4;
    }
}

