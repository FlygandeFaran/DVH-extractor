namespace DVHextractor
{
    partial class MainForm
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
            this.lsbAvailableContours = new System.Windows.Forms.ListBox();
            this.lsbSelectedContours = new System.Windows.Forms.ListBox();
            this.btnAddContour = new System.Windows.Forms.Button();
            this.btnRemoveContour = new System.Windows.Forms.Button();
            this.lsbAdditionalValues = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblVolOrDose = new System.Windows.Forms.Label();
            this.lsbAbsOrRelOutput = new System.Windows.Forms.ListBox();
            this.cbIsKeepValue = new System.Windows.Forms.CheckBox();
            this.lsbAbsOrRelInput = new System.Windows.Forms.ListBox();
            this.lsbDoseOrVol = new System.Windows.Forms.ListBox();
            this.lblInputUnit = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvDVHtable = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lsListOfPlans = new System.Windows.Forms.ListBox();
            this.lbListOfCourses = new System.Windows.Forms.ListBox();
            this.folder = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdlgLoadTemplate = new System.Windows.Forms.OpenFileDialog();
            this.sfdlgSaveTemplate = new System.Windows.Forms.SaveFileDialog();
            this.btnClearList = new System.Windows.Forms.Button();
            this.rbAllUnCalc = new System.Windows.Forms.RadioButton();
            this.rbExtremeUnCalc = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.lsbAbsOrRelTable = new System.Windows.Forms.ListBox();
            this.sfdlgSaveToExcel = new System.Windows.Forms.SaveFileDialog();
            this.addToExistingTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDVHtable)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbAvailableContours
            // 
            this.lsbAvailableContours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbAvailableContours.FormattingEnabled = true;
            this.lsbAvailableContours.Location = new System.Drawing.Point(11, 43);
            this.lsbAvailableContours.Name = "lsbAvailableContours";
            this.lsbAvailableContours.Size = new System.Drawing.Size(142, 277);
            this.lsbAvailableContours.TabIndex = 0;
            // 
            // lsbSelectedContours
            // 
            this.lsbSelectedContours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbSelectedContours.FormattingEnabled = true;
            this.lsbSelectedContours.Location = new System.Drawing.Point(365, 43);
            this.lsbSelectedContours.Name = "lsbSelectedContours";
            this.lsbSelectedContours.Size = new System.Drawing.Size(142, 277);
            this.lsbSelectedContours.TabIndex = 1;
            // 
            // btnAddContour
            // 
            this.btnAddContour.Location = new System.Drawing.Point(65, 19);
            this.btnAddContour.Name = "btnAddContour";
            this.btnAddContour.Size = new System.Drawing.Size(75, 23);
            this.btnAddContour.TabIndex = 2;
            this.btnAddContour.Text = "->";
            this.btnAddContour.UseVisualStyleBackColor = true;
            this.btnAddContour.Click += new System.EventHandler(this.btnAddContour_Click);
            // 
            // btnRemoveContour
            // 
            this.btnRemoveContour.Location = new System.Drawing.Point(65, 65);
            this.btnRemoveContour.Name = "btnRemoveContour";
            this.btnRemoveContour.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveContour.TabIndex = 3;
            this.btnRemoveContour.Text = "<-";
            this.btnRemoveContour.UseVisualStyleBackColor = true;
            this.btnRemoveContour.Click += new System.EventHandler(this.btnRemoveContour_Click);
            // 
            // lsbAdditionalValues
            // 
            this.lsbAdditionalValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbAdditionalValues.Enabled = false;
            this.lsbAdditionalValues.FormattingEnabled = true;
            this.lsbAdditionalValues.Location = new System.Drawing.Point(513, 43);
            this.lsbAdditionalValues.Name = "lsbAdditionalValues";
            this.lsbAdditionalValues.Size = new System.Drawing.Size(82, 277);
            this.lsbAdditionalValues.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lsbAdditionalValues);
            this.groupBox1.Controls.Add(this.lsbAvailableContours);
            this.groupBox1.Controls.Add(this.lsbSelectedContours);
            this.groupBox1.Location = new System.Drawing.Point(12, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 335);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contour Selection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Available Contours";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(510, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Additional Inputs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Selected Contours";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAddContour);
            this.groupBox3.Controls.Add(this.btnRemoveContour);
            this.groupBox3.Location = new System.Drawing.Point(159, 220);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Add or Remove";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblVolOrDose);
            this.groupBox2.Controls.Add(this.lsbAbsOrRelOutput);
            this.groupBox2.Controls.Add(this.cbIsKeepValue);
            this.groupBox2.Controls.Add(this.lsbAbsOrRelInput);
            this.groupBox2.Controls.Add(this.lsbDoseOrVol);
            this.groupBox2.Controls.Add(this.lblInputUnit);
            this.groupBox2.Controls.Add(this.txtInput);
            this.groupBox2.Location = new System.Drawing.Point(159, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 128);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Value";
            // 
            // lblVolOrDose
            // 
            this.lblVolOrDose.AutoSize = true;
            this.lblVolOrDose.Location = new System.Drawing.Point(38, 32);
            this.lblVolOrDose.Name = "lblVolOrDose";
            this.lblVolOrDose.Size = new System.Drawing.Size(27, 13);
            this.lblVolOrDose.TabIndex = 21;
            this.lblVolOrDose.Text = "V/D";
            // 
            // lsbAbsOrRelOutput
            // 
            this.lsbAbsOrRelOutput.FormattingEnabled = true;
            this.lsbAbsOrRelOutput.Location = new System.Drawing.Point(137, 84);
            this.lsbAbsOrRelOutput.Name = "lsbAbsOrRelOutput";
            this.lsbAbsOrRelOutput.Size = new System.Drawing.Size(57, 30);
            this.lsbAbsOrRelOutput.TabIndex = 20;
            this.lsbAbsOrRelOutput.SelectedIndexChanged += new System.EventHandler(this.lsbAbsOrRelOutput_SelectedIndexChanged);
            // 
            // cbIsKeepValue
            // 
            this.cbIsKeepValue.AutoSize = true;
            this.cbIsKeepValue.Location = new System.Drawing.Point(72, 61);
            this.cbIsKeepValue.Name = "cbIsKeepValue";
            this.cbIsKeepValue.Size = new System.Drawing.Size(80, 17);
            this.cbIsKeepValue.TabIndex = 19;
            this.cbIsKeepValue.Text = "Keep value";
            this.cbIsKeepValue.UseVisualStyleBackColor = true;
            // 
            // lsbAbsOrRelInput
            // 
            this.lsbAbsOrRelInput.FormattingEnabled = true;
            this.lsbAbsOrRelInput.Location = new System.Drawing.Point(72, 84);
            this.lsbAbsOrRelInput.Name = "lsbAbsOrRelInput";
            this.lsbAbsOrRelInput.Size = new System.Drawing.Size(57, 30);
            this.lsbAbsOrRelInput.TabIndex = 18;
            this.lsbAbsOrRelInput.SelectedIndexChanged += new System.EventHandler(this.lsbAbsOrRel_SelectedIndexChanged);
            // 
            // lsbDoseOrVol
            // 
            this.lsbDoseOrVol.FormattingEnabled = true;
            this.lsbDoseOrVol.Location = new System.Drawing.Point(6, 84);
            this.lsbDoseOrVol.Name = "lsbDoseOrVol";
            this.lsbDoseOrVol.Size = new System.Drawing.Size(57, 30);
            this.lsbDoseOrVol.TabIndex = 17;
            this.lsbDoseOrVol.SelectedIndexChanged += new System.EventHandler(this.lsbDoseOrVol_SelectedIndexChanged);
            // 
            // lblInputUnit
            // 
            this.lblInputUnit.AutoSize = true;
            this.lblInputUnit.Location = new System.Drawing.Point(134, 32);
            this.lblInputUnit.Name = "lblInputUnit";
            this.lblInputUnit.Size = new System.Drawing.Size(60, 13);
            this.lblInputUnit.TabIndex = 8;
            this.lblInputUnit.Text = "lblInputUnit";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(71, 29);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(57, 20);
            this.txtInput.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dgvDVHtable);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(624, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(579, 482);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Results";
            // 
            // dgvDVHtable
            // 
            this.dgvDVHtable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDVHtable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDVHtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDVHtable.Location = new System.Drawing.Point(9, 42);
            this.dgvDVHtable.Name = "dgvDVHtable";
            this.dgvDVHtable.Size = new System.Drawing.Size(564, 425);
            this.dgvDVHtable.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "DVH table";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1234, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTemplateToolStripMenuItem,
            this.loadTemplateToolStripMenuItem,
            this.exportToCsvToolStripMenuItem,
            this.addToExistingTemplateToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveTemplateToolStripMenuItem
            // 
            this.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
            this.saveTemplateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveTemplateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveTemplateToolStripMenuItem.Text = "Save Template";
            this.saveTemplateToolStripMenuItem.Click += new System.EventHandler(this.saveTemplateToolStripMenuItem_Click);
            // 
            // loadTemplateToolStripMenuItem
            // 
            this.loadTemplateToolStripMenuItem.Name = "loadTemplateToolStripMenuItem";
            this.loadTemplateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadTemplateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.loadTemplateToolStripMenuItem.Text = "Load Template";
            this.loadTemplateToolStripMenuItem.Click += new System.EventHandler(this.loadTemplateToolStripMenuItem_Click);
            // 
            // exportToCsvToolStripMenuItem
            // 
            this.exportToCsvToolStripMenuItem.Name = "exportToCsvToolStripMenuItem";
            this.exportToCsvToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToCsvToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportToCsvToolStripMenuItem.Text = "Export to csv";
            this.exportToCsvToolStripMenuItem.Click += new System.EventHandler(this.exportToCsvToolStripMenuItem_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCalculate.Location = new System.Drawing.Point(890, 519);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(96, 30);
            this.btnCalculate.TabIndex = 9;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lsListOfPlans);
            this.groupBox5.Controls.Add(this.lbListOfCourses);
            this.groupBox5.Location = new System.Drawing.Point(12, 31);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(271, 141);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Plan Selection";
            // 
            // lsListOfPlans
            // 
            this.lsListOfPlans.FormattingEnabled = true;
            this.lsListOfPlans.Location = new System.Drawing.Point(145, 19);
            this.lsListOfPlans.Name = "lsListOfPlans";
            this.lsListOfPlans.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsListOfPlans.Size = new System.Drawing.Size(120, 108);
            this.lsListOfPlans.TabIndex = 1;
            this.lsListOfPlans.SelectedIndexChanged += new System.EventHandler(this.lsListOfPlans_SelectedIndexChanged);
            // 
            // lbListOfCourses
            // 
            this.lbListOfCourses.FormattingEnabled = true;
            this.lbListOfCourses.Location = new System.Drawing.Point(6, 19);
            this.lbListOfCourses.Name = "lbListOfCourses";
            this.lbListOfCourses.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbListOfCourses.Size = new System.Drawing.Size(120, 108);
            this.lbListOfCourses.TabIndex = 0;
            this.lbListOfCourses.SelectedIndexChanged += new System.EventHandler(this.lbListOfCourses_SelectedIndexChanged);
            // 
            // ofdlgLoadTemplate
            // 
            this.ofdlgLoadTemplate.FileName = "openFileDialog1";
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(377, 519);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 11;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // rbAllUnCalc
            // 
            this.rbAllUnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbAllUnCalc.AutoSize = true;
            this.rbAllUnCalc.Location = new System.Drawing.Point(633, 519);
            this.rbAllUnCalc.Name = "rbAllUnCalc";
            this.rbAllUnCalc.Size = new System.Drawing.Size(153, 17);
            this.rbAllUnCalc.TabIndex = 12;
            this.rbAllUnCalc.TabStop = true;
            this.rbAllUnCalc.Text = "All Uncertainty Calculations";
            this.rbAllUnCalc.UseVisualStyleBackColor = true;
            // 
            // rbExtremeUnCalc
            // 
            this.rbExtremeUnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbExtremeUnCalc.AutoSize = true;
            this.rbExtremeUnCalc.Location = new System.Drawing.Point(633, 542);
            this.rbExtremeUnCalc.Name = "rbExtremeUnCalc";
            this.rbExtremeUnCalc.Size = new System.Drawing.Size(184, 17);
            this.rbExtremeUnCalc.TabIndex = 13;
            this.rbExtremeUnCalc.TabStop = true;
            this.rbExtremeUnCalc.Text = "Min/Max Uncertainty Calculations";
            this.rbExtremeUnCalc.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(633, 565);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 14;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // lsbAbsOrRelTable
            // 
            this.lsbAbsOrRelTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbAbsOrRelTable.FormattingEnabled = true;
            this.lsbAbsOrRelTable.Location = new System.Drawing.Point(1083, 519);
            this.lsbAbsOrRelTable.Name = "lsbAbsOrRelTable";
            this.lsbAbsOrRelTable.Size = new System.Drawing.Size(120, 30);
            this.lsbAbsOrRelTable.TabIndex = 16;
            this.lsbAbsOrRelTable.SelectedIndexChanged += new System.EventHandler(this.lsbAbsOrRelTable_SelectedIndexChanged);
            // 
            // addToExistingTemplateToolStripMenuItem
            // 
            this.addToExistingTemplateToolStripMenuItem.Name = "addToExistingTemplateToolStripMenuItem";
            this.addToExistingTemplateToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.addToExistingTemplateToolStripMenuItem.Text = "Add to existing Template";
            this.addToExistingTemplateToolStripMenuItem.Click += new System.EventHandler(this.addToExistingTemplateToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 614);
            this.Controls.Add(this.lsbAbsOrRelTable);
            this.Controls.Add(this.rbNone);
            this.Controls.Add(this.rbExtremeUnCalc);
            this.Controls.Add(this.rbAllUnCalc);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DVH Calculator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDVHtable)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbAvailableContours;
        private System.Windows.Forms.ListBox lsbSelectedContours;
        private System.Windows.Forms.Button btnAddContour;
        private System.Windows.Forms.Button btnRemoveContour;
        private System.Windows.Forms.ListBox lsbAdditionalValues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInputUnit;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCsvToolStripMenuItem;
        private System.Windows.Forms.ListBox lsbDoseOrVol;
        private System.Windows.Forms.ListBox lsbAbsOrRelInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsKeepValue;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.DataGridView dgvDVHtable;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lsListOfPlans;
        private System.Windows.Forms.ListBox lbListOfCourses;
        private System.Windows.Forms.FolderBrowserDialog folder;
        private System.Windows.Forms.OpenFileDialog ofdlgLoadTemplate;
        private System.Windows.Forms.SaveFileDialog sfdlgSaveTemplate;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.RadioButton rbAllUnCalc;
        private System.Windows.Forms.RadioButton rbExtremeUnCalc;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.ListBox lsbAbsOrRelTable;
        private System.Windows.Forms.ListBox lsbAbsOrRelOutput;
        private System.Windows.Forms.Label lblVolOrDose;
        private System.Windows.Forms.SaveFileDialog sfdlgSaveToExcel;
        private System.Windows.Forms.ToolStripMenuItem addToExistingTemplateToolStripMenuItem;
    }
}

