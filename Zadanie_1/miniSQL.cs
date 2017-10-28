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
    public partial class miniSQL : Form
    {
        public miniSQL()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create_DB crtDB = new Create_DB();
            crtDB.Show();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connect_DB cntDB = new Connect_DB();
            cntDB.Show();
        }
    }
}
