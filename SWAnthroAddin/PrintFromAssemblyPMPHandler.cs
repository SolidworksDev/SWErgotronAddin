using System;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using System.IO;
using EdmLib;


namespace SWErgotronAddin
{
    public class PrintFromAssemblyPMPHandler : IPropertyManagerPage2Handler9
    {
        ISldWorks iSwApp;
        SwAddin userAddin;
        PrintFromAssemblyPMPage printFromAssemblyPage;
        bool allSelected = false;
        PageSetup pageSetup;
        int[] sheets = new int[1];
        int err = 0;
        int modelOOD = 0;

        public PrintFromAssemblyPMPHandler(SwAddin addin, PrintFromAssemblyPMPage page)
        {
            userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
            printFromAssemblyPage = page;
        }

        //Implement these methods from the interface
        public void AfterClose()
        {
            //This function must contain code, even if it does nothing, to prevent the
            //.NET runtime environment from doing garbage collection at the wrong time.
            //int IndentSize;
            //IndentSize = System.Diagnostics.Debug.IndentSize;
            //System.Diagnostics.Debug.WriteLine(IndentSize);
            printFromAssemblyPage.partsList.Clear();
            printFromAssemblyPage.PartsList.Clear();
            printFromAssemblyPage.Parts.Clear();
            allSelected = false;
        }

        public void OnCheckboxCheck(int id, bool status)
        {
            if (allSelected == false)
            {

                for (short i = 0; i <= printFromAssemblyPage.partsList.ItemCount; i++)
                {
                    printFromAssemblyPage.partsList.SetSelectedItem(i, true);
                }
                allSelected = true;
            }
            else
            {
                for (short i = 0; i <= printFromAssemblyPage.partsList.ItemCount; i++)
                {
                    printFromAssemblyPage.partsList.SetSelectedItem(i, false);
                }
                allSelected = false;
            }
        }

        public void OnClose(int reason)
        {

            string vaultName = "Production Vault";
            string CompareFloder = "C:\\Production Vault";

            EdmVault5 v = new EdmVault5();

            v.LoginAuto(vaultName, 0);

            if (v.IsLoggedIn == false)
            {
                iSwApp.SendMsgToUser("Failed to log in to vault " + vaultName);
                return;
            }

            if (reason == (int)swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
            {
                short[] selectedDrawings = printFromAssemblyPage.partsList.GetSelectedItems();

                for (short i = 0; i <= selectedDrawings.Length - 1; i++)
                {
                    if (printFromAssemblyPage.openOption.Checked)
                    {
                        foreach (Part p in printFromAssemblyPage.Parts)
                        {
                            if (printFromAssemblyPage.partsList.get_ItemText(selectedDrawings[i]) == p.PartName)
                            {
                                string drawingName = p.PartFullPath;

                                if (File.Exists(drawingName))
                                {
                                    iSwApp.OpenDoc6(drawingName, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_ReadOnly,
                                        "", ref err, ref modelOOD);                                   
                                }
                                else
                                {
                                    drawingName = p.PartFullPath.Replace("-00.", ".");
                                    iSwApp.OpenDoc6(drawingName, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_ReadOnly,
                                        "", ref err, ref modelOOD);
                                }

                                if (err != 0)
                                {
                                    iSwApp.SendMsgToUser("Failed to open drawing for: " + drawingName);
                                }

                                if (modelOOD != 0)
                                {
                                    iSwApp.SendMsgToUser("Drawing: " + drawingName + " is out of date");
                                }
                            
                            }
                        }
                        
                    }
                    else
                    {
                        ModelDoc2 currentDrawing;
                        ModelDocExtension modelDocExt;
                        string drawingName = printFromAssemblyPage.partsList.get_ItemText(selectedDrawings[i]).Replace("-00.", ".");

                        currentDrawing = iSwApp.OpenDoc6(drawingName, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                                        "", ref err, ref modelOOD);

                        if (modelOOD != 0)
                        {
                            iSwApp.SendMsgToUser("Model: " + drawingName + "is out of date");
                        }

                        if (err != 0)
                        {
                            iSwApp.SendMsgToUser("Failed to open part: " + drawingName);
                        }
                        else
                        {
                            pageSetup = currentDrawing.PageSetup;
                            pageSetup.Orientation = 2;
                            modelDocExt = currentDrawing.Extension;
                            modelDocExt.PrintOut3(sheets, 1, true, "\\\\anthro3\\p56b", "", true);
                            iSwApp.CloseDoc(drawingName);
                        }
                    }
                }

                printFromAssemblyPage.partsList.Clear();
                printFromAssemblyPage.PartsList.Clear();
                printFromAssemblyPage.Parts.Clear();
            }

            if (reason == (int)swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Cancel)
            {
                printFromAssemblyPage.partsList.Clear();
                printFromAssemblyPage.PartsList.Clear();
                printFromAssemblyPage.Parts.Clear();
            }
        }

        public void OnComboboxEditChanged(int id, string text)
        {

        }

        public int OnActiveXControlCreated(int id, bool status)
        {
            return -1;
        }

        public void OnButtonPress(int id)
        {

        }

        public void OnComboboxSelectionChanged(int id, int item)
        {

        }

        public void OnGroupCheck(int id, bool status)
        {

        }

        public void OnGroupExpand(int id, bool status)
        {

        }

        public bool OnHelp()
        {
            return true;
        }

        public void OnListboxSelectionChanged(int id, int item)
        {

        }

        public bool OnNextPage()
        {
            return true;
        }

        public void OnNumberboxChanged(int id, double val)
        {

        }

        public void OnNumberBoxTrackingCompleted(int id, double val)
        {

        }

        public void OnOptionCheck(int id)
        {

        }

        public bool OnPreviousPage()
        {
            return true;
        }

        public void OnSelectionboxCalloutCreated(int id)
        {

        }

        public void OnSelectionboxCalloutDestroyed(int id)
        {

        }

        public void OnSelectionboxFocusChanged(int id)
        {

        }

        public void OnSelectionboxListChanged(int id, int item)
        {

        }

        public void OnTextboxChanged(int id, string text)
        {

        }

        public void AfterActivation()
        {

        }

        public bool OnKeystroke(int Wparam, int Message, int Lparam, int Id)
        {
            return true;
        }

        public void OnPopupMenuItem(int Id)
        {

        }

        public void OnPopupMenuItemUpdate(int Id, ref int retval)
        {

        }

        public bool OnPreview()
        {
            return true;
        }

        public void OnSliderPositionChanged(int Id, double Value)
        {

        }

        public void OnSliderTrackingCompleted(int Id, double Value)
        {

        }

        public bool OnSubmitSelection(int Id, object Selection, int SelType, ref string ItemText)
        {
            return true;
        }

        public bool OnTabClicked(int Id)
        {
            return true;
        }

        public void OnUndo()
        {

        }

        public void OnWhatsNew()
        {

        }


        public void OnGainedFocus(int Id)
        {

        }

        public void OnListboxRMBUp(int Id, int PosX, int PosY)
        {

        }

        public void OnLostFocus(int Id)
        {

        }

        public void OnRedo()
        {

        }

        public int OnWindowFromHandleControlCreated(int Id, bool Status)
        {
            return 0;
        }
    }
}
