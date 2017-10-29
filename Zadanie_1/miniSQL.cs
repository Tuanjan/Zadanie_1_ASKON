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
        private string connFile;
        private string connServer;
        private string connDatabase;
        private Inquiries inq;

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                inq.DisconnectDB();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in closeToolStripMenuItem: " + ex.Message);
            }
        }
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                using (Create_DB createDB = new Create_DB())
                {
                    DialogResult dr = createDB.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        connFile = createDB.file;
                        connServer = createDB.server;
                        connDatabase = createDB.database;
                    }
                }
                MessageBox.Show(connFile + " " + connServer + " " + connDatabase);
                inq = new Inquiries(connFile, connServer);
                inq.ConnectDB(connDatabase);

                if (inq.Check_connectionDB() == true)
                {
                    createToolStripMenuItem.Enabled = false;
                    disconnectToolStripMenuItem.Enabled = true;
                    connectionToolStripMenuItem.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in createToolStripMenuItem: " + ex.Message);
            }
        }
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                inq.DisconnectDB();
                if (inq.Check_connectionDB() == false)
                {
                    createToolStripMenuItem.Enabled = true;
                    disconnectToolStripMenuItem.Enabled = false;
                    connectionToolStripMenuItem.Enabled = true;
                    addObject.Visible = false;
                    addAttribute.Visible = false;
                    addConnection.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in disconnectToolStripMenuItem: " + ex.Message);
            }
        }
        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (Connect_DB connDB = new Connect_DB())
                {
                    DialogResult dr = connDB.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        connServer = connDB.server;
                        connDatabase = connDB.database;
                    }
                }

                inq = new Inquiries(connFile, connServer);
                inq.ConnectDB(connDatabase);

                if (inq.Check_connectionDB() == true)
                {
                    createToolStripMenuItem.Enabled = false;
                    disconnectToolStripMenuItem.Enabled = true;
                    connectionToolStripMenuItem.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in connectionToolStripMenuItem: " + ex.Message);
            }
        }
        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addObject.Visible = true;
                addAttribute.Visible = false;
                addConnection.Visible = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in objectToolStripMenuItem: " + ex.Message);
            }
        }
        private void attributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addObject.Visible = false;
                addAttribute.Visible = true;
                addConnection.Visible = false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in attributeToolStripMenuItem: " + ex.Message);
            }
        }
        private void connToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addObject.Visible = false;
                addAttribute.Visible = false;
                addConnection.Visible = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in connToolStripMenuItem: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inq.AddObjectDB(typeBox.Text,productBox.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inq.AddAttributeDB(idBox.Text, nameBox.Text, valueBox.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inq.AddConnectionDB(idparentBox.Text, idchildBox.Text, linknameBox.Text);
        }
    }
}
