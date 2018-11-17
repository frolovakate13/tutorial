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

namespace ARM
{

    public partial class FormAdmin : Form
    {
        private readonly string _sConnStr = new SqlConnectionStringBuilder
        {
            DataSource = @"DESKTOP-F410EJM\SQLEXPRESS",
            InitialCatalog = "Клиника",
            IntegratedSecurity = true
        }.ConnectionString;

        public class Help
        {
            string text;
            int value;
            string doc;
            string pat;
            public Help(string text, int value)
            {
                this.text = text;
                this.value = value;
                this.doc = text + " (" + (value.ToString()) +")";
            }
            public Help(int value, string text, string dop)
            {
                this.text = text + " (" + dop + ")";
                this.value = value;
                //this.pat = text + " (" + dop + ")";
            }

            public string Text
            {
                get { return text; }
            }
            public int Value
            {
                get { return value; }
            }

            public string Doc
            {
                get { return doc; }
            }
            public string Pat
            {
                get { return pat; }
            }
        }
        public bool User
        {
            get { return (UserTSMI.Enabled); }
            set
            {
                UserTSMI.Visible = value;
            }
        }
        public FormAdmin(bool flag)
        {
            InitializeComponent();
            InitializePatient(flag);
            InitializeDis(flag);
            InitializeNote(flag);
            InitializeDoctor(flag);
        }

