using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CRUD_Example_2
{
    public partial class CRUDApp : Form
    {
        EX_Contact contact = new EX_Contact(); 
        public CRUDApp()
        {
            InitializeComponent();
        }

        void Clear()
        {
            txtCity.Text = txtName.Text = txtNumber.Text = "";
            contact.ContactID = 0;
            btnCreate.Text = "Create";
            btnDelete.Enabled = false;
        }
        void Populate()
        {
            using (DBEntities db = new DBEntities())
            {
                dgvContacts.DataSource = db.EX_Contact.ToList();
            }
        }

        private void CRUDApp_Load(object sender, EventArgs e)
        {
            Clear();
            Populate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            contact.Name = txtName.Text.Trim();
            contact.MobileNumber = txtNumber.Text.Trim();
            contact.Address = txtCity.Text.Trim();
            using (DBEntities db = new DBEntities())
            {
                if (contact.ContactID == 0)
                    db.EX_Contact.Add(contact);
                else
                    db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            Populate();
            MessageBox.Show("Contact Created");
        }

        private void dgvContacts_DoubleClick(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentRow.Index != -1)
            {
                contact.ContactID = Convert.ToInt32(dgvContacts.CurrentRow.Cells[0].Value);
                using (DBEntities db = new DBEntities())
                {
                    contact = db.EX_Contact.Where(x => x.ContactID == contact.ContactID).FirstOrDefault();
                    txtName.Text = contact.Name;
                    txtNumber.Text = contact.MobileNumber;
                    txtCity.Text = contact.Address;
                    btnCreate.Text = "Edit";
                    btnDelete.Enabled = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?","EF CRUD Operation",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (DBEntities db = new DBEntities())
                {
                    var entry = db.Entry(contact);
                    if (entry.State == EntityState.Detached)
                        db.EX_Contact.Attach(contact);
                    db.EX_Contact.Remove(contact);
                    db.SaveChanges();
                    Clear();
                    Populate();
                    MessageBox.Show("Deleted Successfully");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Settings f_setings = new Form_Settings();
            f_setings.ShowDialog();
        }
    }
}
