namespace dbComparison
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txt_conn1 = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.txt_conn2 = new System.Windows.Forms.TextBox();
            this.btn_cn1 = new System.Windows.Forms.Button();
            this.btn_cn2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "连接字符串";
            // 
            // txt_conn1
            // 
            this.txt_conn1.Location = new System.Drawing.Point(120, 39);
            this.txt_conn1.Name = "txt_conn1";
            this.txt_conn1.Size = new System.Drawing.Size(345, 21);
            this.txt_conn1.TabIndex = 2;
            this.txt_conn1.Text = "Data Source=192.168.3.45;Initial Catalog=EduPlatDB;uid=sa;pwd=asdf1234;MultipleAc" +
    "tiveResultSets=true";
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(248, 144);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_submit.TabIndex = 3;
            this.btn_submit.Text = "比对";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.Btn_submit_Click);
            // 
            // txt_conn2
            // 
            this.txt_conn2.Location = new System.Drawing.Point(120, 85);
            this.txt_conn2.Name = "txt_conn2";
            this.txt_conn2.Size = new System.Drawing.Size(345, 21);
            this.txt_conn2.TabIndex = 2;
            this.txt_conn2.Text = "Data Source=192.168.3.55;Initial Catalog=EduPlatDB;uid=sa;pwd=asdf1234;MultipleAc" +
    "tiveResultSets=true";
            // 
            // btn_cn1
            // 
            this.btn_cn1.Location = new System.Drawing.Point(483, 37);
            this.btn_cn1.Name = "btn_cn1";
            this.btn_cn1.Size = new System.Drawing.Size(75, 23);
            this.btn_cn1.TabIndex = 3;
            this.btn_cn1.Text = "连接db1";
            this.btn_cn1.UseVisualStyleBackColor = true;
            this.btn_cn1.Click += new System.EventHandler(this.Btn_cn1_Click);
            // 
            // btn_cn2
            // 
            this.btn_cn2.Location = new System.Drawing.Point(483, 83);
            this.btn_cn2.Name = "btn_cn2";
            this.btn_cn2.Size = new System.Drawing.Size(75, 23);
            this.btn_cn2.TabIndex = 3;
            this.btn_cn2.Text = "连接db2";
            this.btn_cn2.UseVisualStyleBackColor = true;
            this.btn_cn2.Click += new System.EventHandler(this.Btn_cn2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "连接字符串";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 196);
            this.Controls.Add(this.btn_cn2);
            this.Controls.Add(this.btn_cn1);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.txt_conn2);
            this.Controls.Add(this.txt_conn1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "mssql表结构比对器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_conn1;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.TextBox txt_conn2;
        private System.Windows.Forms.Button btn_cn1;
        private System.Windows.Forms.Button btn_cn2;
        private System.Windows.Forms.Label label3;
    }
}

