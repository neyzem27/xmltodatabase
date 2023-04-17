//This application stores XML data into a SQL Server Database and visa versa

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace XMLtoSQLserver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
