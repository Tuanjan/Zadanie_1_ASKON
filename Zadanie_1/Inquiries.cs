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
        public void CreateDB(string nameDB)
        {
            String str;
            SqlConnection mymewConn = new SqlConnection("Server=DESKTOP-906H1M0\\SQLEXPRESS;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE " + nameDB + " ON PRIMARY " +
                "(NAME = MyDatabase_Data, " +
                "FILENAME = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.SQLEXPRESS\\MSSQL\\DATA\\" + nameDB + ".mfd', " +
                "SIZE = 5MB, MAXSIZE = 20MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = MyDatabase_Log, " +
                "FILENAME = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.SQLEXPRESS\\MSSQL\\DATA\\" + nameDB + ".ldf', " +
                "SIZE = 5MB, " +
                "MAXSIZE = 20MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, mymewConn);

            try
            {
                mymewConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (mymewConn.State == ConnectionState.Open)
                {
                    mymewConn.Close();
                }
            }

        }
        public void CreateTable(string nameDB)
        {
            String str = "Server=DESKTOP-906H1M0\\SQLEXPRESS;Integrated security=SSPI;database=" + nameDB;
            myConn = new SqlConnection(str);

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

            try
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
            }

        }

        public void ConnectDB(string nameDB)
        {
            String str = "Server=DESKTOP-906H1M0\\SQLEXPRESS;Integrated security=SSPI;database=" + nameDB;
            myConn = new SqlConnection(str);
        }

        public void AddDB(string nameDB)
        {

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
