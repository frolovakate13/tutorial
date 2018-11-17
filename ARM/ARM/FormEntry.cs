using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace ARM
{
    public partial class FormEntry : Form
    {
        private readonly string _sConnStr = new SqlConnectionStringBuilder
        {
            DataSource = @"DESKTOP-F410EJM\SQLEXPRESS",
            InitialCatalog = "Клиника",
            IntegratedSecurity = true
        }.ConnectionString;

        private bool unique(string log)
        {
            bool flag = true;
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open(); // открываем подключение
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = "select count(*) from Пользователи where Login = @login" //поиск пользователей с таким же логином
                };
                sCommand.Parameters.AddWithValue("@login", log);
                if ((int)sCommand.ExecuteScalar() > 0) //как работает: возвращает первую строку первого столбца(вернет кол-во пользователей с заданным логином)
                {
                    flag = false;
                }
            }
            return flag;
        }
        private bool empty(string login, string password)
        {
            bool flag = false;
            if ((login.Trim() == "") || (password == ""))
            {
                MessageBox.Show(@"Заполните логин и пароль!");
                flag = true;
            }
            return flag;
        }
        private string GenSalt(int length)
        {
            RNGCryptoServiceProvider p = new RNGCryptoServiceProvider();
            var salt = new byte[length];
            p.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        private string hash(string salt, string password)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] data = Encoding.Default.GetBytes(salt + password);
            var result = new SHA256Managed().ComputeHash(data);
            string pass = BitConverter.ToString(result).Replace("-", "").ToLower();
            return pass;
        }
        public FormEntry()
        {
            InitializeComponent();
        }
        private void btOK_Click(object sender, EventArgs e)
        {
           
            if ((tbLogin.Text.Trim() == "") || (tbPassword.Text == ""))
            {
                MessageBox.Show(@"Заполните логин и пароль!");
            }

            else
            {
                using (var sConn = new SqlConnection(_sConnStr))
                {
                    var sCommandSalt = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"SELECT Salt FROM Пользователи WHERE Login = @login"
                    };
                    sConn.Open();
                    sCommandSalt.Parameters.AddWithValue("@login", tbLogin.Text.Trim());
                    string salt = (string)sCommandSalt.ExecuteScalar();

                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = "select *  from Пользователи where Login = @login and Password = @password" //поиск пользователей с таким же логином
                    };

                    sCommand.Parameters.AddWithValue("@login", tbLogin.Text.Trim());
                    sCommand.Parameters.AddWithValue("@password", hash(salt, tbPassword.Text));


                    if (sCommand.ExecuteScalar() != null)
                    {
                        int id = (int)sCommand.ExecuteScalar();
                        var sCommand1 = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = "select Статус  from Пользователи where [id пользователя] = @id" //поиск пользователей с таким же логином
                        };
                        sCommand1.Parameters.AddWithValue("@id", id);
                        string st = (string)sCommand1.ExecuteScalar();
                        var fAdmin = new FormAdmin(false);
                        fAdmin.Text = "Вы вошли как " + st;
                        if (st == "Администратор") fAdmin.User = true;
                        else fAdmin.User = false;
                        fAdmin.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(@"Пользователь с таким логином и паролем не найден!");
                    }
                }
            }
        }
        private void btGuest_Click(object sender, EventArgs e)
        {
            var fGuest = new FormAdmin(true);
            fGuest.User = false;
            fGuest.Text = "Вы вошли как гость";
            if (fGuest.ShowDialog() == DialogResult.OK)
            { }
        }
        private void btReg_Click(object sender, EventArgs e)
        {
            if (!empty(tbLogin.Text, tbPassword.Text))
            {
                if (unique(tbLogin.Text))
                {
                    using (var sConn = new SqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand1 = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"INSERT INTO [dbo].[Пользователи]
                                                        ([Login],[Password], [Статус], [Salt])
                                                    VALUES
                                                        (@Login, @Password, @level, @salt)"
                        }; // в таблицу в SQL
                        sCommand1.Parameters.AddWithValue("@login", tbLogin.Text);
                        string salt = GenSalt(32);
                        sCommand1.Parameters.AddWithValue("@salt", salt);
                        string pass = hash(salt, tbPassword.Text);
                        sCommand1.Parameters.AddWithValue("@password", pass);
                        sCommand1.Parameters.AddWithValue("@level", "Оператор");
                        sCommand1.ExecuteNonQuery();
                        MessageBox.Show("Пользователь с логином " + tbLogin.Text + ", паролем " + tbPassword.Text + " и статусом оператор добавлен успешно!");
                    }
                }
                else MessageBox.Show("Логин не уникален! ");
            }
        }
    }
}
