namespace Phone1st
{
    partial class StockList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockList));
            this.cbMaker = new System.Windows.Forms.ComboBox();
            this.cbAgency = new System.Windows.Forms.ComboBox();
            this.grdStockList = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnInput = new System.Windows.Forms.Button();
            this.txtCode2 = new System.Windows.Forms.TextBox();
            this.txtCode3 = new System.Windows.Forms.TextBox();
            this.txtCode5 = new System.Windows.Forms.TextBox();
            this.txtCode6 = new System.Windows.Forms.TextBox();
            this.txtCode7 = new System.Windows.Forms.TextBox();
            this.txtCode8 = new System.Windows.Forms.TextBox();
            this.txtCode10 = new System.Windows.Forms.TextBox();
            this.txtCode9 = new System.Windows.Forms.TextBox();
            this.txtCode4 = new System.Windows.Forms.TextBox();
            this.txtCode1 = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.grdStockList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMaker
            // 
            this.cbMaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaker.FormattingEnabled = true;
            this.cbMaker.Location = new System.Drawing.Point(17, 17);
            this.cbMaker.Name = "cbMaker";
            this.cbMaker.Size = new System.Drawing.Size(121, 20);
            this.cbMaker.TabIndex = 0;
            // 
            // cbAgency
            // 
            this.cbAgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAgency.FormattingEnabled = true;
            this.cbAgency.Location = new System.Drawing.Point(152, 17);
            this.cbAgency.Name = "cbAgency";
            this.cbAgency.Size = new System.Drawing.Size(121, 20);
            this.cbAgency.TabIndex = 1;
            // 
            // grdStockList
            // 
            this.grdStockList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdStockList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStockList.Location = new System.Drawing.Point(17, 55);
            this.grdStockList.Name = "grdStockList";
            this.grdStockList.RowTemplate.Height = 23;
            this.grdStockList.Size = new System.Drawing.Size(646, 460);
            this.grdStockList.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(588, 531);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "확인";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(688, 595);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnInput);
            this.tabPage1.Controls.Add(this.txtCode2);
            this.tabPage1.Controls.Add(this.txtCode3);
            this.tabPage1.Controls.Add(this.txtCode5);
            this.tabPage1.Controls.Add(this.txtCode6);
            this.tabPage1.Controls.Add(this.txtCode7);
            this.tabPage1.Controls.Add(this.txtCode8);
            this.tabPage1.Controls.Add(this.txtCode10);
            this.tabPage1.Controls.Add(this.txtCode9);
            this.tabPage1.Controls.Add(this.txtCode4);
            this.tabPage1.Controls.Add(this.txtCode1);
            this.tabPage1.Controls.Add(this.cbType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(680, 569);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "직접 입력";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(339, 370);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(75, 23);
            this.btnInput.TabIndex = 11;
            this.btnInput.Text = "확인";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // txtCode2
            // 
            this.txtCode2.Location = new System.Drawing.Point(20, 90);
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.Size = new System.Drawing.Size(394, 21);
            this.txtCode2.TabIndex = 2;
            // 
            // txtCode3
            // 
            this.txtCode3.Location = new System.Drawing.Point(20, 120);
            this.txtCode3.Name = "txtCode3";
            this.txtCode3.Size = new System.Drawing.Size(394, 21);
            this.txtCode3.TabIndex = 3;
            // 
            // txtCode5
            // 
            this.txtCode5.Location = new System.Drawing.Point(20, 180);
            this.txtCode5.Name = "txtCode5";
            this.txtCode5.Size = new System.Drawing.Size(394, 21);
            this.txtCode5.TabIndex = 5;
            // 
            // txtCode6
            // 
            this.txtCode6.Location = new System.Drawing.Point(20, 210);
            this.txtCode6.Name = "txtCode6";
            this.txtCode6.Size = new System.Drawing.Size(394, 21);
            this.txtCode6.TabIndex = 6;
            // 
            // txtCode7
            // 
            this.txtCode7.Location = new System.Drawing.Point(20, 240);
            this.txtCode7.Name = "txtCode7";
            this.txtCode7.Size = new System.Drawing.Size(394, 21);
            this.txtCode7.TabIndex = 7;
            // 
            // txtCode8
            // 
            this.txtCode8.Location = new System.Drawing.Point(20, 270);
            this.txtCode8.Name = "txtCode8";
            this.txtCode8.Size = new System.Drawing.Size(394, 21);
            this.txtCode8.TabIndex = 8;
            // 
            // txtCode10
            // 
            this.txtCode10.Location = new System.Drawing.Point(20, 330);
            this.txtCode10.Name = "txtCode10";
            this.txtCode10.Size = new System.Drawing.Size(394, 21);
            this.txtCode10.TabIndex = 10;
            // 
            // txtCode9
            // 
            this.txtCode9.Location = new System.Drawing.Point(20, 300);
            this.txtCode9.Name = "txtCode9";
            this.txtCode9.Size = new System.Drawing.Size(394, 21);
            this.txtCode9.TabIndex = 9;
            // 
            // txtCode4
            // 
            this.txtCode4.Location = new System.Drawing.Point(20, 150);
            this.txtCode4.Name = "txtCode4";
            this.txtCode4.Size = new System.Drawing.Size(394, 21);
            this.txtCode4.TabIndex = 4;
            // 
            // txtCode1
            // 
            this.txtCode1.Location = new System.Drawing.Point(20, 60);
            this.txtCode1.Name = "txtCode1";
            this.txtCode1.Size = new System.Drawing.Size(394, 21);
            this.txtCode1.TabIndex = 1;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "바코드 읽기",
            "직접 입력(일련번호)"});
            this.cbType.Location = new System.Drawing.Point(20, 20);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(174, 20);
            this.cbType.TabIndex = 0;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cbMaker);
            this.tabPage2.Controls.Add(this.grdStockList);
            this.tabPage2.Controls.Add(this.btnOk);
            this.tabPage2.Controls.Add(this.cbAgency);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(680, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "재고 검색";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // StockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(715, 634);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StockList";
            this.Text = "재고 검색";
            this.Load += new System.EventHandler(this.StockList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdStockList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMaker;
        private System.Windows.Forms.ComboBox cbAgency;
        private System.Windows.Forms.DataGridView grdStockList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.TextBox txtCode1;
        private System.Windows.Forms.TextBox txtCode2;
        private System.Windows.Forms.TextBox txtCode3;
        private System.Windows.Forms.TextBox txtCode4;
        private System.Windows.Forms.TextBox txtCode5;
        private System.Windows.Forms.TextBox txtCode6;
        private System.Windows.Forms.TextBox txtCode7;
        private System.Windows.Forms.TextBox txtCode8;        
        private System.Windows.Forms.TextBox txtCode9;
        private System.Windows.Forms.TextBox txtCode10;

        private System.Windows.Forms.ComboBox cbType;
    }
}