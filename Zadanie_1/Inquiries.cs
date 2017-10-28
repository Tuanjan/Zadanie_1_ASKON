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
            catch
            {
                MessageBox.Show("Упс! Ошибка в Check_connectionDB");
                return true;
            }
        }
        public void DisconnectDB()
        {
            try
            {
                myConn.Close();
            }
            catch
            {
                MessageBox.Show("Упс! Ошибка в DisconnectDB");
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
            catch
            {
                MessageBox.Show("Упс! Ошибка в CreateDB");
            }
        }
        public void CreateTable(string nameDB)
        {
            try
            {
                String str;
                myConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=" + nameDB);

                str = "CREATE TABLE objejct (" +
                        "	id INT PRIMARY KEY IDENTITY(1,1)," +
                        "	[type] TEXT," +
                        "	product TEXT )" +

                      "CREATE TABLE attribute (" +
                        "	id INT NOT NULL FOREIGN KEY REFERENCES objejct(id)," +
                        "	name TEXT," +
                        "	value TEXT)" +

                      "CREATE TABLE connection (" +
                        "	idparent INT NOT NULL FOREIGN KEY REFERENCES objejct(id)," +
                        "	idchild INT NOT NULL FOREIGN KEY REFERENCES objejct(id)," +
                        "	linkname TEXT)";

                SqlCommand myCommand = new SqlCommand(str, myConn);

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
            catch
            {
                MessageBox.Show("Упс! Ошибка в CreateTable");
            }
}
        public void ConnectDB(string nameDB)
        {
            try
            {
                myConn = new SqlConnection("Server=" + server + ";Integrated security=SSPI;database=" + nameDB);
            }
            catch
            {
                MessageBox.Show("Упс! Ошибка в CreateTable");
            }
        }


        public void AddDB()
        {
            if (myConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет подключение к базе данных");
            }
            else
            {

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
