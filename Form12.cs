using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using JbsaPrintDataGridView;

namespace museum
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات التي تريد البحث عنها", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else if (radioButton1.Checked == true)
            {

                SqlConnection MyCon = new SqlConnection(ConnectionString.con);
                MyCon.Open();
                SqlCommand mycom = new SqlCommand("select empname from employees where (empname=@empname)", MyCon);
                SqlParameter p = new SqlParameter("@empname", textBox1.Text);
                mycom.CommandType = CommandType.Text;
                mycom.Parameters.Add(p);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الاسم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon.Close();
                }
                else
                {
                    myreader.Close();
                    SqlConnection mycon1 = new SqlConnection(ConnectionString.con);
                    mycon1.Open();
                    SqlCommand mycom1 = new SqlCommand("select * from employees where (empname=@empname)", mycon1);
                    SqlParameter p1 = new SqlParameter("@empname", textBox1.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p1);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (myreader1.Read())
                    {

                        dataGridView1.Rows.Add();
                        dataGridView1[0, dataGridView1.Rows.Count - 1].Value = myreader1[0];
                        dataGridView1[1, dataGridView1.Rows.Count - 1].Value = myreader1[1];
                        dataGridView1[2, dataGridView1.Rows.Count - 1].Value = myreader1[2];
                        dataGridView1[3, dataGridView1.Rows.Count - 1].Value = myreader1[3];
                        dataGridView1[4, dataGridView1.Rows.Count - 1].Value = myreader1[4];
                        dataGridView1[5, dataGridView1.Rows.Count - 1].Value = myreader1[5];
                        dataGridView1[6, dataGridView1.Rows.Count - 1].Value = myreader1[6];
                        dataGridView1[7, dataGridView1.Rows.Count - 1].Value = myreader1[7];
                    }
                    mycon1.Close();
                }
            }
            else if (radioButton2.Checked == true)
            {

                SqlConnection MyCon1 = new SqlConnection(ConnectionString.con);
                MyCon1.Open();
                SqlCommand mycom2 = new SqlCommand("select department from employees where (department=@department) ", MyCon1);
                SqlParameter p2 = new SqlParameter("@department", textBox1.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p2);
                SqlDataReader myreader2 = mycom2.ExecuteReader();
                if (myreader2.HasRows == false)
                {
                    MessageBox.Show("القسم الوظيفي غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon1.Close();
                }
                else
                {
                    myreader2.Close();
                    SqlConnection mycon2 = new SqlConnection(ConnectionString.con);
                    mycon2.Open();
                    SqlCommand mycom3 = new SqlCommand("select * from employees where (department=@department)", mycon2);
                    SqlParameter p3 = new SqlParameter("@department", textBox1.Text);
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p3);
                    SqlDataReader myreader3 = mycom3.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (myreader3.Read())
                    {

                        dataGridView1.Rows.Add();

                        dataGridView1[0, dataGridView1.Rows.Count - 1].Value = myreader3[0];
                        dataGridView1[1, dataGridView1.Rows.Count - 1].Value = myreader3[1];
                        dataGridView1[2, dataGridView1.Rows.Count - 1].Value = myreader3[2];
                        dataGridView1[3, dataGridView1.Rows.Count - 1].Value = myreader3[3];
                        dataGridView1[4, dataGridView1.Rows.Count - 1].Value = myreader3[4];
                        dataGridView1[5, dataGridView1.Rows.Count - 1].Value = myreader3[5];
                        dataGridView1[6, dataGridView1.Rows.Count - 1].Value = myreader3[6];
                        dataGridView1[7, dataGridView1.Rows.Count - 1].Value = myreader3[7];
                    }
                    mycon2.Close();
                }
            }
            else if (radioButton3.Checked == true)
            {

                SqlConnection MyCon2 = new SqlConnection(ConnectionString.con);
                MyCon2.Open();
                SqlCommand mycom4 = new SqlCommand("select diploma from employees where (diploma=@diploma) ", MyCon2);
                SqlParameter p4 = new SqlParameter("@diploma", textBox1.Text);
                mycom4.CommandType = CommandType.Text;
                mycom4.Parameters.Add(p4);
                SqlDataReader myreader4 = mycom4.ExecuteReader();
                if (myreader4.HasRows == false)
                {
                    MessageBox.Show("الشهادة العلمية غير موجودة", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon2.Close();
                }
                else
                {
                    myreader4.Close();
                    SqlConnection mycon3 = new SqlConnection(ConnectionString.con);
                    mycon3.Open();
                    SqlCommand mycom5 = new SqlCommand("select * from employees where (diploma=@diploma)", mycon3);
                    SqlParameter p5 = new SqlParameter("@diploma", textBox1.Text);
                    mycom5.CommandType = CommandType.Text;
                    mycom5.Parameters.Add(p5);
                    SqlDataReader myreader5 = mycom5.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (myreader5.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, dataGridView1.Rows.Count - 1].Value = myreader5[0];
                        dataGridView1[1, dataGridView1.Rows.Count - 1].Value = myreader5[1];
                        dataGridView1[2, dataGridView1.Rows.Count - 1].Value = myreader5[2];
                        dataGridView1[3, dataGridView1.Rows.Count - 1].Value = myreader5[3];
                        dataGridView1[4, dataGridView1.Rows.Count - 1].Value = myreader5[4];
                        dataGridView1[5, dataGridView1.Rows.Count - 1].Value = myreader5[5];
                        dataGridView1[6, dataGridView1.Rows.Count - 1].Value = myreader5[6];
                        dataGridView1[7, dataGridView1.Rows.Count - 1].Value = myreader5[7];


                    }
                    mycon3.Close();
                }
            }
            else
            {
                MessageBox.Show("الرجاء اختيار نوع المعلومات الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                MessageBox.Show("الرجاء اختيار البيانات التي تريد طباعتها", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else
                PrintJbsaDataGridView.Print_Grid(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
