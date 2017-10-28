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
        public string server;
        public string database;

        public Connect_DB()
        {
            InitializeComponent();
        }

        private void Connect_DB_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader read = new StreamReader("autofillconnect.txt");
                textBox2.Text = read.ReadLine();
                textBox3.Text = read.ReadLine();
                read.Close();
            }
            catch
            {
                MessageBox.Show("Упс! Ошибка в Connect_DB_Load");
            }
        }

        private void btnConnectDB(object sender, EventArgs e)
        {
            server = textBox2.Text;
            database = textBox3.Text;

            StreamWriter write = new StreamWriter("autofillconnect.txt");
            string str =textBox2.Text + "\n" + textBox3.Text;
            write.Write(str);
            write.Close();

            this.Close();
        }
    }
}
