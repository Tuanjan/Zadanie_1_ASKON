using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie_1
{
    public partial class CreateDBMSSQL : Form
    {
        public CreateDBMSSQL()
        {
            InitializeComponent();
        }

        private void btnCreateDatabase(object sender, EventArgs e)
        {
            Inquiries test = new Inquiries();
            test.CreateDB(textBox1.Text);
            test.CreateTable(textBox1.Text);
        }

        private void btnConnectDatabase(object sender, EventArgs e)
        {
            Inquiries test = new Inquiries();
            test.ConnectDB(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
