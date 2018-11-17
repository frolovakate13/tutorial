using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM
{
    public partial class FormUser : Form
    {
        public enum FormType
        {
            Insert,
            Update
        }
        public string login
        {
            get { return tbLogin.Text; }
            set { tbLogin.Text = value; }
        }
        public string password
        {
            get { return tbPassword.Text; }
            set { tbPassword.Text = value; }
        }
        public int level
        {
            get { return cbLevel.SelectedIndex; }
            set { cbLevel.SelectedIndex = value; }
        }
        public DateTime date
        {
            get { return dtpDate.Value; }
            set { dtpDate.Value = value; }
        }
        public FormUser(FormType frmType)
        {
            InitializeComponent();
            cbLevel.Items.Add("Администратор");
            cbLevel.Items.Add("Оператор");
            cbLevel.SelectedIndex = 0;

            switch (frmType)
            {
                case FormType.Insert:
                    btOK.Text = @"Добавить";
                    break;
                case FormType.Update:
                    btOK.Text = @"Изменить";
                    break;
            }
        }
    }
}
