using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
                
                inq = new Inquiries(connFile, connServer);
                inq.ConnectDB(connDatabase);
                MessageBox.Show("Подключено:\nСервер: " + connServer + "\nБаза: " + connDatabase);
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
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        private void button1_Click(object sender, EventArgs e)
        {
            inq.AddObjectDB(typeBox.Text,productBox.Text);
            MessageBox.Show("Добавлено!");
        }

        private void attributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addObject.Visible = false;
                addAttribute.Visible = true;
                addConnection.Visible = false;

                comboBox1.DataSource = inq.ComboBoxItem("type", "object");
                comboBox1.ValueMember = "type";
                comboBox1.DisplayMember = "type";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in attributeToolStripMenuItem: " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            inq.AddAttributeDB(comboBox1.Text, comboBox2.Text, nameBox.Text, valueBox.Text);
            MessageBox.Show("Добавлено!");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.DataSource = inq.ComboBoxItem("type", "product", "object", comboBox1.Text);
                comboBox2.ValueMember = "product";
                comboBox2.DisplayMember = "product";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in comboBox1_SelectedIndexChanged: " + ex.Message);
            }
        }
        
        private void connToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                addObject.Visible = false;
                addAttribute.Visible = false;
                addConnection.Visible = true;

                comboBox3.DataSource = inq.ComboBoxItem("type", "object");
                comboBox3.ValueMember = "type";
                comboBox3.DisplayMember = "type";

                comboBox5.DataSource = inq.ComboBoxItem("type", "object");
                comboBox5.ValueMember = "type";
                comboBox5.DisplayMember = "type";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in connToolStripMenuItem: " + ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            inq.AddConnectionDB(comboBox3.Text, comboBox4.Text, comboBox5.Text, comboBox6.Text, linknameBox.Text);
            MessageBox.Show("Добавлено!");
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox4.DataSource = inq.ComboBoxItem("type", "product", "object", comboBox3.Text);
                comboBox4.ValueMember = "product";
                comboBox4.DisplayMember = "product";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in comboBox1_SelectedIndexChanged: " + ex.Message);
            }
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox6.DataSource = inq.ComboBoxItem("type", "product", "object", comboBox5.Text);
                comboBox6.ValueMember = "product";
                comboBox6.DisplayMember = "product";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in comboBox1_SelectedIndexChanged: " + ex.Message);
            }
        }
    }
}
