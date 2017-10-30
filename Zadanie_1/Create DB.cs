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
    public partial class Create_DB : Form
    {
        public string file;
        public string server;
        public string database;

        public Create_DB()
        {
            InitializeComponent();
        }

        private void btnCreateDB(object sender, EventArgs e)
        {
            try
            {
                Inquiries a = new Inquiries(textBox1.Text, textBox2.Text);
                a.CreateDB(textBox3.Text);

                file = textBox1.Text;
                server = textBox2.Text;
                database = textBox3.Text;

                StreamWriter write = new StreamWriter("autofillcreate.txt");
                string str = textBox1.Text + "\n" + textBox2.Text;
                write.Write(str);
                write.Close();

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in btnCreateDB: " + ex.Message);
            }
        }

        private void Create_DB_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists("autofillcreate.txt"))
                {
                    StreamReader read = new StreamReader("autofillcreate.txt");
                    textBox1.Text = read.ReadLine();
                    textBox2.Text = read.ReadLine();
                    read.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in Create_DB_Load: " + ex.Message);
            }
        }
    }
}
