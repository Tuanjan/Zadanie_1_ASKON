using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zadanie_1
{
    public partial class Connect_DB : Form
    {
        public Connect_DB()
        {
            InitializeComponent();
        }

        private void Connect_DB_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader read = new StreamReader("autofill.txt");
                textBox1.Text = read.ReadLine();
                textBox2.Text = read.ReadLine();
                textBox3.Text = read.ReadLine();
                read.Close();
            }
            catch
            {
                
            }
        }

        private void btnConnectDB(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter("autofill.txt");
            string str = textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text;
            write.Write(str);
            write.Close();
        }
    }
}
