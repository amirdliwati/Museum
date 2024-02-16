using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace museum
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void managerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.managerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.museumDataSet);

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'museumDataSet.manager' table. You can move, or remove it, as needed.
            this.managerTableAdapter.Fill(this.museumDataSet.manager);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
