using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace int_to_bmp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(MessageBox.Show("Press ALT + F4 to Exit","Form1",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
                Application.Run(new Form1());
            
        }
    }
}
