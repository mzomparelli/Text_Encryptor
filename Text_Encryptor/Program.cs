using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Text_Encryptor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmMain f = new frmMain();
            foreach (string s in args)
            {
                f.FileName = s;
                if (f.FileName != "")
                {
                    f.LoadFile();
                }
            }


            Application.Run(f);
           
        }
    }
}
