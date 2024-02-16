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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void building_signBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.building_signBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.convers' table. You can move, or remove it, as needed.
            this.conversTableAdapter.Fill(this.museumDataSet.convers);
            // TODO: This line of code loads data into the 'museumDataSet.building_sign' table. You can move, or remove it, as needed.
            this.building_signTableAdapter.Fill(this.museumDataSet.building_sign);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال اسم المبنى  الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select buildingname from building_sign where (buildingname=@buildingname )", mycon);
                SqlParameter p = new SqlParameter("@buildingname", textBox1.Text);
                mycom.CommandType = CommandType.Text;
                mycom.Parameters.Add(p);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الاسم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }
                else
                {
                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("select * from building_sign where (buildingname=@buildingname)", mycon);
                    SqlParameter p1 = new SqlParameter("@buildingname", textBox1.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p1);
                    myreader = mycom1.ExecuteReader();
                    while (myreader.Read())
                    {
                        buildingnumTextBox.Text = myreader[0].ToString();
                        buildingnameTextBox.Text = myreader[1].ToString();
                        geographicplaceTextBox.Text = myreader[2].ToString();
                        signreasonsTextBox.Text = myreader[3].ToString();
                        signdateDateTimePicker.Text = myreader[4].ToString();

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
       
            if (buildingnumTextBox.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم المبنى", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select buildingnum from building_sign where buildingnum=" + buildingnumTextBox.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();

                if (myreader.HasRows)
                {
                    MessageBox.Show("رقم المبنى موجود مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {
                    tableAdapterManager.building_signTableAdapter.add(Convert.ToInt32(buildingnumTextBox.Text), buildingnameTextBox.Text, geographicplaceTextBox.Text, signreasonsTextBox.Text, Convert.ToDateTime(signdateDateTimePicker.Text));
                    buildingnumTextBox.Text = "";
                    buildingnameTextBox.Text = "";
                    geographicplaceTextBox.Text = "";
                    signreasonsTextBox.Text = "";
                    signdateDateTimePicker.Text = "";
                    MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        
            if (buildingnumTextBox.Text == "")
                MessageBox.Show("الرجاء اختيار بيانات المبنى اللازم للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select buildingnum from convers where (buildingnum=@buildingnum )", mycon);
                SqlParameter p = new SqlParameter("@buildingnum", buildingnumTextBox.Text);
                mycom.CommandType = CommandType.Text;
                mycom.Parameters.Add(p);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows)
                {
                    MessageBox.Show("يجب حذف الارتباطات الفرعية لهذا المبنى في جدول المخالفات قبل حذف المبنى", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();

                }
                else
                {
                    myreader.Close();

                    SqlConnection mycon1 = new SqlConnection(ConnectionString.con);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("select buildingnum from rebuild where (buildingnum=@buildingnum )", mycon1);
                    SqlParameter p1 = new SqlParameter("@buildingnum", buildingnumTextBox.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p1);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    if (myreader1.HasRows)
                    {
                        MessageBox.Show("يجب حذف الارتباطات الفرعية لهذا المبنى في جدول الترميم قبل حذف المبنى", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        mycon1.Close();

                    }
                    else if (Convert.ToString(MessageBox.Show("هل انت متأكد من انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
                    {
                        tableAdapterManager.building_signTableAdapter.DeleteQuery(Convert.ToInt32(buildingnumTextBox.Text));
                        buildingnumTextBox.Text = "";
                        buildingnameTextBox.Text = "";
                        geographicplaceTextBox.Text = "";
                        signreasonsTextBox.Text = "";
                        signdateDateTimePicker.Text = "";
                        MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
             
       
            if (buildingnumTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار المبنى اللازم للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.building_signTableAdapter.UpdateQuery(Convert.ToInt32(buildingnumTextBox.Text), buildingnameTextBox.Text, geographicplaceTextBox.Text, signreasonsTextBox.Text, Convert.ToDateTime(signdateDateTimePicker.Text), Convert.ToInt32(buildingnumTextBox.Text));
                buildingnumTextBox.Text = "";
                buildingnameTextBox.Text = "";
                geographicplaceTextBox.Text = "";
                signreasonsTextBox.Text = "";
                signdateDateTimePicker.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            buildingnumTextBox.Text = "";
            buildingnameTextBox.Text = "";
            geographicplaceTextBox.Text = "";
            signreasonsTextBox.Text = "";
            signdateDateTimePicker.Text = "";
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            this.building_signTableAdapter.Fill(this.museumDataSet.building_sign);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        
            if (textBox2.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم المخالفة الذي تريد البحث عنها", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select conversnum from convers where conversnum=" + textBox2.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الرقم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {

                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("select * from convers where conversnum=" + textBox2.Text, mycon);
                    myreader = mycom1.ExecuteReader();
                    while (myreader.Read())
                    {
                        buildingnumTextBox1.Text = myreader[0].ToString();
                        conversnumTextBox.Text = myreader[1].ToString();
                        converstypeTextBox.Text = myreader[2].ToString();
                        reportTextBox.Text = myreader[3].ToString();

                    }
                    mycon.Close();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
       
            if (conversnumTextBox.Text == "")
            {
                MessageBox.Show("الرجاء ادخال رقم المخالفة", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {

                SqlConnection mycon1 = new SqlConnection(ConnectionString.con);
                mycon1.Open();
                SqlCommand mycom1 = new SqlCommand("select buildingnum from building_sign where buildingnum=" + buildingnumTextBox1.Text, mycon1);
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
                    SqlCommand mycom = new SqlCommand("select conversnum from convers where conversnum=" +  conversnumTextBox.Text, mycon);
                    SqlDataReader myreader = mycom.ExecuteReader();

                    if (myreader.HasRows)
                    {
                        MessageBox.Show("رقم المخالفة موجودة مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        mycon.Close();
                    }

                    else
                    {
                        tableAdapterManager.conversTableAdapter.add(Convert.ToInt32(buildingnumTextBox1.Text),Convert.ToInt32(conversnumTextBox.Text),converstypeTextBox.Text,reportTextBox.Text);
                        buildingnumTextBox1.Text = "";
                        conversnumTextBox.Text = "";
                        converstypeTextBox.Text = "";
                        reportTextBox.Text = "";
                        MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    }
                }
          
        }
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        
            if (conversnumTextBox.Text == "")
                MessageBox.Show("الرجاء اختيار بيانات المخالفة اللازمة للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متأكد من انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.conversTableAdapter.DeleteQuery(Convert.ToInt32(conversnumTextBox.Text));
                buildingnumTextBox1.Text = "";
                conversnumTextBox.Text = "";
                converstypeTextBox.Text = "";
                reportTextBox.Text = "";
                MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
       
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        
            if (conversnumTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار المخالفة اللازمة للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.conversTableAdapter.UpdateQuery(Convert.ToInt32(buildingnumTextBox1.Text), Convert.ToInt32(conversnumTextBox.Text), converstypeTextBox.Text, reportTextBox.Text, Convert.ToInt32(conversnumTextBox.Text));
                buildingnumTextBox1.Text = "";
                conversnumTextBox.Text = "";
                converstypeTextBox.Text = "";
                reportTextBox.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            this.conversTableAdapter.Fill(this.museumDataSet.convers);
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            buildingnumTextBox1.Text = "";
            conversnumTextBox.Text = "";
            converstypeTextBox.Text = "";
            reportTextBox.Text = "";
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Close();
        }
      
        
       
        
       
    }
}
