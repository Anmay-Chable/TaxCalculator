namespace TaxCalculator
{
    partial class frmTaxCalculator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TaxableIncomeLabel = new Label();
            IncomeOwedLabel = new Label();
            txbTaxableIncome = new TextBox();
            txbIncomeOwed = new TextBox();
            btnCalculate = new Button();
            btnExit = new Button();
            menuStrip1 = new MenuStrip();
            loadToolStripMenuItem = new ToolStripMenuItem();
            taxScheduleToolStripMenuItem = new ToolStripMenuItem();
            employeeIncomeToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            currentTaxScheduleToolStripMenuItem = new ToolStripMenuItem();
            employeeTaxesToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveEmpolyeeTaxesToFileToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TaxableIncomeLabel
            // 
            TaxableIncomeLabel.AutoSize = true;
            TaxableIncomeLabel.Location = new Point(59, 102);
            TaxableIncomeLabel.Margin = new Padding(4, 0, 4, 0);
            TaxableIncomeLabel.Name = "TaxableIncomeLabel";
            TaxableIncomeLabel.Size = new Size(183, 32);
            TaxableIncomeLabel.TabIndex = 0;
            TaxableIncomeLabel.Text = "Taxable income:";
            // 
            // IncomeOwedLabel
            // 
            IncomeOwedLabel.AutoSize = true;
            IncomeOwedLabel.Location = new Point(59, 183);
            IncomeOwedLabel.Margin = new Padding(4, 0, 4, 0);
            IncomeOwedLabel.Name = "IncomeOwedLabel";
            IncomeOwedLabel.Size = new Size(201, 32);
            IncomeOwedLabel.TabIndex = 1;
            IncomeOwedLabel.Text = "Income tax owed:";
            // 
            // txbTaxableIncome
            // 
            txbTaxableIncome.Location = new Point(331, 102);
            txbTaxableIncome.Margin = new Padding(4, 2, 4, 2);
            txbTaxableIncome.Name = "txbTaxableIncome";
            txbTaxableIncome.Size = new Size(201, 39);
            txbTaxableIncome.TabIndex = 2;
            // 
            // txbIncomeOwed
            // 
            txbIncomeOwed.Location = new Point(331, 179);
            txbIncomeOwed.Margin = new Padding(4, 2, 4, 2);
            txbIncomeOwed.Name = "txbIncomeOwed";
            txbIncomeOwed.ReadOnly = true;
            txbIncomeOwed.Size = new Size(201, 39);
            txbIncomeOwed.TabIndex = 3;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(11, 301);
            btnCalculate.Margin = new Padding(4, 2, 4, 2);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(150, 47);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "&Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(518, 301);
            btnExit.Margin = new Padding(4, 2, 4, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 47);
            btnExit.TabIndex = 5;
            btnExit.Text = "E&xit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadToolStripMenuItem, showToolStripMenuItem, saveToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(682, 42);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { taxScheduleToolStripMenuItem, employeeIncomeToolStripMenuItem });
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(85, 38);
            loadToolStripMenuItem.Text = "Load";
            // 
            // taxScheduleToolStripMenuItem
            // 
            taxScheduleToolStripMenuItem.Name = "taxScheduleToolStripMenuItem";
            taxScheduleToolStripMenuItem.Size = new Size(355, 44);
            taxScheduleToolStripMenuItem.Text = "Tax Schedule ...";
            taxScheduleToolStripMenuItem.Click += taxScheduleToolStripMenuItem_Click;
            // 
            // employeeIncomeToolStripMenuItem
            // 
            employeeIncomeToolStripMenuItem.Name = "employeeIncomeToolStripMenuItem";
            employeeIncomeToolStripMenuItem.Size = new Size(355, 44);
            employeeIncomeToolStripMenuItem.Text = "Employee Income ..";
            employeeIncomeToolStripMenuItem.Click += employeeIncomeToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { currentTaxScheduleToolStripMenuItem, employeeTaxesToolStripMenuItem });
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(92, 38);
            showToolStripMenuItem.Text = "Show";
            // 
            // currentTaxScheduleToolStripMenuItem
            // 
            currentTaxScheduleToolStripMenuItem.Name = "currentTaxScheduleToolStripMenuItem";
            currentTaxScheduleToolStripMenuItem.Size = new Size(372, 44);
            currentTaxScheduleToolStripMenuItem.Text = "Current Tax Schedule";
            currentTaxScheduleToolStripMenuItem.Click += DisplayLoadedTaxSchedule;
            // 
            // employeeTaxesToolStripMenuItem
            // 
            employeeTaxesToolStripMenuItem.Name = "employeeTaxesToolStripMenuItem";
            employeeTaxesToolStripMenuItem.Size = new Size(372, 44);
            employeeTaxesToolStripMenuItem.Text = "Employee Taxes";
            employeeTaxesToolStripMenuItem.Click += DisplayIndividualEmployeeTaxDue;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveEmpolyeeTaxesToFileToolStripMenuItem });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(84, 38);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveEmpolyeeTaxesToFileToolStripMenuItem
            // 
            saveEmpolyeeTaxesToFileToolStripMenuItem.Name = "saveEmpolyeeTaxesToFileToolStripMenuItem";
            saveEmpolyeeTaxesToFileToolStripMenuItem.Size = new Size(467, 44);
            saveEmpolyeeTaxesToFileToolStripMenuItem.Text = "Save Empolyee Taxes to File ...";
            saveEmpolyeeTaxesToFileToolStripMenuItem.Click += saveEmpolyeeTaxesToFileToolStripMenuItem_Click;
            // 
            // frmTaxCalculator
            // 
            AcceptButton = btnCalculate;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(682, 367);
            Controls.Add(btnExit);
            Controls.Add(btnCalculate);
            Controls.Add(txbIncomeOwed);
            Controls.Add(txbTaxableIncome);
            Controls.Add(IncomeOwedLabel);
            Controls.Add(TaxableIncomeLabel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 2, 4, 2);
            Name = "frmTaxCalculator";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Income Tax Calculator";
            Load += frmTaxCalculator_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TaxableIncomeLabel;
        private Label IncomeOwedLabel;
        private TextBox txbTaxableIncome;
        private TextBox txbIncomeOwed;
        private Button btnCalculate;
        private Button btnExit;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem taxScheduleToolStripMenuItem;
        private ToolStripMenuItem employeeIncomeToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem currentTaxScheduleToolStripMenuItem;
        private ToolStripMenuItem employeeTaxesToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveEmpolyeeTaxesToFileToolStripMenuItem;
    }
}
