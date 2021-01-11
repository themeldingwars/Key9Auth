using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShiteLauncher
{
    public partial class Form1 : Form
    {
        private Key9Interface Key9 = new Key9Interface();

        public Form1()
        {
            InitializeComponent();

            Key9.Init();

            Oracle.OracleHostURL = GetOperatorHost();
        }

        private void BTT_Launch_Click(object sender, EventArgs e)
        {
            var email       = TB_Email.Text;
            var password    = TB_Password.Text;
            var loginResult = Oracle.Login(email, password);

            if (loginResult.Error == null)
            {
                // Set the data in the memory mapped file for the client
                Key9.SetData(new Key9Interface.MemMapedData()
                {
                    Token    = loginResult.LoginData.session_id,
                    Username = email
                });

                LaunchFirefall();
            }
            else
            {
                MessageBox.Show(loginResult.Error.message, loginResult.Error.code);
            }
        }

        public void LaunchFirefall()
        {
            var baseDir = Path.GetDirectoryName(Application.ExecutablePath);
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName         = Path.Combine(baseDir, "64/RedHanded.exe"),
                Arguments        = "--debug -- FirefallClient.exe --debug --369 --369debug",
                WorkingDirectory = baseDir
            };

            process.Start();
        }

        public string GetOperatorHost()
        {
            var iniPath     = Path.Combine(new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath)).Parent.Parent.FullName, "firefall.ini");
            var ini         = new FileIniDataParser().ReadFile(iniPath);
            var operatorUrl = ini["Config"]["OperatorHost"].Trim('"');
            return operatorUrl;
        }
    }
}