        private void InitializeDoctor(bool flag)
        {
            if (flag)
            {
                dgvDoctor.ReadOnly = true;
                dgvDoctor.AllowUserToAddRows = false;
                dgvDoctor.AllowUserToDeleteRows = false;
            }
            else
            {
                dgvDoctor.ReadOnly = false;
                dgvDoctor.AllowUserToAddRows = true;
                dgvDoctor.AllowUserToDeleteRows = true;
            }
            dgvDoctor.Rows.Clear();
            dgvDoctor.Columns.Clear();
            dgvDoctor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id врача",
                Visible = false //скрыли от пользователя
            });
            dgvDoctor.Columns.Add("ФИО врача", "ФИО врача");
            dgvDoctor.Columns.Add("Телефон врача", "Телефон врача");
            dgvDoctor.Columns.Add("Специализация", "Специализация");
            List<Help> limit = new List<Help>();
            limit.Add(new Help("общий", 0));
            limit.Add(new Help("детский", 1));
            limit.Add(new Help("взрослый", 2));
            var Limit = new DataGridViewComboBoxColumn
            {
                Name = "Возрастные ограничения",
                HeaderText = @"Возрастные ограничения",
                DisplayMember = "Text",
                ValueMember = "Text",
                DataSource = limit
            };
            dgvDoctor.Columns.AddRange(Limit);
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();

                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT [id врача]
                                          ,[ФИО врача]
                                          ,[Телефон врача]
                                          ,[Специализация]
                                          ,[Возрастные ограничения]
                                      FROM [dbo].[Врачи]" // выборак всех данных из табл врачи
                };

                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    string str = ((string)reader["Возрастные ограничения"]).Trim();
                    dgvDoctor.Rows.Add(reader["id врача"], reader["ФИО врача"], reader["Телефон врача"],
                        reader["Специализация"], str);
                }
            }
        }
        private void InitializePatient(bool flag)
        {
            if (flag)
            {
                dgvPatient.ReadOnly = true;
                dgvPatient.AllowUserToAddRows = false;
                dgvPatient.AllowUserToDeleteRows = false;
            }
            else
            {
                dgvPatient.ReadOnly = false;
                dgvPatient.AllowUserToAddRows = true;
                dgvPatient.AllowUserToDeleteRows = true;
            }
            dgvPatient.Rows.Clear();
            dgvPatient.Columns.Clear();
            dgvPatient.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id пациента",
                Visible = false
            });
            dgvPatient.Columns.Add("ФИО пациента", "ФИО пациента");
            dgvPatient.Columns.Add(new CalendarColumn
            {
                Name = "дата рождения",
                HeaderText = @"Дата рождения"
            });

            List<Help> sex = new List<Help>();
            sex.Add(new Help("Мужской", 0));
            sex.Add(new Help("Женский", 1));
            var Sex = new DataGridViewComboBoxColumn
            {
                Name = "пол",
                HeaderText = @"Пол",
                DisplayMember = "Text",
                ValueMember = "Text",
                DataSource = sex
            };
            dgvPatient.Columns.AddRange(Sex);
            dgvPatient.Columns.Add("Телефон", "Телефон пациента");

            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT [id пациента]
                                          ,[ФИО пациента]
                                          ,[дата рождения]
                                          ,[Телефон]
                                          ,[пол]
                                      FROM [dbo].[Пациенты]"
                };

                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    dgvPatient.Rows.Add(reader["id пациента"], reader["ФИО пациента"], reader["дата рождения"],
                         ((string)reader["пол"]).Trim(), reader["Телефон"]);
                }
            }
        }
        private void InitializeNote(bool flag)
        {
            if (flag)
            {
                dgvNote.ReadOnly = true;
                dgvNote.AllowUserToAddRows = false;
                dgvNote.AllowUserToDeleteRows = false;
            }
            else
            {
                dgvNote.ReadOnly = false;
                dgvNote.AllowUserToAddRows = true;
                dgvNote.AllowUserToDeleteRows = true;
            }
            dgvNote.Rows.Clear();
            dgvNote.Columns.Clear();
            dgvNote.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id записи",
                Visible = false
            });
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                List<Help> Doc = new List<Help>();
                using (var sCommandDoc = new SqlCommand())
                {
                    sCommandDoc.CommandText = "SELECT * FROM Врачи";
                    sCommandDoc.Connection = sConn;
                    var read = sCommandDoc.ExecuteReader();
                    while (read.Read())
                    {
                        Doc.Add(new Help((string)read["ФИО врача"], (int)read["id врача"]));
                    }
                    read.Close();
                }
                var DocNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = @"ФИО врача",
                DisplayMember = "Doc",
                ValueMember = "Value",
                DataSource = Doc,
            };
            dgvNote.Columns.AddRange(DocNameColumn);

            List<Help> Pat = new List<Help>();
            using (var sCommandPat = new SqlCommand())
            {
                sCommandPat.CommandText = "SELECT * FROM Пациенты";
                sCommandPat.Connection = sConn;
                var read = sCommandPat.ExecuteReader();
                while (read.Read())
                {
                    Pat.Add(new Help((int)read["id пациента"], (string)read["ФИО пациента"], read["дата рождения"].ToString()));
                }
                read.Close();
            }
            var PatNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = @"ФИО пациента",
                DataSource = Pat,
                ValueMember = "Value",
                DisplayMember = "Text",
            };
            dgvNote.Columns.AddRange(PatNameColumn);

            dgvNote.Columns.Add("номер кабинета", "Номер кабинета");

            dgvNote.Columns.Add(new CalendarColumn
            {
                Name = "Дата приема",
                HeaderText = @"Дата приема"
            });
            dgvNote.Columns.Add("Время приема", "Время приема");
            dgvNote.Columns.Add("Примечание", "Примечание");
            
                
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT [id записи]
                                          ,[id врача]
                                          ,[id пациента]
                                          ,[номер кабинета]
                                          ,[Дата приема]
                                          ,[Время приема]
                                          ,[Примечание]
                                      FROM [dbo].[Запись]" // выборак всех данных из табл врачи
                };

                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    dgvNote.Rows.Add(reader["id записи"], reader["id врача"], reader["id пациента"],
                        reader["номер кабинета"], reader["Дата приема"], reader["Время приема"], reader["Примечание"]);
                    
                }
                
                reader.Close();
            }
        }
        private void InitializeDis( bool flag)
        {
            if (flag)
            {
                dgvDis.ReadOnly = true;
                dgvDis.AllowUserToAddRows = false;
                dgvDis.AllowUserToDeleteRows = false;
            }
            else
            {
                dgvDis.ReadOnly = false;
                dgvDis.AllowUserToAddRows = true;
                dgvDis.AllowUserToDeleteRows = true;
            }
            dgvDis.Rows.Clear();
            dgvDis.Columns.Clear();
            dgvDis.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id прививки",
                Visible = false 
            });
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                List<Help> Pat = new List<Help>();
            using (var sCommandPat = new SqlCommand())
            {
                sCommandPat.CommandText = "SELECT * FROM Пациенты";
                sCommandPat.Connection = sConn;
                var read = sCommandPat.ExecuteReader();
                while (read.Read())
                {
                    Pat.Add(new Help((int)read["id пациента"], (string)read["ФИО пациента"], read["дата рождения"].ToString()));
                }
                read.Close();
            }
            var PatNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = @"ФИО пациента",
                DataSource = Pat,
                ValueMember = "Value",
                DisplayMember = "Text",
            };
            dgvDis.Columns.AddRange(PatNameColumn);
            var DisName = new DataGridViewComboBoxColumn
            {
                HeaderText = @"Название вакцины",
                DisplayMember = "Название вакцины",
                ValueMember = "id вакцины"
            };
            dgvDis.Columns.AddRange(DisName);
            dgvDis.Columns.Add(new CalendarColumn
            {
                Name = "дата",
                HeaderText = @"Дата вакцинации"
            });

            
                
                using (var sCommandDis = new SqlCommand())
                {
                    sCommandDis.CommandText = "SELECT * FROM Вакцины";
                    sCommandDis.Connection = sConn;
                    var table = new DataTable();
                    table.Load(sCommandDis.ExecuteReader());
                    DisName.DataSource = table;
                }
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT [id прививки-пациент]
                                          ,[id пациента]
                                          ,[id вакцины]
                                          ,[дата]
                                      FROM [dbo].[Прививки]"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                   
                    dgvDis.Rows.Add(reader["id прививки-пациент"], reader["id пациента"] , reader["id вакцины"], reader["дата"]);
                }
                reader.Close();
            }
        }
        
        private void dgvPatient_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvPatient.IsCurrentRowDirty)
            {
                var row = dgvPatient.Rows[e.RowIndex];
                if (string.IsNullOrWhiteSpace((string)row.Cells["ФИО пациента"].Value) || row.Cells["ФИО пациента"].Value.ToString().Length > 51)
                {
                    row.ErrorText = "Ошибка в столбце ФИО пациента";
                    e.Cancel = true;
                }
                
                if (string.IsNullOrWhiteSpace((string)row.Cells["Телефон"].Value) || row.Cells["Телефон"].Value.ToString().Length > 12) 
                {
                    row.ErrorText = "Ошибка в столбце Телефон";
                    e.Cancel = true;
                }
                if (row.Cells["Пол"].Value == null)
                {
                    row.ErrorText = "Ошибка в столбце Пол";
                    e.Cancel = true;
                }
                if (!e.Cancel)
                {
                    using (var sConn = new SqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                        };
                        sCommand.Parameters.AddWithValue("@FIO", row.Cells["ФИО пациента"].Value);
                        sCommand.Parameters.AddWithValue("@phone", row.Cells["Телефон"].Value);
                        sCommand.Parameters.AddWithValue("@date", row.Cells["дата рождения"].Value);
                        sCommand.Parameters.AddWithValue("@sex", (string)dgvPatient[3, e.RowIndex].Value);
                        int? Id = (int?)row.Cells["id пациента"].Value;
                        if (Id.HasValue)
                        {
                            Console.WriteLine(Id.Value);
                            sCommand.CommandText = @"UPDATE [dbo].[Пациенты]
                                                          SET [ФИО пациента] = @FIO
                                                          ,[Телефон] = @phone
                                                          ,[пол] = @sex
                                                          ,[дата рождения] = @date
                                                     WHERE [id пациента] = @ID";
                            sCommand.Parameters.AddWithValue("@ID", Id.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO [dbo].[Пациенты]
                                                        ([ФИО пациента]
                                                        ,[Телефон]
                                                        ,[дата рождения], [пол] )
                                                    OUTPUT inserted.[id пациента]
                                                    VALUES
                                                        (@FIO
                                                        ,@phone
                                                        ,@date, @sex)";

                            row.Cells["id пациента"].Value = sCommand.ExecuteScalar();
                        }
                    }
                    row.ErrorText = "";
                    InitializeDis(false);
                    InitializeNote(false);
                }
            }
        }
        private void dgvPatient_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                bool flag = true;
                int id =0;
                try
                { id = (int)e.Row.Cells["id пациента"].Value; }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    var sCommandFind = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"Select * FROM [dbo].[Запись]
                                WHERE [id пациента] = @ID"
                    };
                    sCommandFind.Parameters.AddWithValue("@ID", id);
                    var sCommandFind1 = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"Select * FROM [dbo].[Прививки]
                                WHERE [id пациента] = @ID"
                    };
                    sCommandFind1.Parameters.AddWithValue("@ID", id);
                    flag = true;
                    if (sCommandFind.ExecuteScalar() != null || sCommandFind1.ExecuteScalar() != null)
                    {
                        flag = false;
                        string message = "Существуют строки в других таблицах зависимые от данной. Удалить?";
                        string str = "Удалить?";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show(message, str, buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        var sCommandDeleteNote = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"Delete FROM [dbo].[Запись]
                                WHERE [id пациента] = @ID"
                        };
                        sCommandDeleteNote.Parameters.AddWithValue("@ID", id);
                        sCommandDeleteNote.ExecuteNonQuery();
                        InitializeNote(false);
                        var sCommandDeleteDis = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"Delete FROM [dbo].[Прививки]
                                WHERE [id пациента] = @ID"
                        };
                        sCommandDeleteDis.Parameters.AddWithValue("@ID", id);
                        sCommandDeleteDis.ExecuteNonQuery();
                        InitializeDis(false);
                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"DELETE FROM [dbo].[Пациенты]
                                    WHERE [id пациента] = @ID"
                        };
                        sCommand.Parameters.AddWithValue("@ID", e.Row.Cells["id пациента"].Value);
                        sCommand.ExecuteNonQuery();

                        InitializeDis(false);
                        InitializeNote(false);
                    }
                    else e.Cancel = true;
                }
            }
        }

        private void dgvDis_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvDis.IsCurrentRowDirty)
            {
                var row = dgvDis.Rows[e.RowIndex];
                if ((string)dgvDis[2, e.RowIndex].FormattedValue == "")
                {
                    row.ErrorText = "Ошибка в столбце вакцина";
                    e.Cancel = true;
                }
                if ((string)dgvDis[1, e.RowIndex].FormattedValue == "")
                {
                    row.ErrorText = "Ошибка в столбце ФИО пациента";
                    e.Cancel = true;
                }
                if (!e.Cancel)
                {
                    using (var sConn = new SqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                        };
                        sCommand.Parameters.AddWithValue("@idpat", (int)dgvDis[1, e.RowIndex].Value);
                        sCommand.Parameters.AddWithValue("@dis", (int)dgvDis[2, e.RowIndex].Value);
                        sCommand.Parameters.AddWithValue("@date", row.Cells["дата"].Value);
                        int? Id = (int?)row.Cells["id прививки"].Value;
                        if (Id.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE [dbo].[Прививки]
                                                          SET [id пациента] = @idpat
                                                              ,[id вакцины] = @dis
                                                              ,[дата] = @date
                                                     WHERE [id прививки-пациент] = @ID";
                            sCommand.Parameters.AddWithValue("@ID", Id.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO [dbo].[Прививки]
                                                        ([id пациента], [id вакцины], [дата])
                                                    OUTPUT inserted.[id прививки-пациент]
                                                    VALUES
                                                        (@idpat, @dis, @date
                                                        )";
                            row.Cells["id прививки"].Value = sCommand.ExecuteScalar(); //записываем значение ID 
                        }
                    }
                    row.ErrorText = "";
                }
            }
        }
        private void dgvDis_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                int id = 0;
                bool flag = true;
                try
                { id = (int)e.Row.Cells["id прививки"].Value;
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"DELETE FROM [dbo].[Прививки]
                                    WHERE [id прививки-пациент] = @ID"
                    };
                    sCommand.Parameters.AddWithValue("@ID", e.Row.Cells["id прививки"].Value);
                    sCommand.ExecuteNonQuery();
                }
            }
        }

        private void dgvNote_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvNote.IsCurrentRowDirty)
            {
                var row = dgvNote.Rows[e.RowIndex];
                var cellWithPotentialErrors = new[]
                {
                    row.Cells["Номер кабинета"]
                };
                foreach (var cell in cellWithPotentialErrors)
                {
                    int n;
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)) || !int.TryParse(Convert.ToString(cell.Value), out n))
                    {
                        row.ErrorText = "Ошибка в столбце " + cell.OwningColumn.HeaderText;
                        e.Cancel = true;
                        break;
                    }
                }
                if ((string)dgvNote[1, e.RowIndex].FormattedValue == "" && (string)dgvNote[2, e.RowIndex].FormattedValue == "")
                {
                    row.ErrorText = "Ошибка!!! Укажите ФИО врача и пациента";
                    e.Cancel = true;
                }
                else if ((string)dgvNote[1, e.RowIndex].FormattedValue == "")
                {
                    row.ErrorText = "Ошибка!!! Укажите ФИО врача";
                    e.Cancel = true;
                }
                else if ((string)dgvNote[2, e.RowIndex].FormattedValue == "")
                {
                    row.ErrorText = "Ошибка!!! Укажите ФИО пациента";
                    e.Cancel = true;
                }
                
                try
                {
                    string str = (row.Cells["Время приема"].Value).ToString();
                    str.Trim();
                    DateTime time = Convert.ToDateTime(str);
                }
                catch
                {
                    row.ErrorText = "Ошибка!!! Введите время в формате час:минуты";
                    e.Cancel = true;
                }
                


                if (!e.Cancel)
                {
                    using (var sConn = new SqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                        };
                        sCommand.Parameters.AddWithValue("@idpat", (int)dgvNote[2, e.RowIndex].Value);
                        sCommand.Parameters.AddWithValue("@iddoc", (int)dgvNote[1, e.RowIndex].Value);
                        sCommand.Parameters.AddWithValue("@room", row.Cells["Номер кабинета"].Value);
                        sCommand.Parameters.AddWithValue("@date", row.Cells["Дата приема"].Value);
                        if (row.Cells["Время приема"].Value == null)
                        {
                            sCommand.Parameters.AddWithValue("@time", "");
                            row.Cells["Время приема"].Value = "00:00:00";
                        }
                        else sCommand.Parameters.AddWithValue("@time", row.Cells["Время приема"].Value);
                        if (row.Cells["Примечание"].Value == null) sCommand.Parameters.AddWithValue("@prim", "");
                        else sCommand.Parameters.AddWithValue("@prim", row.Cells["Примечание"].Value);
                        int? Id = (int?)row.Cells["id записи"].Value;
                        if (Id.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE [dbo].[Запись]
                                                          SET [id врача] = @iddoc
                                                              ,[id пациента] = @idpat
                                                              ,[номер кабинета] = @room
                                                              ,[Дата приема] = @date
                                                              ,[Время приема] = @time
                                                              ,[Примечание] = @prim
                                                     WHERE [id записи] = @ID";
                            sCommand.Parameters.AddWithValue("@ID", Id.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO [dbo].[Запись]
                                                        ([id врача]
                                                        ,[id пациента] 
                                                        ,[номер кабинета] 
                                                        ,[Дата приема] 
                                                        ,[Время приема] 
                                                        ,[Примечание] )
                                                    OUTPUT inserted.[id записи]
                                                    VALUES
                                                        (@iddoc
                                                        ,@idpat
                                                        ,@room
                                                        ,@date
                                                        ,@time, @prim)";
                            row.Cells["id записи"].Value = sCommand.ExecuteScalar();
                        }
                    }
                    row.ErrorText = "";
                }
            }
        }
        private void dgvNote_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                int id = 0;
                bool flag = true;
                try
                { id = (int)e.Row.Cells["id записи"].Value;
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"DELETE FROM [dbo].[Запись]
                                    WHERE [id записи] = @ID"
                    };
                    sCommand.Parameters.AddWithValue("@ID", e.Row.Cells["id записи"].Value);
                    sCommand.ExecuteNonQuery();
                }
            }
        }

        private void dgvDoctor_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvDoctor.IsCurrentRowDirty)
            {
                var row = dgvDoctor.Rows[e.RowIndex];
                var cellWithPotentialErrors = new[]
                {
                    row.Cells["ФИО врача"], row.Cells["Телефон врача"], row.Cells["Специализация"], row.Cells["Возрастные ограничения"]
                };
                foreach (var cell in cellWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)) || cell.Value.ToString().Length > 51)
                    {
                        row.ErrorText = "Ошибка в столбце " + cell.OwningColumn.HeaderText;
                        e.Cancel = true;
                        break; 
                    }
                }
                if (Convert.ToString(row.Cells["Телефон врача"].Value).Length > 12)
                {
                    row.ErrorText = "Ошибка в столбце запись в столбце Телефон врача слишком длинная";
                    e.Cancel = true;
                }
                if (row.Cells["Возрастные ограничения"].Value == null)
                {
                    row.ErrorText = "Ошибка в столбце Возрастные ограничения";
                    e.Cancel = true;
                }
                if (!e.Cancel)
                {
                    using (var sConn = new SqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                        };
                        sCommand.Parameters.AddWithValue("@FIO", row.Cells["ФИО врача"].Value);
                        sCommand.Parameters.AddWithValue("@phone", row.Cells["Телефон врача"].Value);
                        sCommand.Parameters.AddWithValue("@specialization", row.Cells["Специализация"].Value);
                        sCommand.Parameters.AddWithValue("@limit", row.Cells["Возрастные ограничения"].Value);
                        int? Id = (int?)row.Cells["id врача"].Value;
                        if (Id.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE [dbo].[Врачи]
                                                          SET [ФИО врача] = @FIO
                                                          ,[Телефон врача] = @phone
                                                          ,[Специализация] = @specialization
                                                          ,[Возрастные ограничения] = @limit
                                                     WHERE [id врача] = @ID";
                            sCommand.Parameters.AddWithValue("@ID", Id.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO [dbo].[Врачи]
                                                        ([ФИО врача] 
                                                          ,[Телефон врача] 
                                                          ,[Специализация]  
                                                          ,[Возрастные ограничения])
                                                    OUTPUT inserted.[id врача]
                                                    VALUES
                                                        (@FIO
                                                        ,@phone
                                                        ,@specialization
                                                        ,@limit)";
                            row.Cells["id врача"].Value = sCommand.ExecuteScalar();
                        }
                    }
                    row.ErrorText = "";
                    InitializeNote(false);
                }
            }
        }
        private void dgvDoctor_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var sConn = new SqlConnection(_sConnStr))
            {
                sConn.Open();
                int id = 0;
                bool flag = true;
                try
                { id = (int)e.Row.Cells["id врача"].Value;
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    var sCommandFind = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"Select * FROM [dbo].[Запись]
                                WHERE [id врача] = @ID"
                    };
                    sCommandFind.Parameters.AddWithValue("@ID", id);
                    flag = true;
                    if (sCommandFind.ExecuteScalar() != null)
                    {
                        flag = false;
                        string message = "Существуют строки в других таблицах зависимые от данной. Удалить?";
                        string str = "Удалить?";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show(message, str, buttons);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        var sCommandDelete = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"Delete FROM [dbo].[Запись]
                                WHERE [id врача] = @ID"
                        };
                        sCommandDelete.Parameters.AddWithValue("@ID", id);
                        sCommandDelete.ExecuteNonQuery();
                        InitializeNote(false);

                        var sCommand = new SqlCommand
                        {
                            Connection = sConn,
                            CommandText = @"DELETE FROM [dbo].[Врачи]
                                    WHERE [id врача] = @ID"
                        };
                        sCommand.Parameters.AddWithValue("@ID", e.Row.Cells["id врача"].Value);
                        sCommand.ExecuteNonQuery();
                        InitializeNote(false);     
                    }
                    else e.Cancel = true;
                }
            }
        }

        private void UserTSMI_Click(object sender, EventArgs e)
        {
            var fUser = new FormUserList();
            fUser.ShowDialog();
        }
    }
}
