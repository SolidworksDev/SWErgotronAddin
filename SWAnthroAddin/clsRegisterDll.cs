using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;



namespace SWAnthroAddin
{
    [RunInstaller(true)]
    public partial class clsRegisterDll : System.Configuration.Install.Installer
    {
        public clsRegisterDll()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(System.Collections.IDictionary savedState)
        {
            
            //base.Commit(savedState);
            //System.Diagnostics.Debugger.Launch();

            ////// Get the location of regasm
            //string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            ////// Get the location of our DLL
            //string componentPath = typeof(clsRegisterDll).Assembly.Location;

            ////// Execute regasm
            //Process P = new Process();
            //string result = "";
            //P.StartInfo.FileName = regasmPath;
            //P.StartInfo.UseShellExecute = false;
            //P.StartInfo.RedirectStandardInput = true;
            //P.StartInfo.RedirectStandardOutput = true;
            //P.StartInfo.Arguments = "/codebase \"" + componentPath + "\"";

            //P.Start();

            //StreamReader sOut = P.StandardOutput;

            //result = sOut.ReadToEnd();
            //sOut.Close();

            //MessageBox.Show(result);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(System.Collections.IDictionary savedState)
        {
            System.Diagnostics.Debugger.Launch();
            base.Commit(savedState);

            //// Get the location of regasm
            string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            //// Get the location of our DLL
            string componentPath = typeof(clsRegisterDll).Assembly.Location;

            //// Execute regasm
            Process P = new Process();
            string result = "";
            P.StartInfo.FileName = regasmPath;
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.RedirectStandardOutput = true;
            P.StartInfo.Arguments = "/codebase \"" + componentPath + "\"";

            P.Start();

            StreamReader sOut = P.StandardOutput;

            result = sOut.ReadToEnd();
            sOut.Close();

            //MessageBox.Show(result);

            //RegistrationServices regsrv = new RegistrationServices();
            //System.Reflection.Assembly assemblyType = GetType().Assembly;
            //regsrv.RegisterAssembly(assemblyType, AssemblyRegistrationFlags.SetCodeBase);
            //if (!regsrv.RegisterAssembly(assemblyType, AssemblyRegistrationFlags.SetCodeBase))
            //{
            //    throw new InstallException("Failed to register for COM Interop.");
            //}
            //else
            //{
            //    MessageBox.Show("Registered " + componentPath + Environment.NewLine + "With " + regasmPath);
            //}
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            
            base.Commit(savedState);
            System.Diagnostics.Debugger.Launch();

            //// Get the location of regasm
            string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            //// Get the location of our DLL
            string componentPath = typeof(clsRegisterDll).Assembly.Location;

            //// Execute regasm
            Process P = new Process();
            string result = "";
            P.StartInfo.FileName = regasmPath;
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.RedirectStandardOutput = true;
            P.StartInfo.Arguments = "/u \"" + componentPath + "\"";

            P.Start();

            StreamReader sOut = P.StandardOutput;

            result = sOut.ReadToEnd();
            sOut.Close();

            MessageBox.Show(result);
        }      
    }
}
