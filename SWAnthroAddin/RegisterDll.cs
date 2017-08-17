using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace SWErgotronAddin
{
    [RunInstaller(true)]
    public partial class RegisterDll : System.Configuration.Install.Installer
    {
        public RegisterDll()
        {
            InitializeComponent();
        }

        //public override void Install(System.Collections.IDictionary stateSaver)
        //{
        //    base.Install(stateSaver);
        //    RegistrationServices regSrv = new RegistrationServices();
        //    regSrv.RegisterAssembly(base.GetType().Assembly,
        //      AssemblyRegistrationFlags.SetCodeBase);
        //}

        //public override void Uninstall(System.Collections.IDictionary savedState)
        //{
        //    base.Uninstall(savedState);
        //    RegistrationServices regSrv = new RegistrationServices();
        //    regSrv.UnregisterAssembly(base.GetType().Assembly);
        //}

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(System.Collections.IDictionary savedState)
        {
            //System.Diagnostics.Debugger.Launch();
            base.Install(savedState);           

            string batPath = Context.Parameters["targetdir"] + @"Reg-DLL.bat";

            // Execute Reg-DLL.bat
            Process P = new Process();           
            P.StartInfo.FileName = batPath;
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.RedirectStandardOutput = true;            

            P.Start();

            //StreamReader sOut = P.StandardOutput;
            //string result = "";
            //result = sOut.ReadToEnd();
            //sOut.Close();

            //MessageBox.Show(result);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(System.Collections.IDictionary savedState)
        {

            //System.Diagnostics.Debugger.Launch();
            base.Uninstall(savedState);

            string batPath = Context.Parameters["targetdir"] + @"Unreg-DLL.bat";

            //// Execute regasm
            Process P = new Process();
            P.StartInfo.FileName = batPath;
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.RedirectStandardOutput = true;
            P.Start();

            //StreamReader sOut = P.StandardOutput;
            //string result = "";
            //result = sOut.ReadToEnd();
            //sOut.Close();

            //MessageBox.Show(result);
        }      
    }
}
