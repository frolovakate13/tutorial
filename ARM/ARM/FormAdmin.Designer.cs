namespace ARM
{
    partial class FormAdmin
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
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.dgvDis = new System.Windows.Forms.DataGridView();
            this.dgvNote = new System.Windows.Forms.DataGridView();
            this.dgvDoctor = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.UserTSMI = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPatient
            // 
            this.dgvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatient.Location = new System.Drawing.Point(3, 16);
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.Size = new System.Drawing.Size(658, 311);
            this.dgvPatient.TabIndex = 0;
            this.dgvPatient.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvPatient_RowValidating);
            this.dgvPatient.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvPatient_UserDeletingRow);
            // 
            // dgvDis
            // 
            this.dgvDis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDis.Location = new System.Drawing.Point(3, 16);
            this.dgvDis.Name = "dgvDis";
            this.dgvDis.Size = new System.Drawing.Size(674, 311);
            this.dgvDis.TabIndex = 1;
            this.dgvDis.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDis_RowValidating);
            this.dgvDis.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvDis_UserDeletingRow);
            // 
            // dgvNote
            // 
            this.dgvNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNote.Location = new System.Drawing.Point(3, 16);
            this.dgvNote.Name = "dgvNote";
            this.dgvNote.Size = new System.Drawing.Size(652, 304);
            this.dgvNote.TabIndex = 2;
            this.dgvNote.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvNote_RowValidating);
            this.dgvNote.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvNote_UserDeletingRow);
            // 
            // dgvDoctor
            // 
            this.dgvDoctor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoctor.Location = new System.Drawing.Point(3, 16);
            this.dgvDoctor.Name = "dgvDoctor";
            this.dgvDoctor.Size = new System.Drawing.Size(671, 304);
            this.dgvDoctor.TabIndex = 3;
            this.dgvDoctor.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDoctor_RowValidating);
            this.dgvDoctor.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvDoctor_UserDeletingRow);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvPatient);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 330);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пациенты";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDis);
            this.groupBox2.Location = new System.Drawing.Point(670, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(680, 330);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Прививки";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvNote);
            this.groupBox3.Location = new System.Drawing.Point(3, 363);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(658, 323);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Запись на прием";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvDoctor);
            this.groupBox4.Location = new System.Drawing.Point(673, 363);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(677, 323);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Врачи";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserTSMI});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // UserTSMI
            // 
            this.UserTSMI.Name = "UserTSMI";
            this.UserTSMI.Size = new System.Drawing.Size(151, 20);
            this.UserTSMI.Text = "Таблица пользователей";
            this.UserTSMI.Click += new System.EventHandler(this.UserTSMI_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 698);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAdmin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.DataGridView dgvDis;
        private System.Windows.Forms.DataGridView dgvNote;
        private System.Windows.Forms.DataGridView dgvDoctor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UserTSMI;
    }
}