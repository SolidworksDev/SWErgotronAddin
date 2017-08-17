using System;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;

namespace SWErgotronAddin
{
    public class ExportDXFPMPHandler : IPropertyManagerPage2Handler9
    {
        ISldWorks iSwApp;
        SwAddin userAddin;
        ExportDXFPMPage exportDXFPage;
        bool allSelected = false;

        public ExportDXFPMPHandler(SwAddin addin, ExportDXFPMPage page)
        {
            userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
            exportDXFPage = page;
        }

        //Implement these methods from the interface
        public void AfterClose()
        {
            //This function must contain code, even if it does nothing, to prevent the
            //.NET runtime environment from doing garbage collection at the wrong time.
            //int IndentSize;
            //IndentSize = System.Diagnostics.Debug.IndentSize;
            //System.Diagnostics.Debug.WriteLine(IndentSize);
            exportDXFPage.partsList.Clear();
            exportDXFPage.PartsList.Clear();
            exportDXFPage.Parts.Clear();
            allSelected = false;
        }

        public void OnCheckboxCheck(int id, bool status)
        {
            if (allSelected == false)
            {

                for (short i = 0; i <= exportDXFPage.partsList.ItemCount; i++)
                {
                    exportDXFPage.partsList.SetSelectedItem(i, true);
                }
                allSelected = true;
            }
            else
            {
                for (short i = 0; i <= exportDXFPage.partsList.ItemCount; i++)
                {
                    exportDXFPage.partsList.SetSelectedItem(i, false);
                }
                allSelected = false;
            }
        }

        public void OnClose(int reason)
        {

            if (reason == (int)swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
            {                
                foreach (Part p in exportDXFPage.Parts)
                {
                    for (short i = 0; i <= exportDXFPage.partsList.GetSelectedItemsCount(); i++)
                    {
                        if (exportDXFPage.partsList.get_ItemText(i) == p.PartName)
                        {
                            string filePath = @"\\svr12T\TRUMPF.NET\DXF\" + p.PartName;

                            int err = 0;
                            int warn = 0;
                            ModelDoc2 swModel = iSwApp.OpenDoc6(p.PartFullPath, (int)swDocumentTypes_e.swDocPART,
                                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, null, ref err, ref warn);

                            if (err != 0)
                                iSwApp.SendMsgToUser("Failed to open part.");
                            else
                            {
                                PartDoc swPart = (PartDoc)swModel;
                                bool retVal = swPart.ExportFlatPatternView(filePath.Replace("SLDPRT", "DXF"),
                                    (int)swExportFlatPatternViewOptions_e.swExportFlatPatternOption_RemoveBends);
                                if (retVal == false)
                                    iSwApp.SendMsgToUser("Failed to save flat pattern.");
                            }
                        }
                    }
                }

                exportDXFPage.partsList.Clear();
                exportDXFPage.PartsList.Clear();
                exportDXFPage.Parts.Clear();
            }

            if (reason == (int)swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Cancel)
            {
                exportDXFPage.partsList.Clear();
                exportDXFPage.PartsList.Clear();
                exportDXFPage.Parts.Clear();
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
