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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

       

        private void divanBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.divanBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.divan' table. You can move, or remove it, as needed.
            

        }

        private void button5_Click(object sender, EventArgs e)
        {

        
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم عملية الترميم الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection MyCon = new SqlConnection(ConnectionString.con);
                SqlCommand mycom = new SqlCommand("select mailnum from divan where mailnum=" + textBox1.Text, MyCon);
                MyCon.Open();
                SqlDataReader myreader = mycom.ExecuteReader();

                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الرقم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon.Close();
                }

                else
                {
                    myreader.Close();
                    SqlCommand MyCom = new SqlCommand("Select * from divan where mailnum=" + textBox1.Text, MyCon);

                    myreader = MyCom.ExecuteReader();

                    while (myreader.Read())
                    {
                        mailnumTextBox.Text = myreader[0].ToString();
                        expropriateTextBox.Text = myreader[1].ToString();
                        importedfromTextBox.Text = myreader[2].ToString();
                        mailtypeComboBox.Text = myreader[3].ToString();
                        maicontainerTextBox.Text = myreader[4].ToString();

                    }
                    MyCon.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        
            if (mailnumTextBox.Text == "")
                MessageBox.Show("الرجاء ادخال رقم البريد", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select mailnum from divan where mailnum=" + mailnumTextBox.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();

                if (myreader.HasRows)
                {
                    MessageBox.Show("رقم البريد موجود مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {
                    tableAdapterManager.divanTableAdapter.add(Convert.ToInt32(mailnumTextBox.Text), expropriateTextBox.Text, importedfromTextBox.Text, mailtypeComboBox.Text, maicontainerTextBox.Text);
                    mailnumTextBox.Text = "";
                    expropriateTextBox.Text = "";
                    importedfromTextBox.Text = "";
                    mailtypeComboBox.Text = "";
                    maicontainerTextBox.Text = "";
                    MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
            
        }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
      
            if (mailnumTextBox.Text == "")
                MessageBox.Show("الرجاء اختيار البريد اللازم للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متأكد من انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.divanTableAdapter.DeleteQuery(Convert.ToInt32(mailnumTextBox.Text));
                mailnumTextBox.Text = "";
                expropriateTextBox.Text = "";
                importedfromTextBox.Text = "";
                mailtypeComboBox.Text = "";
                maicontainerTextBox.Text = "";
                MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        
            if (mailnumTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار البريد اللازم للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.divanTableAdapter.UpdateQuery(Convert.ToInt32(mailnumTextBox.Text), expropriateTextBox.Text, importedfromTextBox.Text, mailtypeComboBox.Text, maicontainerTextBox.Text, Convert.ToInt32(mailnumTextBox.Text));
                mailnumTextBox.Text = "";
                expropriateTextBox.Text = "";
                importedfromTextBox.Text = "";
                mailtypeComboBox.Text = "";
                maicontainerTextBox.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            mailnumTextBox.Text = "";
            expropriateTextBox.Text = "";
            importedfromTextBox.Text = "";
            mailtypeComboBox.Text = "";
            maicontainerTextBox.Text = "";
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.divanTableAdapter.Fill(this.museumDataSet.divan);
        }
        
    }
}
