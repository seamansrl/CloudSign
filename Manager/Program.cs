using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horus
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static String LogInToken = "";
        public static String Server = "https://server1.proyectohorus.com.ar"; //"server1.proyectohorus.com.ar";

        public static Boolean ShowLogo = false;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Landing());
        }
    }
}
