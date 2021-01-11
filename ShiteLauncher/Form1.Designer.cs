namespace ShiteLauncher
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Email = new System.Windows.Forms.TextBox();
            this.BTT_Launch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(71, 42);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '*';
            this.TB_Password.Size = new System.Drawing.Size(229, 20);
            this.TB_Password.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // TB_Email
            // 
            this.TB_Email.Location = new System.Drawing.Point(71, 6);
            this.TB_Email.Name = "TB_Email";
            this.TB_Email.Size = new System.Drawing.Size(229, 20);
            this.TB_Email.TabIndex = 3;
            // 
            // BTT_Launch
            // 
            this.BTT_Launch.Location = new System.Drawing.Point(15, 79);
            this.BTT_Launch.Name = "BTT_Launch";
            this.BTT_Launch.Size = new System.Drawing.Size(285, 23);
            this.BTT_Launch.TabIndex = 4;
            this.BTT_Launch.Text = "Launch";
            this.BTT_Launch.UseVisualStyleBackColor = true;
            this.BTT_Launch.Click += new System.EventHandler(this.BTT_Launch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 119);
            this.Controls.Add(this.BTT_Launch);
            this.Controls.Add(this.TB_Email);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Shite Firefall Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Email;
        private System.Windows.Forms.Button BTT_Launch;
    }
}

