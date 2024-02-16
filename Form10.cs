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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال اسم المبنى الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else if (radioButton1.Checked == true)
            {


                SqlConnection MyCon = new SqlConnection(ConnectionString.con);
                MyCon.Open();
                SqlCommand mycom = new SqlCommand("select buildingname from building_sign where (buildingname=@buildingname)", MyCon);
                SqlParameter p = new SqlParameter("@buildingname", textBox1.Text);
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

                    SqlCommand mycom1 = new SqlCommand("SELECT building_sign.buildingnum,building_sign.buildingname,building_sign.geographicplace, convers.conversnum, convers.converstype, convers.report FROM building_sign INNER JOIN convers ON building_sign.buildingnum = convers.buildingnum WHERE (building_sign.buildingname = @buildingname)", MyCon);
                    SqlParameter p1 = new SqlParameter("@buildingname", textBox1.Text);
                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p1);
                    SqlDataReader myreader1 = mycom1.ExecuteReader();
                    dataGridView2.Rows.Clear();


                    while (myreader1.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2[0, dataGridView2.Rows.Count - 1].Value = myreader1[0];
                        dataGridView2[1, dataGridView2.Rows.Count - 1].Value = myreader1[1];
                        dataGridView2[2, dataGridView2.Rows.Count - 1].Value = myreader1[2];
                        dataGridView2[3, dataGridView2.Rows.Count - 1].Value = myreader1[3];
                        dataGridView2[4, dataGridView2.Rows.Count - 1].Value = myreader1[4];
                        dataGridView2[5, dataGridView2.Rows.Count - 1].Value = myreader1[5];


                    }
                    MyCon.Close();
                }
            }
            else if (radioButton2.Checked == true)
            {

                SqlConnection MyCon1 = new SqlConnection(ConnectionString.con);
                MyCon1.Open();
                SqlCommand mycom2 = new SqlCommand("select buildingname from building_sign where (buildingname=@buildingname)", MyCon1);
                SqlParameter p2 = new SqlParameter("@buildingname", textBox1.Text);
                mycom2.CommandType = CommandType.Text;
                mycom2.Parameters.Add(p2);
                SqlDataReader myreader2 = mycom2.ExecuteReader();
                if (myreader2.HasRows == false)
                {
                    MessageBox.Show("هذا الاسم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon1.Close();
                }
                else
                {
                    myreader2.Close();
                    SqlConnection mycon2 = new SqlConnection(ConnectionString.con);
                    mycon2.Open();
                    SqlCommand mycom3 = new SqlCommand("SELECT building_sign.buildingnum,building_sign.buildingname,building_sign.geographicplace, rebuild.* FROM building_sign INNER JOIN rebuild ON building_sign.buildingnum = rebuild.buildingnum WHERE (building_sign.buildingname = @buildingname)", mycon2);
                    SqlParameter p3 = new SqlParameter("@buildingname", textBox1.Text);
                    mycom3.CommandType = CommandType.Text;
                    mycom3.Parameters.Add(p3);
                    SqlDataReader myreader3 = mycom3.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    while (myreader3.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2[0, dataGridView2.Rows.Count - 1].Value = myreader3[0];
                        dataGridView2[1, dataGridView2.Rows.Count - 1].Value = myreader3[1];
                        dataGridView2[2, dataGridView2.Rows.Count - 1].Value = myreader3[2];
                        dataGridView2[3, dataGridView2.Rows.Count - 1].Value = myreader3[4];
                        dataGridView2[4, dataGridView2.Rows.Count - 1].Value = myreader3[5];
                        dataGridView2[5, dataGridView2.Rows.Count - 1].Value = myreader3[6];
                        dataGridView2[6, dataGridView2.Rows.Count - 1].Value = myreader3[7];
                        dataGridView2[7, dataGridView2.Rows.Count - 1].Value = myreader3[8];
                        dataGridView2[8, dataGridView2.Rows.Count - 1].Value = myreader3[9];
                        dataGridView2[9, dataGridView2.Rows.Count - 1].Value = myreader3[10];
                        dataGridView2[10, dataGridView2.Rows.Count - 1].Value = myreader3[11];
                    }
                    mycon2.Close();
                }
            }
            else
            {
                MessageBox.Show("الرجاء اختيار نوع المعلومات الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)

                if (dataGridView2.Rows.Count == 0)
                    MessageBox.Show("الرجاء اختيار البيانات التي تريد طباعتها", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                else
                    PrintJbsaDataGridView.Print_Grid(dataGridView2);
            else if (radioButton2.Checked)
                if (dataGridView2.Rows.Count == 0)
                    MessageBox.Show("الرجاء اختيار البيانات التي تريد طباعتها", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                else
                    PrintJbsaDataGridView.Print_Grid(dataGridView2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView2.Visible = true;
                dataGridView2.Rows.Clear();
            }
            else
            {
                dataGridView2.Visible = false;
                dataGridView2.Rows.Clear();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                dataGridView2.Visible = true;
            else
                dataGridView2.Visible = false;
        }

    }
}
