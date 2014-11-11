namespace Phone1st.Forms
{
    partial class SellMain
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOrderDelete = new System.Windows.Forms.Button();
            this.btnPrint2 = new System.Windows.Forms.Button();
            this.grdOrder = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtEdate = new System.Windows.Forms.TextBox();
            this.txtSdate = new System.Windows.Forms.TextBox();
            this.cbSearch = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPhoneSearch = new System.Windows.Forms.Button();
            this.companyUC1 = new Phone1st.Forms.companyUC();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrder)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnOrderDelete);
            this.groupBox1.Controls.Add(this.btnPrint2);
            this.groupBox1.Controls.Add(this.grdOrder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtEdate);
            this.groupBox1.Controls.Add(this.txtSdate);
            this.groupBox1.Controls.Add(this.cbSearch);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1085, 365);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "매도 조회";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(914, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "주문서 출력(매입금액X)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOrderDelete
            // 
            this.btnOrderDelete.Location = new System.Drawing.Point(721, 18);
            this.btnOrderDelete.Name = "btnOrderDelete";
            this.btnOrderDelete.Size = new System.Drawing.Size(80, 23);
            this.btnOrderDelete.TabIndex = 51;
            this.btnOrderDelete.Text = "주문서 삭제";
            this.btnOrderDelete.UseVisualStyleBackColor = true;
            this.btnOrderDelete.Click += new System.EventHandler(this.btnOrderDelete_Click);
            // 
            // btnPrint2
            // 
            this.btnPrint2.Location = new System.Drawing.Point(807, 18);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.Size = new System.Drawing.Size(101, 23);
            this.btnPrint2.TabIndex = 50;
            this.btnPrint2.Text = "주문서 출력";
            this.btnPrint2.UseVisualStyleBackColor = true;
            this.btnPrint2.Click += new System.EventHandler(this.btnPrint2_Click);
            // 
            // grdOrder
            // 
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grdOrder.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle27;
            this.grdOrder.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.grdOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOrder.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdOrder.Location = new System.Drawing.Point(20, 50);
            this.grdOrder.MultiSelect = false;
            this.grdOrder.Name = "grdOrder";
            this.grdOrder.ReadOnly = true;
            this.grdOrder.RowTemplate.Height = 23;
            this.grdOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdOrder.Size = new System.Drawing.Size(1050, 300);
            this.grdOrder.TabIndex = 15;
            this.grdOrder.SelectionChanged += new System.EventHandler(this.grdOrder_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "~";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::Phone1st.Properties.Resources.search_glyph;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(496, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "검색";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtEdate
            // 
            this.txtEdate.Location = new System.Drawing.Point(387, 20);
            this.txtEdate.Name = "txtEdate";
            this.txtEdate.Size = new System.Drawing.Size(100, 21);
            this.txtEdate.TabIndex = 12;
            // 
            // txtSdate
            // 
            this.txtSdate.Location = new System.Drawing.Point(254, 20);
            this.txtSdate.Name = "txtSdate";
            this.txtSdate.Size = new System.Drawing.Size(100, 21);
            this.txtSdate.TabIndex = 11;
            // 
            // cbSearch
            // 
            this.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearch.FormattingEnabled = true;
            this.cbSearch.Location = new System.Drawing.Point(20, 20);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(214, 20);
            this.cbSearch.TabIndex = 10;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(20, 20);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(214, 20);
            this.cbCompany.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDefault);
            this.groupBox3.Controls.Add(this.btnSaveAll);
            this.groupBox3.Controls.Add(this.txtMemo);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.btnPhoneSearch);
            this.groupBox3.Controls.Add(this.cbCompany);
            this.groupBox3.Location = new System.Drawing.Point(15, 390);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 240);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "매도 관리";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(481, 18);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 50;
            this.btnDefault.Text = "초기화";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(401, 18);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAll.TabIndex = 49;
            this.btnSaveAll.Text = "출고처리";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(71, 53);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(485, 70);
            this.txtMemo.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(20, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 48;
            this.label10.Text = "메모";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(15, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 0);
            this.panel1.TabIndex = 44;
            // 
            // btnPhoneSearch
            // 
            this.btnPhoneSearch.Location = new System.Drawing.Point(320, 18);
            this.btnPhoneSearch.Name = "btnPhoneSearch";
            this.btnPhoneSearch.Size = new System.Drawing.Size(75, 23);
            this.btnPhoneSearch.TabIndex = 17;
            this.btnPhoneSearch.Text = "재고 검색";
            this.btnPhoneSearch.UseVisualStyleBackColor = true;
            this.btnPhoneSearch.Click += new System.EventHandler(this.btnPhoneSearch_Click);
            // 
            // companyUC1
            // 
            this.companyUC1.BackColor = System.Drawing.Color.White;
            this.companyUC1.Location = new System.Drawing.Point(675, 379);
            this.companyUC1.Name = "companyUC1";
            this.companyUC1.Size = new System.Drawing.Size(435, 190);
            this.companyUC1.TabIndex = 18;
            this.companyUC1.Visible = false;
            // 
            // SellMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.companyUC1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "SellMain";
            this.Size = new System.Drawing.Size(1100, 650);
            this.Load += new System.EventHandler(this.SellMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrder)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grdOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtEdate;
        private System.Windows.Forms.ComboBox cbSearch;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnPhoneSearch;
        private System.Windows.Forms.TextBox txtSdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnPrint2;
        private System.Windows.Forms.Button btnOrderDelete;
        private companyUC companyUC1;
        private System.Windows.Forms.Button button1;

    }
}
