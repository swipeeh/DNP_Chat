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
            mainForm.Show();
            loginForm.Close();
            
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
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
