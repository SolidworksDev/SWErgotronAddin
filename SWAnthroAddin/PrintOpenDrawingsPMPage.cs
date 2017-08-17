using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;

namespace SWErgotronAddin
{
    public class PrintOpenDrawingsPMPage
    {
        //Local Objects
        IPropertyManagerPage2 swPropertyPage = null;
        PrintOpenDrawingsPMHandler handler = null;
        ISldWorks SwApp = null;
        SwAddin userAddin = null;
        public AssemblyDoc currentAssembly;
        public ModelDoc2 currentModel;
        public List<string> DrawingsList = new List<string>();

        //Groups
        public IPropertyManagerPageGroup mainGroup;

        //Controls
        public IPropertyManagerPageListbox partsList;
        public IPropertyManagerPageCheckbox cbSelectAll;
        
        #region Property Manager Page Controls

        //Control IDs
        public const int mainGroupID = 0;
        public const int cbSelectAllID = 1;
        public const int partsListID = 2;

        #endregion

        public PrintOpenDrawingsPMPage(SwAddin addin)
        {
            userAddin = addin;
            if (userAddin != null)
            {
                SwApp = (ISldWorks)userAddin.SwApp;
                CreatePropertyManagerPage();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("SwAddin not set.");
            }
        }

        protected void CreatePropertyManagerPage()
        {
            int errors = -1;
            int options = (int)swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton |
                (int)swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton;

            handler = new PrintOpenDrawingsPMHandler(userAddin, this);
            swPropertyPage = (IPropertyManagerPage2)SwApp.CreatePropertyManagerPage("Print Drawings", options, handler, ref errors);
            if (swPropertyPage != null && errors == (int)swPropertyManagerPageStatus_e.swPropertyManagerPage_Okay)
            {
                try
                {
                    AddControls();
                }
                catch (Exception e)
                {
                    SwApp.SendMsgToUser2(e.Message, 0, 0);
                }
            }
        }

        //Controls are displayed on the page top to bottom in the order 
        //in which they are added to the object.
        protected void AddControls()
        {
            short controlType = -1;
            short align = -1;
            int options = -1;

            //Add the groups
            options = (int)swAddGroupBoxOptions_e.swGroupBoxOptions_Expanded |
                      (int)swAddGroupBoxOptions_e.swGroupBoxOptions_Visible;

            mainGroup = (IPropertyManagerPageGroup)swPropertyPage.AddGroupBox(mainGroupID, "Select Drawings to Print", options);

            options = (int)swAddGroupBoxOptions_e.swGroupBoxOptions_Checkbox |
                      (int)swAddGroupBoxOptions_e.swGroupBoxOptions_Visible;

            //textbox1
            controlType = (int)swPropertyManagerPageControlType_e.swControlType_Textbox;
            align = (int)swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge;
            options = (int)swAddControlOptions_e.swControlOptions_Enabled |
                      (int)swAddControlOptions_e.swControlOptions_Visible;

            //cbSelectAll
            controlType = (int)swPropertyManagerPageControlType_e.swControlType_Checkbox;
            align = (int)swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge;
            options = (int)swAddControlOptions_e.swControlOptions_Enabled |
                      (int)swAddControlOptions_e.swControlOptions_Visible;

            cbSelectAll = (IPropertyManagerPageCheckbox)mainGroup.AddControl(cbSelectAllID, controlType, "Select All Parts", align, options, "Select all part file to export flats");            

            //partsList
            controlType = (int)swPropertyManagerPageControlType_e.swControlType_Listbox;
            align = (int)swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge;
            options = (int)swAddControlOptions_e.swControlOptions_Enabled |
                      (int)swAddControlOptions_e.swControlOptions_Visible;

            partsList = (IPropertyManagerPageListbox)mainGroup.AddControl(partsListID, controlType, "Print or Open Drawings", align, options, "Select the drawing you want to print");
        }

        public void Show()
        {
            if (swPropertyPage != null)
            {

                object[] components = (object[])SwApp.GetDocuments();
               
                foreach (object comp in components)
                {
                    ModelDoc2 modeldoc2 = (ModelDoc2)comp;

                    if (!object.ReferenceEquals(modeldoc2, null))
                    {
                        if (modeldoc2.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                        {
                            if (!DrawingsList.Contains(Path.GetFileName(Path.ChangeExtension(modeldoc2.GetPathName(), "slddrw"))))
                            {
                                DrawingsList.Add(Path.GetFileName(modeldoc2.GetPathName()));
                                //Parts.Add(new Part(Path.ChangeExtension(Path.GetFileName(modeldoc2.GetPathName()), "slddrw"), Path.ChangeExtension(modeldoc2.GetPathName(), "slddrw")));
                            }
                        }
                    }
                }

                if (partsList != null)
                {
                    string[] items = { };
                    items = DrawingsList.ToArray();
                    partsList.Style = (int)swPropMgrPageListBoxStyle_e.swPropMgrPageListBoxStyle_Sorted + (int)swPropMgrPageListBoxStyle_e.swPropMgrPageListBoxStyle_MultipleItemSelect;
                    partsList.Height = 100;
                    partsList.AddItems(items);
                }
                swPropertyPage.Show();
            }
        }
    }
}
