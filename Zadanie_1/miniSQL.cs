﻿using System;
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
            inq.DisconnectDB();
            this.Close();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
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

            if(inq.Check_connectionDB() == false)
            {
                createToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
                connectionToolStripMenuItem.Enabled = false;
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inq.DisconnectDB();
            if (inq.Check_connectionDB() == false)
            {
                connectionToolStripMenuItem.Enabled = true;
                createToolStripMenuItem.Enabled = true;
                disconnectToolStripMenuItem.Enabled = false;
            }
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
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

            if (inq.Check_connectionDB() == false)
            {
                createToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
                connectionToolStripMenuItem.Enabled = false;
            }
        }
    }
}
