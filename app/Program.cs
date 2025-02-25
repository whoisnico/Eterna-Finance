using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartUp();
        }
        static void StartUp()
        {
            // Use a relative path or combine with the application's startup path.
            string cacheDirectory = Path.Combine(Application.StartupPath, "cache");
            string cachePath = Path.Combine(cacheDirectory, "userdata.json");

            // Ensure the directory exists
            if (!Directory.Exists(cacheDirectory))
            {
                Directory.CreateDirectory(cacheDirectory);
            }

            if (!File.Exists(cachePath))
            {
                // File.Create(cachePath).Close();
                LoginR();
            }
            else
            {
                Dashboard();
            }
        }

        static void LoginR(){
            Login loginForm = new Login();
            loginForm.SetUserText("Enter your username...");
            loginForm.SetPasswordText("Enter your password...");
            loginForm.LoginButtonClicked += (sender, e) =>
            {
                Dashboard();
            };

            Application.Run(loginForm);
        }
    static void Dashboard()
        {

        }
    }
}
