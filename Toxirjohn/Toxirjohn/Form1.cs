using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toxirjohn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tb_ModuleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'universityDataSet.tb_ModuleType' table. You can move, or remove it, as needed.
                this.tb_ModuleTypeTableAdapter.Fill(this.universityDataSet.tb_ModuleType);
                // TODO: This line of code loads data into the 'universityDataSet.tb_Module' table. You can move, or remove it, as needed.
                this.tb_ModuleTableAdapter.Fill(this.universityDataSet.tb_Module);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveLast();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.MoveFirst();
        }
        private void EnableDisable()
        {
            if (tb_ModuleBindingSource.Position == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;    
            }
            else
            {
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
            }
            if (tb_ModuleBindingSource.Position == tb_ModuleBindingSource.Count - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                    btnLast.Enabled = true;
            }
        }

        private void tb_ModuleBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            EnableDisable();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void SaveData()
        {
            if(this.Validate())
            try
            {
                    this.tb_ModuleBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.universityDataSet);
                    MessageBox.Show("Your changes saved");
                }
            catch (Exception ex)
            {
                  MessageBox.Show(ex.Message);
            }  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(tb_ModuleBindingSource.Position == 0)
            {
                MessageBox.Show("There is no information to delete");
            }
            else
            {
                var userResponse = MessageBox.Show("do you want to delete this data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(userResponse == DialogResult.Yes)
                {
                    tb_ModuleBindingSource.RemoveCurrent();
                    MessageBox.Show("Deleted");
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Validate())
            {
                this.tb_ModuleBindingSource.EndEdit();
                if (universityDataSet.HasChanges())
                {
                    if(MessageBox.Show("Do you want save all data?", "Saved", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedtype = ((DataRowView)cmbNewType.SelectedItem).Row;
                universityDataSet.tb_Module.Addtb_ModuleRow(
                    tbxNewCode.Text,
                    tbxNewName.Text,
                    Convert.ToInt32(nudNewYear.Value),
                    (UniversityDataSet.tb_ModuleTypeRow)selectedtype
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tb_ModuleBindingSource.Filter = $"ModuleName LIKE ('{tbxFilter.Text}%')";
        }

        private void moduleNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(moduleNameTextBox.Text))
            {
                MessageBox.Show("Module name cannot be empty!");
                e.Cancel = true;
            }
        }

        private void tbxNewName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(tbxNewName.Text))
            {
                MessageBox.Show("Module name cannot be empty!");
                e.Cancel = true;
            }
        }
    }
}
