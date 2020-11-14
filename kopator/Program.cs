using System;
using System.Windows.Forms;

namespace kopator
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
            var oForm = new kopatorMainForm();

            Application.Run(oForm);
            oForm?.Dispose();
        }
    }
}
