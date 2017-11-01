using System;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace Zadanie_1
{
    public class XmlHandler
    {
        XmlDocument xmlDocument;

        public void TreeViewToXml(TreeView treeView, String path)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateElement("ROOT"));
            XmlRekursivExport(xmlDocument.DocumentElement, treeView.Nodes);
            xmlDocument.Save(path);
        }
        public void XmlToTreeView(String path, TreeView treeView)
        {
            xmlDocument = new XmlDocument();

            xmlDocument.Load(path);
            treeView.Nodes.Clear();
            XmlRekursivImport(treeView.Nodes, xmlDocument.DocumentElement.ChildNodes);
        }

        private XmlNode XmlRekursivExport(XmlNode nodeElement, TreeNodeCollection treeNodeCollection)
        {
            XmlNode xmlNode = null;
            foreach (TreeNode treeNode in treeNodeCollection)
            {
                xmlNode = xmlDocument.CreateElement("TreeViewNode");

                xmlNode.Attributes.Append(xmlDocument.CreateAttribute("value"));
                xmlNode.Attributes["value"].Value = treeNode.Text;


                if (nodeElement != null)
                    nodeElement.AppendChild(xmlNode);

                if (treeNode.Nodes.Count > 0)
                {
                    XmlRekursivExport(xmlNode, treeNode.Nodes);
                }
            }
            return xmlNode;
        }

        private void XmlRekursivImport(TreeNodeCollection elem, XmlNodeList xmlNodeList)
        {
            TreeNode treeNode;
            foreach (XmlNode myXmlNode in xmlNodeList)
            {
                treeNode = new TreeNode(myXmlNode.Attributes["value"].Value);

                if (myXmlNode.ChildNodes.Count > 0)
                {
                    XmlRekursivImport(treeNode.Nodes, myXmlNode.ChildNodes);
                }
                elem.Add(treeNode);
            }
        }

        public void Converts(string file, string path)
        {
            DataSet ds = new DataSet();

            //Convert the XML into Dataset
            ds.ReadXml(file);

            System.Data.DataTable dt = new System.Data.DataTable();

            //Retrieve the table fron Dataset
            dt = ds.Tables[0];

            // Create an Excel object
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            //Create workbook object
            string str = path;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(Filename: str);
            //excel.
            //Create worksheet object

            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
            //worksheet= workbook.Worksheets.get_Item(3);

            // Column Headings
            int iColumn = 0;

            foreach (DataColumn c in dt.Columns)
            {
                iColumn++;
                excel.Cells[1, iColumn] = c.ColumnName;
            }

            // Row Data
            int iRow = worksheet.UsedRange.Rows.Count - 1;

            foreach (DataRow dr in dt.Rows)
            {
                iRow++;

                // Row's Cell Data
                iColumn = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iColumn++;
                    excel.Cells[iRow + 1, iColumn] = dr[c.ColumnName];
                }
            }

            ((Microsoft.Office.Interop.Excel._Worksheet)worksheet).Activate();

            //Save the workbook
            workbook.Save();

            //Close the Workbook
            workbook.Close();
            Console.Out.WriteLine("close");
            // Finally Quit the Application
            ((Microsoft.Office.Interop.Excel._Application)excel).Quit();
        }



        public void mamaain(string file, string path)
        {
            DataSet ds = new DataSet();

            //Convert the XML into Dataset
            ds.ReadXml(file);

            //Retrieve the table fron Dataset
            //DataTable dt = ds.Tables[0];

            // Create an Excel object
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            //Create workbook object
            string str = path;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(Filename: str);

            foreach (System.Data.DataTable tab in ds.Tables)
            {
                FromDataTableToExcel(tab, excel, workbook);
            }

            //Save the workbook
            workbook.Save();

            //Close the Workbook
            workbook.Close();

            // Finally Quit the Application
            ((Microsoft.Office.Interop.Excel._Application)excel).Quit();

        }

        static void FromDataTableToExcel(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Application excel, Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            //Create worksheet object
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

            // Column Headings
            int iColumn = worksheet.UsedRange.Columns.Count - 1;
            int iColumn1 = iColumn;
            int iColumn2 = iColumn;

            foreach (DataColumn c in dt.Columns)
            {
                iColumn++;
                excel.Cells[1, iColumn] = c.ColumnName;
            }

            // Row Data
            int iRow = 0;

            foreach (DataRow dr in dt.Rows)
            {
                iRow++;

                // Row's Cell Data                
                foreach (DataColumn c in dt.Columns)
                {
                    iColumn1++;
                    excel.Cells[iRow + 1, iColumn1] = dr[c.ColumnName];
                }

                iColumn1 = iColumn2;
            }

            ((Microsoft.Office.Interop.Excel._Worksheet)worksheet).Activate();

        }

    }
}
