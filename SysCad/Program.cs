using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysCad
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

            using (FormLogin Flogin = new FormLogin())
            {
                if(Flogin.ShowDialog()==DialogResult.OK)
                {
                    Flogin.Close();
                    Application.Run(new FormCadSys());
                }
            }
        }
    }
}
