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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void employeesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.museumDataSet.employees);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        
            if (textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال اسم الموظف الذي تريد البحث عنه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {


                SqlConnection MyCon = new SqlConnection(ConnectionString.con);
                MyCon.Open();

                SqlCommand MyCom = new SqlCommand("SELECT empname FROM employees WHERE  (empname = @name)", MyCon);
                SqlParameter p = new SqlParameter("@name", textBox1.Text);
                MyCom.CommandType = CommandType.Text;
                MyCom.Parameters.Add(p);

                SqlDataReader myreader = MyCom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("هذا الاسم غير موجود", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    MyCon.Close();
                }
                else
                {
                    myreader.Close();

                    SqlCommand mycom = new SqlCommand("select empno,empname,diploma,hiredate,department,governmental,behavior,notice from employees where (empname = @name)", MyCon);
                    SqlParameter s = new SqlParameter("@name", textBox1.Text);
                    mycom.CommandType = CommandType.Text;
                    mycom.Parameters.Add(s);
                    SqlDataReader MyReader = mycom.ExecuteReader();

                    while (MyReader.Read())
                    {
                        empnoTextBox.Text = MyReader[0].ToString();
                        empnameTextBox.Text = MyReader[1].ToString();
                        diplomaTextBox.Text = MyReader[2].ToString();
                        hiredateDateTimePicker.Text = MyReader[3].ToString();
                        departmentTextBox.Text = MyReader[4].ToString();
                        governmentalTextBox.Text = MyReader[5].ToString();
                        behaviorTextBox.Text = MyReader[6].ToString();
                        noticeTextBox.Text = MyReader[7].ToString();
                    }
                    MyCon.Close();




                
            }
        }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
       
            if (empnoTextBox.Text == "")
                MessageBox.Show("الرجاء ادخال رقم الموظف", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else
            {




                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("select empno from employees where empno=" + empnoTextBox.Text, mycon);
                SqlDataReader myreader = mycom.ExecuteReader();

                if (myreader.HasRows)
                {
                    MessageBox.Show("رقم الموظف موجود مسبقا الرجاء ادخال رقم غير موجود ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }

                else
                {
                    tableAdapterManager.employeesTableAdapter.add(Convert.ToInt32(empnoTextBox.Text), empnameTextBox.Text, diplomaTextBox.Text, Convert.ToDateTime(hiredateDateTimePicker.Text), departmentTextBox.Text, governmentalTextBox.Text, behaviorTextBox.Text, noticeTextBox.Text);
                    empnoTextBox.Text = "";
                    empnameTextBox.Text = "";
                    diplomaTextBox.Text = "";
                    departmentTextBox.Text = "";
                    governmentalTextBox.Text = "";
                    behaviorTextBox.Text = "";
                    noticeTextBox.Text = "";
                    MessageBox.Show("تم الحفظ بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (empnoTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار بيانات الموظف اللازم للحذف", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد الحذف", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {


                tableAdapterManager.employeesTableAdapter.DeleteQuery(Convert.ToInt32(empnoTextBox.Text));
                empnoTextBox.Text = "";
                empnameTextBox.Text = "";
                diplomaTextBox.Text = "";
                departmentTextBox.Text = "";
                governmentalTextBox.Text = "";
                behaviorTextBox.Text = "";
                noticeTextBox.Text = "";

                MessageBox.Show("تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
     
            if (empnoTextBox.Text == "")
                MessageBox.Show("الرجاء اخنيار بيانات الموظف اللازم للتعديل", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            else if (Convert.ToString(MessageBox.Show("هل انت متاكد من انك تريد التعديل", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
            {
                tableAdapterManager.employeesTableAdapter.UpdateQuery(Convert.ToInt32(empnoTextBox.Text), empnameTextBox.Text, diplomaTextBox.Text, Convert.ToDateTime(hiredateDateTimePicker.Text), departmentTextBox.Text, governmentalTextBox.Text, behaviorTextBox.Text, noticeTextBox.Text, Convert.ToInt32(empnoTextBox.Text));
                empnoTextBox.Text = "";
                empnameTextBox.Text = "";
                diplomaTextBox.Text = "";
                departmentTextBox.Text = "";
                governmentalTextBox.Text = "";
                behaviorTextBox.Text = "";
                noticeTextBox.Text = "";
                MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            empnoTextBox.Text = "";
            empnameTextBox.Text = "";
            diplomaTextBox.Text = "";
            departmentTextBox.Text = "";
            governmentalTextBox.Text = "";
            behaviorTextBox.Text = "";
            noticeTextBox.Text = "";
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Close();
        }
        
        
    }
}
