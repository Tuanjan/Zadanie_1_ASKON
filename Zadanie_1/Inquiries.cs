using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
                SqlCommand comandCreatDB = new SqlCommand(str, mynewConn);

                mynewConn.Open();
                comandCreatDB.ExecuteNonQuery();
                mynewConn.Close();

                myConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=" + nameDB);
                myConn.Open();

                GoComandInFile("CreateDatabase.txt");
                GoComandInFile("CreateProceduteInsertIntoObject.txt");
                GoComandInFile("CreateProceduteInsertIntoAttribute.txt");
                GoComandInFile("CreateProceduteInsertIntoConnection.txt");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in CreateDB: " + ex.Message);
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

        private void GoComandInFile(string thePathToFile)
        {
            try
            {
                StreamReader reader = new StreamReader(thePathToFile);
                string str = reader.ReadToEnd();
                reader.Close();
                SqlCommand comand = new SqlCommand(str, myConn);
                comand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in GoCommandInFile: " + ex.Message);
            }
        }
        private void GoComandInString(String str)
        {
            try
            {
                SqlCommand comand = new SqlCommand(str, myConn);
                comand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in GoComandInString: " + ex.Message);
            }
        }

        public DataTable ComboBoxItem(string column_name, string table_name)
        {
            SqlCommand sc = new SqlCommand("SELECT DISTINCT " + column_name + " FROM " + table_name, myConn);
            SqlDataReader reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add(column_name, typeof(string));
            dt.Load(reader);
            return dt;
        }
        public DataTable ComboBoxItem(string column_name1, string column_name2, string table_name,string comboBoxSelected)
        {
            SqlCommand sc = new SqlCommand("SELECT " + column_name2 + " FROM " + table_name + " WHERE " + column_name1 + " = '" + comboBoxSelected + "'", myConn);
            SqlDataReader reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add(column_name2, typeof(string));
            dt.Load(reader);
            return dt;
        }

        public void AddObjectDB(string value_1,string value_2)
        {
            try
            {
                String str = "EXEC [InsertIntoObject] '" + value_1 + "','" + value_2 + "'";
                GoComandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddObjectDB: " + ex.Message);
            }
        }
        public void AddAttributeDB(string comboBoxSelectedType, string comboBoxSelectedProduct, string value_1, string value_2)
        {
            try
            {
                String str = "EXEC [InsertIntoAttribute] '" + comboBoxSelectedType + "','" + comboBoxSelectedProduct +
                                "','" + value_1 + "','" + value_2+"'";
                GoComandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddAttributeDB: " + ex.Message);
            }
        }
        public void AddConnectionDB(string type1, string product1, string type2, string product2, string linkname)
        {
            try
            {
                String str = "EXEC [InsertIntoConnection] '" + type1 + "','" + product1 + "','" + type2 + "','" + product2 + "','" + linkname + "'";
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
