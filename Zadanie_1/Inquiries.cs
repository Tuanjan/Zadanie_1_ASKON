using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
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

                GoCommandInFile("CreateDatabase.txt");
                GoCommandInFile("CreateProcedureInsertIntoObject.txt");
                GoCommandInFile("CreateProcedureInsertIntoAttribute.txt");
                GoCommandInFile("CreateProcedureInsertIntoConnection.txt");
                GoCommandInFile("CreateTrigger.txt");
                GoCommandInFile("CreateProcedureDeleteFromObject.txt");
                GoCommandInFile("CreateProcedureDeleteFromAttribute.txt");
                GoCommandInFile("CreateProcedureDeleteFromConnection.txt");
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

        private void GoCommandInFile(string thePathToFile)
        {
            try
            {
                StreamReader reader = new StreamReader(thePathToFile);
                string str = reader.ReadToEnd();
                reader.Close();
                SqlCommand command = new SqlCommand(str, myConn);
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in GoCommandInFile: " + ex.Message);
            }
        }
        private void GoCommandInString(string str)
        {
            try
            {
                SqlCommand command = new SqlCommand(str, myConn);
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in GoCommandInString: " + ex.Message);
            }
        }
        public List<string> GoCommandInString(string strcommand,string column)
        {
                SqlCommand sqlcmd = new SqlCommand(strcommand, myConn);
                SqlDataReader sqldr = sqlcmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add(column, typeof(string));
                dt.Load(sqldr);

                List<string> str = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn Col in dt.Columns)
                    {
                        str.Add(row[Col].ToString());
                    }
                }

                return str;
        }

        public List<string> TableItem(string column, string table)
        {
            SqlCommand sqlcmd = new SqlCommand("SELECT " + column + " FROM " + table, myConn);
            SqlDataReader sqldr = sqlcmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add(column, typeof(string));
            dt.Load(sqldr);

            List<string> str = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    str.Add(row[Col].ToString());
                }
            }

            return str;
        }

        public DataTable ComboBoxItem(string column, string table)
        {
            SqlCommand sc = new SqlCommand("SELECT DISTINCT " + column + " FROM " + table, myConn);
            SqlDataReader reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add(column, typeof(string));
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
                GoCommandInString(str);
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
                GoCommandInString(str);
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
                GoCommandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in AddConnectionDB: " + ex.Message);
            }
        }
        
        public void DeleteObjectDB(string value_1, string value_2)
        {
            try
            {
                String str = "EXEC [DeleteFromObject] '" + value_1 + "','" + value_2 + "'";
                GoCommandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in DeleteObjectDB: " + ex.Message);
            }
        }
        public void DeleteAttributeDB(string value_1, string value_2)
        {
            try
            {
                String str = "EXEC [DeleteFromAttribute] '" + value_1 + "','" + value_2 + "'";
                GoCommandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in DeleteAttributeDB: " + ex.Message);
            }
        }
        public void DeleteConnectionDB(string value_1)
        {
            try
            {
                String str = "EXEC [DeleteFromConnection] '" + value_1 + "'";
                GoCommandInString(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error in DeleteConnectionDB: " + ex.Message);
            }
        }
    }
}
