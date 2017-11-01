using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace Zadanie_1
{
    public partial class miniSQL : Form
    {
        private string connFile;
        private string connServer;
        private string connDatabase;
        private Inquiries inq;

        public miniSQL()
        {
            InitializeComponent();
        }
        private void visibleGroupBox(string a)
        {
            addObject.Visible = false;
            addAttribute.Visible = false;
            addConnection.Visible = false;
            deleteObject.Visible = false;
            deleteAttribute.Visible = false;
            deleteConn.Visible = false;

            switch (a)
            {
                case "addObject": addObject.Visible = true; break;
                case "addAttribute": addAttribute.Visible = true; break;
                case "addConnection": addConnection.Visible = true; break;
                case "deleteObject": deleteObject.Visible = true; break;
                case "deleteAttribute": deleteAttribute.Visible = true; break;
                case "deleteConn": deleteConn.Visible = true; break;
            }
        }

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
                        int a = 0;
                        for (int i = 0; i < connFile.Length - 1; i++)
                        {
                            if (connFile[i] == connFile[i + 1])
                            {
                                a++;
                            }
                        }

                        if (a == 0)
                        {
                            connFile = "@\"" + connFile + "\"";
                        }
                        a = 0;
                        inq = new Inquiries(connFile, connServer);
                        inq.ConnectDB(connDatabase);

                        if (inq.Check_connectionDB())
                        {
                            MessageBox.Show("Подключено!\nСервер : " + connServer + "\n База : " + connDatabase);
                        }

                        if (inq.Check_connectionDB() == true)
                        {
                            createToolStripMenuItem.Enabled = false;
                            disconnectToolStripMenuItem.Enabled = true;
                            connectionToolStripMenuItem.Enabled = false;
                        }
                    }
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
                        inq = new Inquiries(connFile, connServer);
                        inq.ConnectDB(connDatabase);

                        if (inq.Check_connectionDB() == true)
                        {
                            createToolStripMenuItem.Enabled = false;
                            disconnectToolStripMenuItem.Enabled = true;
                            connectionToolStripMenuItem.Enabled = false;
                        }
                    }
                }
                if (inq.Check_connectionDB())
                {
                    MessageBox.Show("Подключено!\nСервер : " + connServer + "\n База : " + connDatabase);
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
                visibleGroupBox("addObject");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in objectToolStripMenuItem: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            inq.AddObjectDB(typeBox.Text,productBox.Text);
            MessageBox.Show("Добавлено запись!");
        }

        private void attributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                visibleGroupBox("addAttribute");

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
            MessageBox.Show("Добавлено запись!");
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
                visibleGroupBox("addConnection");

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
            MessageBox.Show("Добавлено запись!");
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
                MessageBox.Show("Error in comboBox3_SelectedIndexChanged: " + ex.Message);
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
                MessageBox.Show("Error in comboBox5_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                visibleGroupBox("deleteObject");

                comboBox7.DataSource = inq.ComboBoxItem("type", "object");
                comboBox7.ValueMember = "type";
                comboBox7.DisplayMember = "type";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in objectToolStripMenuItem: " + ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            inq.DeleteObjectDB(comboBox7.Text,comboBox8.Text);
            MessageBox.Show("Удалено запись!");
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox8.DataSource = inq.ComboBoxItem("type", "product", "object", comboBox7.Text);
                comboBox8.ValueMember = "product";
                comboBox8.DisplayMember = "product";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in comboBox1_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void deleteAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                visibleGroupBox("deleteAttribute");

                comboBox9.DataSource = inq.ComboBoxItem("name", "attribute");
                comboBox9.ValueMember = "name";
                comboBox9.DisplayMember = "name";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in objectToolStripMenuItem: " + ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            inq.DeleteAttributeDB(comboBox9.Text, comboBox10.Text);
            MessageBox.Show("Удалено запись!");
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox10.DataSource = inq.ComboBoxItem("name", "value", "attribute", comboBox9.Text);
                comboBox10.ValueMember = "value";
                comboBox10.DisplayMember = "value";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in comboBox9_SelectedIndexChanged: " + ex.Message);
            }
        }

        private void deleteConnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                visibleGroupBox("deleteConn");

                comboBox11.DataSource = inq.ComboBoxItem("linkname", "connection");
                comboBox11.ValueMember = "linkname";
                comboBox11.DisplayMember = "linkname";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in objectToolStripMenuItem: " + ex.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            inq.DeleteConnectionDB(comboBox11.Text);
            MessageBox.Show("Удалено запись!");
        }
        
        private void add_child(TreeNode treenode, string parent, List<string> idparent, List<string> idchild, List<string> idObject, List<string> product)
        {
            int j = 0;
            for (int i = 0; i < idparent.Count; i++)
            {
                if (idparent[i] == parent)
                {
                    treenode.Nodes.Add(product[idObject.IndexOf(idchild[i])]);
                    add_child(treenode.Nodes[j], idchild[i], idparent, idchild,idObject, product);
                    j++;
                }
            }
            j = 0;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            var idparent = new List<string>();
            var idchild = new List<string>();
            var distinct_idparent = new List<string>();

            idparent = inq.TableItem("idparent", "connection");
            idchild = inq.TableItem("idchild", "connection");

            distinct_idparent = inq.GoCommandInString("SELECT DISTINCT idparent " +
                                                               " FROM connection " +
                                                               " WHERE idparent NOT IN ( SELECT idchild FROM connection ) ", "idparent");

            var idObject = new List<string>();
            var product = new List<string>();

            idObject = inq.GoCommandInString("SELECT id FROM object ", "id");
            product = inq.GoCommandInString("SELECT product FROM object ", "product");

            for (int i = 0; i < distinct_idparent.Count; i++)
            {
                foreach (string a in idObject)
                {
                    if (distinct_idparent[i] == a)
                    {
                        treeView1.Nodes.Add(product[idObject.IndexOf(a)]);
                    }
                }
            }

            for (int i = 0; i < distinct_idparent.Count; i++)
            {
                add_child(treeView1.Nodes[i], distinct_idparent[i], idparent, idchild, idObject, product);
            }

            treeView1.EndUpdate();
            treeView1.ExpandAll();
        }
        

        private string OpenFile()
        {
            string file_xml = "xml";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                file_xml = dlg.FileName;
            }
            return file_xml;
        }

        private void converttoxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlHandler xml = new XmlHandler();
            MessageBox.Show("Выберите файл xml.xml");
            string file_xml = OpenFile();
            MessageBox.Show("Выберите или создайте файл .xlsx");
            string file_xlsx = OpenFile();

            xml.TreeViewToXml(treeView1, file_xml);
            xml.XMLtoXLSX(file_xml, file_xlsx);
        }
        private static List<XElement> CreateXmlElement(TreeNodeCollection treeViewNodes)
        {
            var elements = new List<XElement>();
            foreach (TreeNode treeViewNode in treeViewNodes)
            {
                var element = new XElement(treeViewNode.Name);
                if (treeViewNode.GetNodeCount(true) == 1)
                    element.Value = treeViewNode.Nodes[0].Name;
                else
                    element.Add(CreateXmlElement(treeViewNode.Nodes));
                elements.Add(element);
            }
            return elements;
        }

    }
}
