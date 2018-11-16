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

namespace Client
{
    public partial class FormLogin : Form
    {

        public FormLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
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
                MessageBox.Show("USER DOES NOT EXIST","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //Register user
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\School\\DNP1\\ProjectDNP\\DNP_Chat\\Chat\\Client\\Database1.mdf;Integrated Security=True");
            con.Open();
            SqlCommand sc = new SqlCommand ("INSERT INTO Users values('"+ NameTextBox.Text + "','"+PasswordTextbox.Text+"');",con);
            object o = sc.ExecuteNonQuery();
            MessageBox.Show(o + ": New user has been added");
            con.Close();
            NameTextBox.Clear();
            PasswordTextbox.Clear();
        }
    }
}
