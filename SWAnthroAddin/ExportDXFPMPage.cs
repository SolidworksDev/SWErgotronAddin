using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices; 

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools;
using SolidWorksTools.File;

namespace SWErgotronAddin
{
    public class ExportDXFPMPage
    {
        //Local Objects
        public IPropertyManagerPage2 swPropertyPage = null;
        public ExportDXFPMPHandler handler = null;
        public ISldWorks SwApp = null;
        public SwAddin userAddin = null;
        public AssemblyDoc currentAssembly;
        public ModelDoc2 currentModel;
        public object[] AllComponents;
        public List<string> PartsList = new List<string>();
        public PartList Parts = new PartList();
        
        //Groups
        public IPropertyManagerPageGroup mainGroup;
        
        //Controls
        public IPropertyManagerPageListbox partsList;
        public IPropertyManagerPageCheckbox cbSelectAll;

        //Control ID's
        public const int mainGroupID = 0;
        public const int cbSelectAllID = 1;
        public const int partsListID = 2;        

        public ExportDXFPMPage(SwAddin addin)
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

            handler = new ExportDXFPMPHandler(userAddin, this);
            swPropertyPage = (IPropertyManagerPage2)SwApp.CreatePropertyManagerPage("Export DXF", options, handler, ref errors);
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

            mainGroup = (IPropertyManagerPageGroup)swPropertyPage.AddGroupBox(mainGroupID, "Select Parts to Export", options);

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

            //list1
            controlType = (int)swPropertyManagerPageControlType_e.swControlType_Listbox;
            align = (int)swPropertyManagerPageControlLeftAlign_e.swControlAlign_LeftEdge;
            options = (int)swAddControlOptions_e.swControlOptions_Enabled |
                      (int)swAddControlOptions_e.swControlOptions_Visible;

            partsList = (IPropertyManagerPageListbox)mainGroup.AddControl(partsListID, controlType, "Flat Patterns to Export", align, options, "Select the parts you want to export flat pattern for");
        }

        public void Show()
        {
            if (swPropertyPage != null)
            {
                currentModel = (ModelDoc2)SwApp.ActiveDoc;
                if (currentModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
                {
                    MessageBox.Show("Document is not an assembly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                currentAssembly = (AssemblyDoc)SwApp.ActiveDoc;
                currentAssembly.ResolveAllLightWeightComponents(true);
                object[] components = currentAssembly.GetComponents(false);
                AllComponents = currentModel.GetDependencies2(true, true, true);

                foreach ( Object comp in components)
                {
                    Component2 cmp = (Component2)comp;

                    ModelDoc2 modeldoc2;
                    modeldoc2 = cmp.GetModelDoc2();

                    if (!object.ReferenceEquals(modeldoc2, null))
                    {
                        if (modeldoc2.GetType() == (int)swDocumentTypes_e.swDocPART)
                        {
                            if (Path.GetFileName(modeldoc2.GetPathName()).isDrawing() || (Path.GetFileName(modeldoc2.GetPathName()).isProto()))
                            {
                                if (!PartsList.Contains(Path.GetFileName(modeldoc2.GetPathName())))
                                {
                                    PartsList.Add(Path.GetFileName(modeldoc2.GetPathName()));
                                    Parts.Add(new Part(Path.GetFileName(modeldoc2.GetPathName()), modeldoc2.GetPathName()));
                                }
                            }
                        }
                    }
                }

                if (partsList != null)
                {
                    string[] items = {};
                    items = PartsList.ToArray();
                    partsList.Style = (int)swPropMgrPageListBoxStyle_e.swPropMgrPageListBoxStyle_Sorted + (int)swPropMgrPageListBoxStyle_e.swPropMgrPageListBoxStyle_MultipleItemSelect;
                    partsList.Height = 100;
                    partsList.AddItems(items);
                }

                swPropertyPage.Show();                
            }
        }
    }
}
