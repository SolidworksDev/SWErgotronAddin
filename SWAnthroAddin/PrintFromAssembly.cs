using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.swconst;
using SolidWorksTools;
using SolidWorksTools.File;
using System.Diagnostics;
using System.IO;


namespace SWErgotronAddin
{
    public partial class PrintFromAssembly : Form
    {
        AssemblyDoc currentAssembly;
        ModelDoc2 currentModel;
        object[] AllComponents;
        List<string> DrawingList = new List<string>();
        DrawingList Drawings = new DrawingList();
        PageSetup pageSetup;
        int[] sheets = new int[1];        
        ISldWorks SwApp;


        public PrintFromAssembly(ISldWorks iSwApp)
        {
            InitializeComponent();
            SwApp = iSwApp;         
        }

        public Control.ControlCollection DrawingListFormControls
        {
            get { return this.Controls; }
        }

        
        public void DrawingsDialog_AddListBox(Control.ControlCollection inControls)
        {


	        try {
		            System.Drawing.Point LablePosition = default(System.Drawing.Point);
		            LablePosition.X = 10;
		            LablePosition.Y = 10;

		            System.Drawing.Point ListPosition = default(System.Drawing.Point);
		            ListPosition.X = 10;
		            ListPosition.Y = 40;

		            CheckedListBox newList = new CheckedListBox();
		            System.Windows.Forms.Label newLable = new System.Windows.Forms.Label();

		            newList.CheckOnClick = true;

		            newLable.Name = "DrawingsListDialogLable";
		            newLable.Text = "Select Drawings to Print";
		            newLable.Width = 200;
		            newLable.Location = LablePosition;

		            newList.Text = "Select Drawings";
		            newList.Name = "DrawingsListBox";
		            newList.Width = 245;
		            newList.Height = 200;
		            newList.Location = ListPosition;
                                

                    currentAssembly = (AssemblyDoc)SwApp.ActiveDoc;
                    currentModel = (ModelDoc2)SwApp.ActiveDoc;

                    Drawings.Add(new Drawing(Path.GetFileName(currentModel.GetPathName()), currentModel.GetPathName()));
                    newList.Items.Add(Path.GetFileName(currentModel.GetPathName()));

                    if (object.ReferenceEquals(currentAssembly, null))
                    {
                        MessageBox.Show("No Assembly Opened");
                        return;
                    }

                    AllComponents = currentAssembly.GetComponents(false);

                    foreach (object comp in AllComponents)
                    {
                        Component2 icomp = (Component2)comp;
                        ModelDoc2 modeldoc2 = icomp.GetModelDoc2();

                        if (!object.ReferenceEquals(modeldoc2, null))
                        {                        
                            if (newList.FindString(Path.GetFileNameWithoutExtension(modeldoc2.GetPathName())) == ListBox.NoMatches)
                            {                            
                                if (Path.GetFileName(modeldoc2.GetPathName()).isDrawing())
                                {                            
                                    Drawings.Add(new Drawing(Path.GetFileName(modeldoc2.GetPathName()), modeldoc2.GetPathName()));
                                    newList.Items.Add(Path.GetFileName(modeldoc2.GetPathName()));
                                }
                            }                        
                        }
                    }

                    newList.Sorted = true;
		            inControls.Add(newLable);
		            inControls.Add(newList);

	            } catch (Exception ex) {
		            MessageBox.Show(ex.ToString());
	        }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            VerifyForm verifyForm = new VerifyForm();
            Control.ControlCollection DrawingListControls = verifyForm.DrawingListFormControls;
           
            foreach (Control aControl in this.Controls)
            {                
                if (aControl.Name == "DrawingsListBox")
                {
                    CheckedListBox currentCheckedListBox = (CheckedListBox)aControl;

                    if (currentCheckedListBox.CheckedItems.Count == 0)
                    {
                        verifyForm.Dispose();
                        MessageBox.Show("You must select at least" + '\n' + "one drawing from the list", "Please Select a File", 
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                       
                        return;
                    }

                    foreach (object item in ((CheckedListBox)aControl).CheckedItems)
                    {
                        if (item.GetType() == typeof(String))
                        {
                            if (!DrawingList.Contains(item.ToString()))
                            {
                                DrawingList.Add(Path.ChangeExtension(item.ToString(),"slddrw"));
                            }
                        }
                    }
                }
            }

            this.Visible = false;

            if (rbtnOpenDrawings.Checked)
            {

                verifyForm.Icon = Properties.Resources.folder;
                verifyForm.Text = "Verify Drawings to Open";
                verifyForm.VerifyDialog_AddListBox(DrawingList, DrawingListControls,
                                                   "DrawingsListDialogLable",
                                                   "Open the following drawings?",
                                                   "DrawingsListBox",
                                                   "Selected Drawings");
            }
            else
            {
                verifyForm.Icon = Properties.Resources.printer;
                verifyForm.Text = "Verify Drawings to Print";
                verifyForm.VerifyDialog_AddListBox(DrawingList, DrawingListControls,
                                                   "DrawingsListDialogLable",
                                                   "Open the following drawings?",
                                                   "DrawingsListBox",
                                                   "Selected Drawings");
            }

            verifyForm.ShowDialog();
            sheets[0] = 0;

            if (verifyForm.bAcceptClicked == true)
            {
                foreach (Drawing drawing in Drawings)
                {
                    if (DrawingList.Contains(Path.ChangeExtension(drawing.DrawingName, "slddrw")))
                    {
                        if (rbtnOpenDrawings.Checked)
                        {
                            string drawingName = Path.ChangeExtension(drawing.DrawingFullPath, "slddrw");
                            SwApp.OpenDoc6(drawingName, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_ReadOnly,
                                            "", (int)swFileLoadError_e.swGenericError, (int)swFileLoadWarning_e.swFileLoadWarning_ModelOutOfDate);
                        }
                        else
                        {
                            ModelDoc2 currentDrawing;
                            ModelDocExtension modelDocExt;
                            string drawingName = Path.ChangeExtension(drawing.DrawingFullPath, "slddrw");
 
                            currentDrawing = SwApp.OpenDoc6(drawingName, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                                            "", (int)swFileLoadError_e.swGenericError, (int)swFileLoadWarning_e.swFileLoadWarning_ModelOutOfDate);
                            pageSetup = currentDrawing.PageSetup;
                            pageSetup.Orientation = 2;
                            modelDocExt = currentDrawing.Extension;
                            modelDocExt.PrintOut3(sheets, 1, true, "\\\\anthro3\\p56b", "", true);
                            SwApp.CloseDoc(drawingName);
                        }
                    }
                }
            }
            else
            {
                DrawingList.Clear();
                Drawings.Clear();
                verifyForm.Dispose();
                this.Visible = true;
            }
        }
    }
}
