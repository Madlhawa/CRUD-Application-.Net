using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CRUD_Example_2
{
    public partial class Form_Settings : Form
    {
        public Form_Settings()
        {
            InitializeComponent();
        }

        string connectionString, server, database = "";
        private void Form_Settings_Load(object sender, EventArgs e)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString.ToString();

            char[] delimiterChars = { '=', ';' };
            string[] words = connectionString.Split(delimiterChars);

            server = words[Array.IndexOf(words, "\"data source") + 1];
            database = words[Array.IndexOf(words, "initial catalog") + 1];

            txtServer.Text = server;
            txtDatabase.Text = database;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string updatedConnectionString = connectionString.Replace(server, txtServer.Text);
            updatedConnectionString = updatedConnectionString.Replace(database, txtDatabase.Text);

            MessageBox.Show(updatedConnectionString);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings[DBEntities].ConnectionString = textBox1.Text;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
