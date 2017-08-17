namespace SWErgotronAddin
{
    partial class PrintFromAssembly
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SelectAll = new System.Windows.Forms.CheckBox();
            this.rbtnOpenDrawings = new System.Windows.Forms.RadioButton();
            this.rbtnPrintDrawings = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(92, 298);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "O&K";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectAll
            // 
            this.SelectAll.AutoSize = true;
            this.SelectAll.Location = new System.Drawing.Point(13, 303);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(70, 17);
            this.SelectAll.TabIndex = 2;
            this.SelectAll.Text = "Select All";
            this.SelectAll.UseVisualStyleBackColor = true;
            // 
            // rbtnOpenDrawings
            // 
            this.rbtnOpenDrawings.AutoSize = true;
            this.rbtnOpenDrawings.Checked = true;
            this.rbtnOpenDrawings.Location = new System.Drawing.Point(13, 271);
            this.rbtnOpenDrawings.Name = "rbtnOpenDrawings";
            this.rbtnOpenDrawings.Size = new System.Drawing.Size(98, 17);
            this.rbtnOpenDrawings.TabIndex = 3;
            this.rbtnOpenDrawings.TabStop = true;
            this.rbtnOpenDrawings.Text = "Open Drawings";
            this.rbtnOpenDrawings.UseVisualStyleBackColor = true;
            // 
            // rbtnPrintDrawings
            // 
            this.rbtnPrintDrawings.AutoSize = true;
            this.rbtnPrintDrawings.Location = new System.Drawing.Point(13, 246);
            this.rbtnPrintDrawings.Name = "rbtnPrintDrawings";
            this.rbtnPrintDrawings.Size = new System.Drawing.Size(93, 17);
            this.rbtnPrintDrawings.TabIndex = 4;
            this.rbtnPrintDrawings.Text = "Print Drawings";
            this.rbtnPrintDrawings.UseVisualStyleBackColor = true;
            // 
            // PrintFromAssembly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 332);
            this.Controls.Add(this.rbtnPrintDrawings);
            this.Controls.Add(this.rbtnOpenDrawings);
            this.Controls.Add(this.SelectAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Name = "PrintFromAssembly";
            this.Text = "PrintFromAssembly";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox SelectAll;
        private System.Windows.Forms.RadioButton rbtnOpenDrawings;
        private System.Windows.Forms.RadioButton rbtnPrintDrawings;
    }
}