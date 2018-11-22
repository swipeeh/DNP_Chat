using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    
    class DatabaseAdapter
    {
        /*private bool isPasswordOk()
        {
            if (something.Text == "") 
            {
                MessageBox.Show("Please enter valid name.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isNameOk()
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Please enter valid name.");
                return false;
            }
            else
            {
                return true;
            }
        }*/

        private void loginUser(object sender, EventArgs e)
        {

            /* MainForm mainForm = new MainForm();
             FormLogin loginForm = new FormLogin();

             //Login authentificator
             SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\School\\DNP1\\ProjectDNP\\DNP_Chat\\Chat\\Client\\Database1.mdf;Integrated Security=True");
             con.Open();
             SqlDataAdapter sda = new SqlDataAdapter("select count (*) from Users where loginName ='" + NameTextBox.Text + "' and password ='" + PasswordTextbox.Text + "'", con);
             DataTable dt = new DataTable();
             sda.Fill(dt);
             if (dt.Rows[0][0].ToString() == "1")
             {
                 mainForm.Show();
                 this.Hide();
             }
             else
                 MessageBox.Show("USER DOES NOT EXIST","Error",MessageBoxButtons.OK,MessageBoxIcon.Error); */


                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    const String sql = "SELECT * FROM UsersInfo WHERE loginName = '@LoginName' AND 'password' = @Password";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar, 50));
                        sqlCommand.Parameters["@LoginName"].Value = NameTextBox.Text;

                        sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50));
                        sqlCommand.Parameters["@Password"].Value = PasswordTextbox.Text;

                        try
                        {
                            connection.Open();

                            SqlDataReader dataReader = sqlCommand.ExecuteReader();
                            if ((dataReader.Read() == true))
                            {
                                MessageBox.Show("Logged in");
                               // mainForm.Show();
                               
                            }
                            else
                            {
                                MessageBox.Show("USER DOES NOT EXIST", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                        catch
                        {
                            MessageBox.Show("Some SQL error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
        }

        public void registerUser(object sender,EventArgs e)
        {
           
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("NewLogin", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar, 50));
                        sqlCommand.Parameters["@LoginName"].Value = NameTextBox.Text;

                        sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50));
                        sqlCommand.Parameters["@Password"].Value = PasswordTextbox.Text;

                        try
                        {
                            connection.Open();
                            sqlCommand.ExecuteNonQuery();

                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show("Invalid values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            MessageBox.Show("New user has been added");
                            connection.Close();
                        }
                    }
                }

        }
    }
}
