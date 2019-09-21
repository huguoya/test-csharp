namespace TestApp
{
    partial class FrmGwyyt
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lbUrl = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnRefByDate = new System.Windows.Forms.Button();
            this.btnReLogin = new System.Windows.Forms.Button();
            this.lbAction = new System.Windows.Forms.Label();
            this.btnGet12 = new System.Windows.Forms.Button();
            this.lbLogin = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnGetCookie = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Location = new System.Drawing.Point(10, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 800);
            this.panel1.TabIndex = 0;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(800, 10);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(600, 180);
            this.txtMsg.TabIndex = 11;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1400, 800);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUrl.ForeColor = System.Drawing.Color.Red;
            this.lbUrl.Location = new System.Drawing.Point(10, 60);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(59, 19);
            this.lbUrl.TabIndex = 1;
            this.lbUrl.Text = "lbUrl";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(80, 21);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // btnRefByDate
            // 
            this.btnRefByDate.Location = new System.Drawing.Point(200, 20);
            this.btnRefByDate.Name = "btnRefByDate";
            this.btnRefByDate.Size = new System.Drawing.Size(75, 23);
            this.btnRefByDate.TabIndex = 5;
            this.btnRefByDate.Text = "刷新";
            this.btnRefByDate.UseVisualStyleBackColor = true;
            this.btnRefByDate.Click += new System.EventHandler(this.btnRefByDate_Click);
            // 
            // btnReLogin
            // 
            this.btnReLogin.Location = new System.Drawing.Point(400, 20);
            this.btnReLogin.Name = "btnReLogin";
            this.btnReLogin.Size = new System.Drawing.Size(75, 23);
            this.btnReLogin.TabIndex = 7;
            this.btnReLogin.Text = "重新登录";
            this.btnReLogin.UseVisualStyleBackColor = true;
            this.btnReLogin.Click += new System.EventHandler(this.btnReLogin_Click);
            // 
            // lbAction
            // 
            this.lbAction.AutoSize = true;
            this.lbAction.Location = new System.Drawing.Point(10, 130);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(53, 12);
            this.lbAction.TabIndex = 8;
            this.lbAction.Text = "lbAction";
            // 
            // btnGet12
            // 
            this.btnGet12.Location = new System.Drawing.Point(300, 20);
            this.btnGet12.Name = "btnGet12";
            this.btnGet12.Size = new System.Drawing.Size(75, 23);
            this.btnGet12.TabIndex = 9;
            this.btnGet12.Text = "查询全部";
            this.btnGet12.UseVisualStyleBackColor = true;
            this.btnGet12.Click += new System.EventHandler(this.btnGet12_Click);
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Location = new System.Drawing.Point(10, 100);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(47, 12);
            this.lbLogin.TabIndex = 10;
            this.lbLogin.Text = "lbLogin";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnGetCookie
            // 
            this.btnGetCookie.Location = new System.Drawing.Point(500, 20);
            this.btnGetCookie.Name = "btnGetCookie";
            this.btnGetCookie.Size = new System.Drawing.Size(130, 23);
            this.btnGetCookie.TabIndex = 12;
            this.btnGetCookie.Text = "获取cookie+api";
            this.btnGetCookie.UseVisualStyleBackColor = true;
            this.btnGetCookie.Click += new System.EventHandler(this.btnGetCookie_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 961);
            this.Controls.Add(this.btnGetCookie);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.btnGet12);
            this.Controls.Add(this.lbAction);
            this.Controls.Add(this.btnReLogin);
            this.Controls.Add(this.btnRefByDate);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lbUrl);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "国网网上营业厅数据读取";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnRefByDate;
        private System.Windows.Forms.Button btnReLogin;
        private System.Windows.Forms.Label lbAction;
        private System.Windows.Forms.Button btnGet12;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnGetCookie;
    }
}