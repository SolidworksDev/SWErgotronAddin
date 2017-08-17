using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWErgotronAddin
{
    public partial class VerifyForm : Form
    {
        public Boolean bAcceptClicked = false;
        public Boolean bCancleClicked = false;

        public VerifyForm()
        {
            InitializeComponent();
        }

        public Control.ControlCollection DrawingListFormControls
        {
            get { return this.Controls; }
        }

        public bool VerifyDialog_AddListBox(List<string> drawingList, Control.ControlCollection inControls, string LableName, string LableText, string ListName, string ListText)
        {

            try
            {
                System.Drawing.Point LablePosition = default(System.Drawing.Point);
                LablePosition.X = 15;
                LablePosition.Y = 10;

                System.Drawing.Point ListPosition = default(System.Drawing.Point);
                ListPosition.X = 15;
                ListPosition.Y = 40;

                ListBox newList = new ListBox();
                System.Windows.Forms.Label newLable = new System.Windows.Forms.Label();

                newLable.Name = LableName;
                newLable.Text = LableText;
                newLable.Width = 250;
                newLable.Location = LablePosition;

                newList.Name = ListName;
                newList.Text = ListText;
                newList.Width = 225;
                newList.Height = 280;
                newList.Location = ListPosition;


                foreach (string drawing in drawingList)
                {
                    if (!newList.Items.Contains(drawing))
                    {
                        newList.Items.Add(drawing);
                    }

                }

                inControls.Add(newLable);
                inControls.Add(newList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bAcceptClicked = false;
            bCancleClicked = true;
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            bAcceptClicked = true;
            bCancleClicked = false;
            this.Dispose();
        }
    }
}
