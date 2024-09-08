using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ClientSideProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addClient_Click(object sender, EventArgs e)
        {
           StartNewClient();
        }
        private void StartNewClient()
        {
            string relativePath = @"..\..\ClientProgram\bin\Debug\ClientProgram.exe";
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = absolutePath;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;  
            Process.Start(psi);
        }

    }
}
