namespace ARM
{
    partial class FormEntry
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btOK = new System.Windows.Forms.Button();
            this.btGuest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btReg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(259, 120);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "Вход";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btGuest
            // 
            this.btGuest.Location = new System.Drawing.Point(12, 209);
            this.btGuest.Name = "btGuest";
            this.btGuest.Size = new System.Drawing.Size(316, 40);
            this.btGuest.TabIndex = 1;
            this.btGuest.Text = "Войти как гость";
            this.btGuest.UseVisualStyleBackColor = true;
            this.btGuest.Click += new System.EventHandler(this.btGuest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbLogin);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 48);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Логин";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(7, 20);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(309, 20);
            this.tbLogin.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbPassword);
            this.groupBox2.Location = new System.Drawing.Point(12, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 47);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Пароль";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(7, 20);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(309, 20);
            this.tbPassword.TabIndex = 0;
            // 
            // btReg
            // 
            this.btReg.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btReg.Location = new System.Drawing.Point(12, 120);
            this.btReg.Name = "btReg";
            this.btReg.Size = new System.Drawing.Size(137, 23);
            this.btReg.TabIndex = 4;
            this.btReg.Text = "Регистрация";
            this.btReg.UseVisualStyleBackColor = true;
            this.btReg.Click += new System.EventHandler(this.btReg_Click);
            // 
            // FormEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 261);
            this.Controls.Add(this.btReg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btGuest);
            this.Controls.Add(this.btOK);
            this.Name = "FormEntry";
            this.Text = "Вход";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btGuest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btReg;
    }
}

