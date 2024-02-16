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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(System.DateTime.Now);
            label5.Text = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            label3.Text = Convert.ToString(dateTimePicker1.Value.Hour) + ":" + Convert.ToString(dateTimePicker1.Value.Minute) + ":" + Convert.ToString(dateTimePicker1.Value.Second); 
        }

        private void تسجيلالخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("شكراً لك", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            this.Close();
        }

        private void حولالبرنامجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            f.ShowDialog();
        }

        private void إضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "" || toolStripTextBox2.Text == "" || toolStripTextBox5.Text == "")
            {
                MessageBox.Show(" الرجاء كتابة اسم المستخدم أو كلمة المرور أو الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

            }
            else
            {
                SqlConnection mycon = new SqlConnection(ConnectionString.con);

                mycon.Open();
                SqlCommand mycom = new SqlCommand("SELECT * FROM manager WHERE  (num = @num)", mycon);
                SqlParameter p = new SqlParameter("@num", toolStripTextBox5.Text);


                mycom.Parameters.Add(p);

                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows)
                {
                    MessageBox.Show("الرقم موجود مسبقا  الرجاء كتابة رقم غير مستخدم وشكراً ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }
                else
                {
                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("SELECT * FROM manager WHERE  (user_name = @user_name)", mycon);
                    SqlParameter p1 = new SqlParameter("@user_name", toolStripTextBox1.Text);


                    mycom1.Parameters.Add(p1);

                    myreader = mycom1.ExecuteReader();
                    if (myreader.HasRows)
                    {
                        MessageBox.Show("اسم المستخدم موجود مسبقا  الرجاء كتابة اسم غير مستخدم وشكراً ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        mycon.Close();
                    }

                    else
                    {
                        myreader.Close();

                        SqlCommand mycom3 = new SqlCommand("INSERT INTO [dbo].[manager] ([num], [user_name], [password]) VALUES(@num, @user_name, @password)", mycon);
                        SqlParameter p4 = new SqlParameter("@num", toolStripTextBox5.Text);
                        SqlParameter p5 = new SqlParameter("@user_name", toolStripTextBox1.Text);
                        SqlParameter p3 = new SqlParameter("@password", toolStripTextBox2.Text);

                        mycom3.Parameters.Add(p4);
                        mycom3.Parameters.Add(p5);
                        mycom3.Parameters.Add(p3);
                        SqlDataReader myreader1 = mycom3.ExecuteReader();
                        mycon.Close();
                        MessageBox.Show("تم إضافة اسم المستخدم وكلمة المرور بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

                    }
                }
            }
        }

        private void حذفToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (toolStripTextBox3.Text == "" || toolStripTextBox4.Text == "" || toolStripTextBox6.Text == "")
            {
                MessageBox.Show(" الرجاء كتابة اسم المستخدم أو كلمة المرور أو الرقم", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

            }
            else
            {


                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("SELECT * FROM manager WHERE  (num = @num)", mycon);
                SqlParameter p = new SqlParameter("@num", toolStripTextBox6.Text);


                mycom.Parameters.Add(p);

                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("الرجاء كتابة الرقم اللازم للحذف ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }
                else
                {
                    myreader.Close();
                    SqlCommand mycom1 = new SqlCommand("SELECT * FROM manager WHERE  (user_name = @user_name)", mycon);
                    SqlParameter p1 = new SqlParameter("@user_name", toolStripTextBox3.Text);

                    mycom1.CommandType = CommandType.Text;
                    mycom1.Parameters.Add(p1);

                    myreader = mycom1.ExecuteReader();
                    if (myreader.HasRows == false)
                    {
                        MessageBox.Show("الرجاء كتابة اسم المستخدم اللازم للحذف ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        mycon.Close();
                    }
                    else
                    {
                        myreader.Close();
                        SqlCommand mycom2 = new SqlCommand("SELECT * FROM manager WHERE  (password = @password)", mycon);
                        SqlParameter p2 = new SqlParameter("@password", toolStripTextBox4.Text);
                        mycom2.CommandType = CommandType.Text;
                        mycom2.Parameters.Add(p2);
                        myreader = mycom2.ExecuteReader();
                        if (myreader.HasRows == false)
                        {
                            MessageBox.Show(" الرجاء كتابة كلمة المرور اللازمة للحذف ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                            mycon.Close();
                        }


                        else
                        {
                            myreader.Close();

                            SqlCommand mycom3 = new SqlCommand("DELETE FROM [dbo].[manager] WHERE (([num] = @num) AND ([user_name] = @user_name) AND ([password] = @password))", mycon);
                            SqlParameter p4 = new SqlParameter("@num", toolStripTextBox6.Text);
                            SqlParameter p5 = new SqlParameter("@user_name", toolStripTextBox3.Text);
                            SqlParameter p3 = new SqlParameter("@password", toolStripTextBox4.Text);
                            mycom3.CommandType = CommandType.Text;
                            mycom3.Parameters.Add(p4);
                            mycom3.Parameters.Add(p5);
                            mycom3.Parameters.Add(p3);
                            SqlDataReader myreader1 = mycom3.ExecuteReader();
                            mycon.Close();
                            MessageBox.Show("تم حذف اسم المستخدم وكلمة المرور بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

                        }
                    }
                }
            }

        }

        private void عرضToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11();
            f11.ShowDialog();
        }

        private void عنالمخالفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 f10 = new Form10();
            f10.ShowDialog();
        }

        private void عنموظفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 f12 = new Form12();
            f12.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(System.DateTime.Now);
            label5.Text = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            label3.Text = Convert.ToString(dateTimePicker1.Value.Hour) + ":" + Convert.ToString(dateTimePicker1.Value.Minute) + ":" + Convert.ToString(dateTimePicker1.Value.Second); 

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
        }
    }
}
