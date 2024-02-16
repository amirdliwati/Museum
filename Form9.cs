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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        
        
            private void button1_Click(object sender, EventArgs e)
        {
            if (user_nameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور خطأ الرجاء التأكد منها وشكراً ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
            else
            {
                ConnectionString c = new ConnectionString();

                SqlConnection mycon = new SqlConnection(ConnectionString.con);
                mycon.Open();
                SqlCommand mycom = new SqlCommand("SELECT * FROM manager WHERE  ((user_name = @user_name)and(password=@password))", mycon);
                SqlParameter p = new SqlParameter("@user_name", user_nameTextBox.Text);
                SqlParameter p1 = new SqlParameter("@password", passwordTextBox.Text);

                mycom.CommandType = CommandType.Text;
                mycom.Parameters.Add(p);
                mycom.Parameters.Add(p1);
                SqlDataReader myreader = mycom.ExecuteReader();
                if (myreader.HasRows == false)
                {
                    MessageBox.Show("اسم المستخدم أو كلمة المرور  خطأ الرجاء التأكد منه وشكراً ", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    mycon.Close();
                }
               
                    else {
                        
                        
                        Form1 f1 = new Form1();
                        MessageBox.Show("أهلاً بك مرة أخرى","",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);
                        
                        f1.ShowDialog();
                         
                       
                        user_nameTextBox.Text = "";
                        passwordTextBox.Text = ""; 
                    }

                }
                

            }

            private void button2_Click(object sender, EventArgs e)
            {
                if (Convert.ToString(MessageBox.Show("هل تريد الخروج؟", "خروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign)) == "Yes")
                {
                    Close();
                }
            }
        
    }
}
