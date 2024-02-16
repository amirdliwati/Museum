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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
        
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم عملية الترميم الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select rebuildnum from rebuild where rebuildnum=" + textBox1.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الرقم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {

                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("select * from rebuild where rebuildnum=" + textBox1.Text, mycon);
                    myreader = mycom1.ExecuteReader();
                    while (myreader.Read())
                    {
                        buildingnumTextBox.Text = myreader[0].ToString();
                        rebuildnumTextBox.Text = myreader[1].ToString();
                        worktypeTextBox.Text = myreader[2].ToString();
                        nowsituationTextBox.Text = myreader[3].ToString();
                        expectedsituationTextBox.Text = myreader[4].ToString();
                        expectedcoastTextBox.Text = myreader[5].ToString();
                        directorengineersTextBox.Text = myreader[6].ToString();
                        startdateDateTimePicker.Text = myreader[7].ToString();
                        expectedfinishdateDateTimePicker.Text = myreader[8].ToString();

                    }
                    mycon.Close();
                }
            }
        

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        

            if (rebuildnumTextBox.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم عملية الترميم", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon1 = new SqlConnection(ConnectionString.con);
                mycon1.Open();
                SqlCommand mycom1 = new SqlCommand("select buildingnum from building_sign where buildingnum=" + buildingnumTextBox.Text, mycon1);
                SqlDataReader myreader1 = mycom1.ExecuteReader();
                if (myreader1.HasRows == false)
                {
                    MessageBox.Show("رقم المبنى غير موجود الرجاء التأكد منه وشكرا", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon1.Close();
                }

                else
                {
                    myreader1.Close();
                    SqlConnection mycon = new SqlConnection(ConnectionString.con);
                    mycon.Open();
                    SqlCommand mycom = new SqlCommand("select rebuildnum from rebuild where rebuildnum=" + rebuildnumTextBox.Text, mycon);
                    SqlDataReader myreader = mycom.ExecuteReader();

                    if (myreader.HasRows)
                    {
                        MessageBox.Show("رقم الترميم موجود مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        mycon.Close();
                    }

                    else
                    {
                        tableAdapterManager.rebuildTableAdapter.add(Convert.ToInt32(buildingnumTextBox.Text), Convert.ToInt32(rebuildnumTextBox.Text), worktypeTextBox.Text, nowsituationTextBox.Text, expectedsituationTextBox.Text, Convert.ToDecimal(expectedcoastTextBox.Text), directorengineersTextBox.Text, Convert.ToDateTime(startdateDateTimePicker.Text), Convert.ToDateTime(expectedfinishdateDateTimePicker.Text));
                        buildingnumTextBox.Text = "";
                        rebuildnumTextBox.Text = "";
                        worktypeTextBox.Text = "";
                        nowsituationTextBox.Text = "";
                        expectedsituationTextBox.Text = "";
                        expectedcoastTextBox.Text = "";
                        directorengineersTextBox.Text = "";
                        startdateDateTimePicker.Text = "";
                        expectedfinishdateDateTimePicker.Text = "";
                        MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    }
                }
            
        }
        }

        private void rebuildBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rebuildBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.rebuild' table. You can move, or remove it, as needed.
            this.rebuildTableAdapter.Fill(this.museumDataSet.rebuild);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rebuildnumTextBox.Text == "")
                MessageBox.Show("الرجاء اختيار بيانات الترميم اللازم للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متأكد من انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.rebuildTableAdapter.DeleteQuery(Convert.ToInt32(rebuildnumTextBox.Text));
                buildingnumTextBox.Text = "";
                rebuildnumTextBox.Text = "";
                worktypeTextBox.Text = "";
                nowsituationTextBox.Text = "";
                expectedsituationTextBox.Text = "";
                expectedcoastTextBox.Text = "";
                directorengineersTextBox.Text = "";
                startdateDateTimePicker.Text = "";
                expectedfinishdateDateTimePicker.Text = "";
                MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
    
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (buildingnumTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار بيانات الترميم اللازم للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.rebuildTableAdapter.UpdateQuery(Convert.ToInt32(buildingnumTextBox.Text), Convert.ToInt32(rebuildnumTextBox.Text), worktypeTextBox.Text, nowsituationTextBox.Text, expectedsituationTextBox.Text, Convert.ToDecimal(expectedcoastTextBox.Text), directorengineersTextBox.Text, Convert.ToDateTime(startdateDateTimePicker.Text), Convert.ToDateTime(expectedfinishdateDateTimePicker.Text), Convert.ToInt32(rebuildnumTextBox.Text));
                buildingnumTextBox.Text = "";
                rebuildnumTextBox.Text = "";
                worktypeTextBox.Text = "";
                nowsituationTextBox.Text = "";
                expectedsituationTextBox.Text = "";
                expectedcoastTextBox.Text = "";
                directorengineersTextBox.Text = "";
                startdateDateTimePicker.Text = "";
                expectedfinishdateDateTimePicker.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
       
           
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            buildingnumTextBox.Text = "";
            rebuildnumTextBox.Text = "";
            worktypeTextBox.Text = "";
            nowsituationTextBox.Text = "";
            expectedsituationTextBox.Text = "";
            expectedcoastTextBox.Text = "";
            directorengineersTextBox.Text = "";
            startdateDateTimePicker.Text = "";
            expectedfinishdateDateTimePicker.Text = "";
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.rebuildTableAdapter.Fill(this.museumDataSet.rebuild);
        }

        

        private void tabPage1_Enter_1(object sender, EventArgs e)
        {
            buildingnumTextBox.Text = "";
            rebuildnumTextBox.Text = "";
            worktypeTextBox.Text = "";
            nowsituationTextBox.Text = "";
            expectedsituationTextBox.Text = "";
            expectedcoastTextBox.Text = "";
            directorengineersTextBox.Text = "";
            startdateDateTimePicker.Text = "";
            expectedfinishdateDateTimePicker.Text = "";
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Close();
        }

       
     

       
       
        
    }
}
