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

namespace CRUD_Example
{
    public partial class ContactApp : Form
    {
        int ContactID;
        SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-14EG6QS\SQLEXPRESS;Initial Catalog=Development;Integrated Security=True");
        public ContactApp()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                if(btnSave.Text == "Save")
                {
                    SqlCommand sqlcmd = new SqlCommand("EX_Contact_AddOrEdit", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "add");
                    sqlcmd.Parameters.AddWithValue("@ContactID", 0);
                    sqlcmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfully");
                }
                else
                {
                    SqlCommand sqlcmd = new SqlCommand("EX_Contact_AddOrEdit", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "edit");
                    sqlcmd.Parameters.AddWithValue("@ContactID", ContactID);
                    sqlcmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                }
                Reset();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        void FillDataGridView()
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("EX_Contact_ViewOrSearch", sqlcon);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.Parameters.AddWithValue("@Name", txtSearch.Text.Trim());
                DataTable dtbl = new DataTable();
                sqlda.Fill(dtbl);
                dgvContacts.DataSource = dtbl;
                dgvContacts.Columns[0].Visible = false;
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        void Reset()
        {
            txtName.Text = txtMobileNumber.Text = txtAddress.Text = "";
            btnSave.Text = "Save";
            ContactID = 0;
            btnDelete.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void dgvContacts_DoubleClick(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow.Index != -1)
            {
                ContactID = Convert.ToInt32(dgvContacts.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = dgvContacts.CurrentRow.Cells[1].Value.ToString();
                txtMobileNumber.Text = dgvContacts.CurrentRow.Cells[2].Value.ToString();
                txtAddress.Text = dgvContacts.CurrentRow.Cells[3].Value.ToString();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("EX_Contact_Delete", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@ContactID", ContactID);
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                Reset();
                FillDataGridView();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
