using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw_3
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder builder;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "MyDB";
            builder.DataSource = "localhost";
            connection = new SqlConnection();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = builder.ToString();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                MessageBox.Show("Подключение открыто");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить подключение! ");
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string connectionString = builder.ToString();
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = "select * from People;";
                sqlCommand.CommandText = query;
                try
                {
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<People> peoples = new List<People>();
                    while (sqlDataReader.Read())
                    {
                        peoples.Add(new People
                        {
                            Id = Guid.Parse(sqlDataReader["id"].ToString()),
                            FullName = sqlDataReader["fullName"].ToString(),
                            Age = Int32.Parse(sqlDataReader["age"].ToString()),
                            PhoneNumber = sqlDataReader["phoneNumber"].ToString(),
                        });
                    }
                    foreach (People people in peoples)
                    {
                        MessageBox.Show(people.FullName +"\n" + people.Age.ToString() +"\n" + people.PhoneNumber);
                    }
                }
                catch (InvalidOperationException exception)
                {
                    MessageBox.Show("Для выполнения операции нужно открыть подключение");
                }
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            connection.Close();
            MessageBox.Show("Подключение закрыто");
        }
    }
}
