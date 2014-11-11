namespace Phone1st.Forms
{
    partial class companyUC
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
            this.phoneGroup = new System.Windows.Forms.GroupBox();
            this.lblAddr = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblDamdang = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape9 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape8 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape7 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape6 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.phoneGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // phoneGroup
            // 
            this.phoneGroup.Controls.Add(this.lblAddr);
            this.phoneGroup.Controls.Add(this.lblTel);
            this.phoneGroup.Controls.Add(this.lblPhone);
            this.phoneGroup.Controls.Add(this.lblDamdang);
            this.phoneGroup.Controls.Add(this.lblCompany);
            this.phoneGroup.Controls.Add(this.label8);
            this.phoneGroup.Controls.Add(this.label7);
            this.phoneGroup.Controls.Add(this.label5);
            this.phoneGroup.Controls.Add(this.label4);
            this.phoneGroup.Controls.Add(this.label6);
            this.phoneGroup.Controls.Add(this.shapeContainer1);
            this.phoneGroup.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.phoneGroup.Location = new System.Drawing.Point(10, 10);
            this.phoneGroup.Name = "phoneGroup";
            this.phoneGroup.Size = new System.Drawing.Size(415, 167);
            this.phoneGroup.TabIndex = 20;
            this.phoneGroup.TabStop = false;
            this.phoneGroup.Text = "업체정보";
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(103, 135);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(38, 12);
            this.lblAddr.TabIndex = 42;
            this.lblAddr.Text = "label1";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new System.Drawing.Point(103, 110);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(38, 12);
            this.lblTel.TabIndex = 41;
            this.lblTel.Text = "label1";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(103, 85);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(38, 12);
            this.lblPhone.TabIndex = 40;
            this.lblPhone.Text = "label1";
            // 
            // lblDamdang
            // 
            this.lblDamdang.AutoSize = true;
            this.lblDamdang.Location = new System.Drawing.Point(103, 60);
            this.lblDamdang.Name = "lblDamdang";
            this.lblDamdang.Size = new System.Drawing.Size(38, 12);
            this.lblDamdang.TabIndex = 39;
            this.lblDamdang.Text = "label1";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(103, 35);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(38, 12);
            this.lblCompany.TabIndex = 38;
            this.lblCompany.Text = "label1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(20, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "업체명";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(20, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "연락처";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(20, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "핸드폰";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(20, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "주소";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(20, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "담당자명";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 17);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape9,
            this.lineShape8,
            this.lineShape7,
            this.lineShape6,
            this.lineShape5,
            this.lineShape4,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(409, 147);
            this.shapeContainer1.TabIndex = 37;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape9
            // 
            this.lineShape9.Name = "lineShape9";
            this.lineShape9.X1 = 90;
            this.lineShape9.X2 = 90;
            this.lineShape9.Y1 = 10;
            this.lineShape9.Y2 = 135;
            // 
            // lineShape8
            // 
            this.lineShape8.Name = "lineShape8";
            this.lineShape8.X1 = 400;
            this.lineShape8.X2 = 400;
            this.lineShape8.Y1 = 10;
            this.lineShape8.Y2 = 135;
            // 
            // lineShape7
            // 
            this.lineShape7.Name = "lineShape7";
            this.lineShape7.X1 = 10;
            this.lineShape7.X2 = 10;
            this.lineShape7.Y1 = 10;
            this.lineShape7.Y2 = 135;
            // 
            // lineShape6
            // 
            this.lineShape6.Name = "lineShape6";
            this.lineShape6.X1 = 10;
            this.lineShape6.X2 = 400;
            this.lineShape6.Y1 = 135;
            this.lineShape6.Y2 = 135;
            // 
            // lineShape5
            // 
            this.lineShape5.Name = "lineShape5";
            this.lineShape5.X1 = 10;
            this.lineShape5.X2 = 400;
            this.lineShape5.Y1 = 110;
            this.lineShape5.Y2 = 110;
            // 
            // lineShape4
            // 
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 10;
            this.lineShape4.X2 = 400;
            this.lineShape4.Y1 = 85;
            this.lineShape4.Y2 = 85;
            // 
            // lineShape3
            // 
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 10;
            this.lineShape3.X2 = 400;
            this.lineShape3.Y1 = 60;
            this.lineShape3.Y2 = 60;
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 10;
            this.lineShape2.X2 = 400;
            this.lineShape2.Y1 = 35;
            this.lineShape2.Y2 = 35;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 10;
            this.lineShape1.X2 = 400;
            this.lineShape1.Y1 = 10;
            this.lineShape1.Y2 = 10;
            // 
            // companyUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.phoneGroup);
            this.Name = "companyUC";
            this.Size = new System.Drawing.Size(435, 190);
            this.Load += new System.EventHandler(this.companyUC_Load);
            this.phoneGroup.ResumeLayout(false);
            this.phoneGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox phoneGroup;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblDamdang;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape9;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape8;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape7;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape6;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}
