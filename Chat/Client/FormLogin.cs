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
using System.Text.RegularExpressions;

namespace Client
{
    public partial class FormLogin : Form
    {


        public FormLogin()
        {
            InitializeComponent();
        }
        private bool isPasswordOk()
        {
            if (PasswordTextbox.Text == "")
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
        }
        private void ClearForm()
        {
            NameTextBox.Clear();
            PasswordTextbox.Clear();
        }  
        
        private void LoginButton_Click(object sender, EventArgs e)
        {
             MainForm mainForm = new MainForm();
             FormLogin loginForm = new FormLogin();

             //Login authentificator
             SqlConnection con = new SqlConnection(Properties.Settings.Default.connString);
             con.Open();
             SqlDataAdapter sda = new SqlDataAdapter("select count (*) from UsersInfo where loginName ='" + NameTextBox.Text + "' and password ='" + PasswordTextbox.Text + "'", con);
             DataTable dt = new DataTable();
             sda.Fill(dt);
             if (dt.Rows[0][0].ToString() == "1")
             {
                 mainForm.Show();
                 this.Hide();
             }
             else
                 MessageBox.Show("USER DOES NOT EXIST","Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 

          /*  MainForm mainForm = new MainForm();
            FormLogin loginForm = new FormLogin();

            if (isPasswordOk() && isNameOk())
            {
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
                                mainForm.Show();
                                this.Hide();
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
                            ClearForm();
                            connection.Close();
                        }
                    }
                } 
            } */
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (isPasswordOk() == true && isNameOk() == true)
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
                            ClearForm();
                            connection.Close();
                        }
                    }
                }
            }

        }
    }
}
