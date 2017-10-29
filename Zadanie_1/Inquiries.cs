using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Zadanie_1
{
    class Inquiries
    {
        private SqlConnection myConn;
        private string server;
        private string file;

        public Inquiries() { }
        public Inquiries(string file,string server)
        {
            this.server = server;
            this.file = file;
        }
        
        public bool Check_connectionDB()
        {
            try
            {
                if (myConn.State == ConnectionState.Open)
                {
                    return true;
                }
                else return false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }
        public void DisconnectDB()
        {
            try
            {
                myConn.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in DisconnectDB: " + ex.Message);
            }
        }
        public void CreateDB(string nameDB)
        {
            try
            {
                String str;
                SqlConnection mynewConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=master");

                str = "CREATE DATABASE " + nameDB + " ON PRIMARY " +
                    "(NAME = " + nameDB + "_Data, " +
                    "FILENAME = '" + file + nameDB + ".mfd', " +
                    "SIZE = 5MB, MAXSIZE = 20MB, FILEGROWTH = 10%) " +
                    "LOG ON (NAME = " + nameDB + "_Log, " +
                    "FILENAME = '" + file + nameDB + ".ldf', " +
                    "SIZE = 5MB, " +
                    "MAXSIZE = 20MB, " +
                    "FILEGROWTH = 10%)";
                SqlCommand myCommand = new SqlCommand(str, mynewConn);

                mynewConn.Open();
                myCommand.ExecuteNonQuery();
                mynewConn.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in CreateDB: " + ex.Message);
            }
        }
        public void CreateTable(string nameDB)
        {
            try
            {
                SqlConnection mycreatetableConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=" + nameDB);
                String str = "CREATE TABLE object (" +
                        "	id INT PRIMARY KEY IDENTITY(1,1)," +
                        "	[type] TEXT," +
                        "	product TEXT )" +

                      "CREATE TABLE attribute (" +
                        "   id_attribute INT PRIMARY KEY IDENTITY(1,1)," +
                        "	id INT NOT NULL FOREIGN KEY REFERENCES object(id)," +
                        "	name TEXT," +
                        "	value TEXT)" +

                      "CREATE TABLE connection (" +
                        "   id_connection INT PRIMARY KEY IDENTITY(1,1)," +
                        "	idparent INT NOT NULL FOREIGN KEY REFERENCES object(id)," +
                        "	idchild INT NOT NULL FOREIGN KEY REFERENCES object(id)," +
                        "	linkname TEXT)";

                SqlCommand myCommand = new SqlCommand(str, mycreatetableConn);

                mycreatetableConn.Open();
                myCommand.ExecuteNonQuery();
                mycreatetableConn.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in CreateTable: " + ex.Message);
            }
        }
        public void ConnectDB(string nameDB)
        {
            try
            {
                myConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=" + nameDB);
                myConn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in ConnectDB: " + ex.Message);
            }
        }
        public void AddObjectDB(string value_1,string value_2)
        {
            try
            {
                String str = "INSERT INTO object ([type],product) values ('" + value_1 + "','" + value_2 + "')";
                SqlCommand myCommand = new SqlCommand(str, myConn);
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddObjectDB: " + ex.Message);
            }
        }
        public void AddAttributeDB(string value_1, string value_2, string value_3)
        {
            try
            {
                String str = "INSERT INTO attribute (id,name,value) values ('" + value_1 + "','" + value_2 + "','" + value_3 + "')";
                SqlCommand myCommand = new SqlCommand(str, myConn);
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddAttributeDB: " + ex.Message);
            }
        }
        public void AddConnectionDB(string value_1, string value_2, string value_3)
        {
            try
            {
                String str = "INSERT INTO connection (idparent,idchild,linkname) values ('" + value_1 + "','" + value_2 + "','" + value_3 + "')";
                SqlCommand myCommand = new SqlCommand(str, myConn);
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddConnectionDB: " + ex.Message);
            }
        }

        //  Проверка подключение к базы //
        /*try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
         }*/
    }
}
