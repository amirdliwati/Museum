using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace museum
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void drillingBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.drillingBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.drilling' table. You can move, or remove it, as needed.
            this.drillingTableAdapter.Fill(this.museumDataSet.drilling);

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            delegationnumTextBox.Text = "";
            delegationtypeComboBox.Text = "";
            drillingplaceTextBox.Text = "";
            drillingstartdateDateTimePicker.Text = "";
            drillingresaultsTextBox.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
       
        
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم بعثة التنقيب الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select delegationnum from drilling where delegationnum=" + textBox1.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الرقم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }
                else
                {
                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("select * from drilling where delegationnum=" + textBox1.Text, mycon);
                    myreader = mycom1.ExecuteReader();
                    while (myreader.Read())
                    {
                        delegationnumTextBox.Text = myreader[0].ToString();
                        delegationtypeComboBox.Text = myreader[1].ToString();
                        drillingplaceTextBox.Text = myreader[2].ToString();
                        drillingstartdateDateTimePicker.Text = myreader[3].ToString();
                        drillingresaultsTextBox.Text = myreader[4].ToString();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        
            if (delegationnumTextBox.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم البعثة", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select delegationnum from drilling where delegationnum=" + delegationnumTextBox.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();

                if (myreader.HasRows)
                {
                    MessageBox.Show("رقم البعثة موجود مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {
                    tableAdapterManager.drillingTableAdapter.add(Convert.ToInt32(delegationnumTextBox.Text), delegationtypeComboBox.Text, drillingplaceTextBox.Text, Convert.ToDateTime(drillingstartdateDateTimePicker.Text), drillingresaultsTextBox.Text);
                    delegationnumTextBox.Text = "";
                    delegationtypeComboBox.Text = "";
                    drillingplaceTextBox.Text = "";
                    drillingstartdateDateTimePicker.Text = "";
                    drillingresaultsTextBox.Text = "";
                    MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        
             if (delegationnumTextBox.Text == "")
                MessageBox.Show("الرجاء اختيار البعثة اللازمة للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متأكد من انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.drillingTableAdapter.DeleteQuery(Convert.ToInt32(delegationnumTextBox.Text));
                delegationnumTextBox.Text = "";
                delegationtypeComboBox.Text = "";
                drillingplaceTextBox.Text = "";
                drillingstartdateDateTimePicker.Text = "";
                drillingresaultsTextBox.Text = "";
                MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

            }
     
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        
            if (delegationnumTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار البعثة اللازمة للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.drillingTableAdapter.UpdateQuery(Convert.ToInt32(delegationnumTextBox.Text), delegationtypeComboBox.Text, drillingplaceTextBox.Text, Convert.ToDateTime(drillingstartdateDateTimePicker.Text), drillingresaultsTextBox.Text, Convert.ToInt32(delegationnumTextBox.Text));
                delegationnumTextBox.Text = "";
                delegationtypeComboBox.Text = "";
                drillingplaceTextBox.Text = "";
                drillingstartdateDateTimePicker.Text = "";
                drillingresaultsTextBox.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
     
        }
        
      
    }
}
