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

namespace ARM
{
    public partial class FormUserList : Form
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
            if ((login == "") || (password == ""))
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
        public FormUserList()
        {
            InitializeComponent();
            InitializeLv();
        }

        private void InitializeLv()
        {
            lv_user.Columns.Add("Login", "Логин"); // добавляем столбцы в листвью
            lv_user.Columns.Add("Password", "Пароль");
            lv_user.Columns.Add("Статус", "Статус");
            lv_user.Columns.Add("Date", "Дата регистрации");
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT [Login]
                                          ,[Password]
                                          , Статус
                                          ,[Date]
                                      FROM [dbo].[Пользователи]"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var lvi = new ListViewItem(new[]
                    {
                        (string)reader["Login"],
                        (string)reader["Password"],
                        (string)reader["Статус"],
                        ((DateTime)reader["Date"]).ToLongDateString(),
                    });
                    lvi.Tag = (DateTime)reader["Date"];
                    lv_user.Items.Add(lvi);// добавляем новые элементы
                }
            }
            lv_user.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); // задаем авторазмер для столбца
            lv_user.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formInsert = new FormUser(FormUser.FormType.Insert); //создаем экземпляр формы
            if (formInsert.ShowDialog() == DialogResult.OK) //чтобы не мог переключаться между разными формами. 
            {
                string log = formInsert.login.Trim();
                if (!unique(log))
                {
                    MessageBox.Show(@"Логин не уникальный! Измените логин!");
                    добавитьToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    if (empty(log, formInsert.password))
                    {
                        добавитьToolStripMenuItem_Click(sender, e);
                    }
                    else
                    {
                        using (var sConn = new SqlConnection(_sConnStr))
                        {
                            sConn.Open();
                            var sCommand1 = new SqlCommand
                            {
                                Connection = sConn,
                                CommandText = @"INSERT INTO [dbo].[Пользователи]
                                                    ([Login],[Password], [Статус],[Date], [Salt])
                                                VALUES
                                                    (@Login, @Password, @level, @Date, @salt)"
                            }; // в таблицу в SQL
                            sCommand1.Parameters.AddWithValue("@login", log);
                            string salt = GenSalt(32);
                            sCommand1.Parameters.AddWithValue("@salt", salt);

                            string pass = hash(salt, formInsert.password);
                            sCommand1.Parameters.AddWithValue("@password", pass);
                            sCommand1.Parameters.AddWithValue("@Date", formInsert.date);
                            if (formInsert.level == 0)
                            {
                                sCommand1.Parameters.AddWithValue("@level", "Администратор");
                                var lvi = new ListViewItem(new[]
                                    {
                                        log,
                                        pass,
                                        "Администратор",
                                        formInsert.date.ToLongDateString(),
                                    });
                                lvi.Tag = formInsert.date;
                                lv_user.Items.Add(lvi);
                            }
                            else
                            {
                                sCommand1.Parameters.AddWithValue("@level", "Оператор");
                                var lvi = new ListViewItem(new[]
                                   {
                                        log,
                                        pass,
                                        "Оператор",
                                        formInsert.date.ToLongDateString(),
                                    });
                                lvi.Tag = formInsert.date;
                                lv_user.Items.Add(lvi);
                            }
                           
                            sCommand1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in lv_user.SelectedItems)
            {
                DateTime ModifiedDate = (DateTime)selectedItem.Tag;
                var formUpdate = new FormUser(FormUser.FormType.Update);
                formUpdate.login = selectedItem.SubItems[0].Text;
                string oldlogin = selectedItem.SubItems[0].Text;
                formUpdate.password = selectedItem.SubItems[1].Text;
                string oldpass = selectedItem.SubItems[1].Text;
                formUpdate.date = ModifiedDate;
                if (formUpdate.ShowDialog() == DialogResult.OK) //чтобы не мог переключаться между разными формами. 
                {
                    string log = formUpdate.login.Trim();
                    if (!unique(log) & formUpdate.login != oldlogin)
                    {
                        MessageBox.Show(@"Логин не уникальный! Измените логин!");
                        изменитьToolStripMenuItem_Click(sender, e);
                    }
                    else
                    {
                        if (empty(log, formUpdate.password))
                        {
                            изменитьToolStripMenuItem_Click(sender, e);
                        }
                        else
                        {
                            using (var sConn = new SqlConnection(_sConnStr))
                            {
                                sConn.Open();
                                if (formUpdate.password != oldpass) //поменяли пароль
                                {
                                    var sCommandNew = new SqlCommand
                                    {
                                        Connection = sConn,
                                        CommandText = @"UPDATE [dbo].[Пользователи]
                                                            SET [Login] = @newlogin
                                                                ,[Salt] = @salt
                                                                ,[Password] = @password
                                                                ,[Статус] = @level
                                                                ,[Date] = @date
                                                            WHERE Login = @oldLogin"
                                    };
                                    sCommandNew.Parameters.AddWithValue("@oldLogin", oldlogin);
                                    sCommandNew.Parameters.AddWithValue("@newlogin", log);
                                    string salt = GenSalt(32);
                                    sCommandNew.Parameters.AddWithValue("@salt", salt);
                                    string pass = hash(salt, formUpdate.password);
                                    sCommandNew.Parameters.AddWithValue("@password", pass);
                                    if (formUpdate.level == 0)
                                    {
                                        sCommandNew.Parameters.AddWithValue("@level", "Администратор");
                                        selectedItem.SubItems[2].Text = "Администратор";
                                    }
                                    else
                                    {
                                        sCommandNew.Parameters.AddWithValue("@level", "Оператор");
                                        selectedItem.SubItems[2].Text = "Оператор";
                                    }
                                    sCommandNew.Parameters.AddWithValue("@date", formUpdate.date);
                                    sCommandNew.ExecuteNonQuery();
                                    selectedItem.SubItems[0].Text = log;
                                    selectedItem.SubItems[1].Text = pass;
                                }
                                else
                                {
                                    var sCommandOld = new SqlCommand
                                    {
                                        Connection = sConn,
                                        CommandText = @"UPDATE [dbo].[Пользователи]
                                                            SET [Login] = @newlogin
                                                                ,[Статус] = @level
                                                                ,[Date] = @date
                                                            WHERE Login = @oldLogin"
                                    };
                                    sCommandOld.Parameters.AddWithValue("@oldLogin", oldlogin);
                                    sCommandOld.Parameters.AddWithValue("@newlogin", log);
                                    if (formUpdate.level == 0)
                                    {
                                        sCommandOld.Parameters.AddWithValue("@level", "Администратор");
                                        selectedItem.SubItems[2].Text = "Администратор";
                                    }
                                    else
                                    {
                                        sCommandOld.Parameters.AddWithValue("@level", "Оператор");
                                        selectedItem.SubItems[2].Text = "Оператор";
                                    }
                                    sCommandOld.Parameters.AddWithValue("@password", oldpass);
                                    sCommandOld.Parameters.AddWithValue("@date", formUpdate.date);
                                    sCommandOld.ExecuteNonQuery();
                                    selectedItem.SubItems[0].Text = log;
                                    selectedItem.SubItems[1].Text = oldpass;
                                }
                                selectedItem.SubItems[3].Text = formUpdate.date.ToLongDateString();
                            }
                        }
                    }
                }
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in lv_user.SelectedItems)
            {
                using (var sConn = new SqlConnection(_sConnStr))
                {
                    sConn.Open();
                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"delete from [Пользователи] where Login = @Login"
                    };
                    // var selectedItemLog = selectedItem.Text;
                    sCommand.Parameters.AddWithValue("@Login", selectedItem.Text);
                    sCommand.ExecuteNonQuery();//результат не нужен, выполнили и забыли
                    lv_user.Items.Remove(selectedItem); // удаляем из listView (пользовательского интерфейса)
                }
            }
        }
    }
}
