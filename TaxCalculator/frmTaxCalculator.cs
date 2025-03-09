using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.IO;
using System.Net.Http.Headers;

namespace TaxCalculator
{
    public partial class frmTaxCalculator : Form
    {
        /* This List store a collection of TaxBracket objects
         each TaxBracket object represents a tax bracket in the tax schedule */
        List<TaxBracket> taxSchedule = new List<TaxBracket>();
        string[,] employeeIncomeData; // 2D array to store the employee income data
        private string[] stringvalues;
        frmTaxCalculatorDataVerifiers verifier = new frmTaxCalculatorDataVerifiers();
        frmTaxCalculationCollection calculationCollection = new frmTaxCalculationCollection();
        //string[] taxHeaders = new string[] { "LowerBound", "UpperBound", "BaseTax", "ExcessOver" };
        //string[] taxHeaders = new string[] { "Slice", "MinIncome", "MaxIncome", "MinTax", "TaxRate" };
        //string[] employeeHeaders = new string[] { "EmployeeID", "Salary" };

        private string taxScheduleFilePath = "";
        private string employeeIncomeFilePath = "";

        public frmTaxCalculator()
        {
            InitializeComponent();
        }

        // This class is used to store a tax bracket
        public class TaxBracket
        {
            // Properties of the TaxBracket class & using { get; set; } to get and set the values
            public decimal SliceIndex { get; set; }
            public decimal LowerBound { get; set; }
            public decimal UpperBound { get; set; }
            public decimal BaseTax { get; set; }
            public decimal TaxRate { get; set; }
            public decimal ExcessOver { get; set; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTaxCalculator_Load(object sender, EventArgs e)
        {

        }

        // Method to load the tax schedule from a CSV file 4.1**
        private void LoadCsvFile(bool isTaxSchedule)
        {
            //OpenFile dialog to allow the user to select the tax schedule & or employee income file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv) | *.csv| All file (*.*) | *.*"; // Filter to show only csv files
            openFileDialog.Title = isTaxSchedule ? "Select Tax Schedule CSV File" : "Select Employee Income CSV File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // attempt to validate the file type and handle potential exceptions
                try
                {
                    string filePath = openFileDialog.FileName;
                    if (!filePath.ToLower().EndsWith(".csv"))
                    {
                        // if the file is not a CSV file, throw an exception
                        throw new ArgumentException("Invalid file type. Please select a CSV file.");
                    }

                    string fileName = Path.GetFileName(filePath);
                    if (isTaxSchedule) { taxScheduleFilePath = filePath; } else { employeeIncomeFilePath = filePath; }
                    MessageBox.Show("You Selected: " + fileName, "Loading File"); // display only the file name

                    //for (int i = 0; i < taxScheduleFilePath.Length; i++) 
                    //{ 
                    ReadCsvFile(filePath, isTaxSchedule/*, isTaxSchedule ? taxHeaders : employeeHeaders*/); // call the method to read the file
                    //}
                }
                catch (ArgumentException ex)
                {
                    // display an error message if the file type is invalid
                    MessageBox.Show("Error reading the file: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // display an error message if an unexpected error occurs
                    MessageBox.Show("An unexpected error occured: " + ex.Message);
                }
            }

        }

        // Method to read the tax schedule from the csv file
        private void taxScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCsvFile(true); // call the method to read the file
        }

        // Method to read the employee income from the csv file
        private void employeeIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCsvFile(false);// call the method to read the file
        }

        // Method to calculate the tax for each employee
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                verifier.IsCsvFileLoaded(taxScheduleFilePath, true, true);

                if (!string.IsNullOrWhiteSpace(txbTaxableIncome.Text) && !verifier.IsCsvFileLoaded(employeeIncomeFilePath, false, false))
                {
                    if (!verifier.IsDecimal(txbTaxableIncome.Text, -1))
                    {
                        MessageBox.Show("Please enter a vaild decimal number for taxable income or an employee income spread sheet first.", "Entry Error");
                        return;
                    }

                    decimal salary = decimal.Parse(txbTaxableIncome.Text);
                    decimal taxDue = Math.Round(calculationCollection.CalculateTax(salary, taxSchedule), 4);
                    txbIncomeOwed.Text = taxDue.ToString();
                }
                else if (verifier.IsCsvFileLoaded(employeeIncomeFilePath, false, true))
                {
                    if (!string.IsNullOrWhiteSpace(txbTaxableIncome.Text))
                    {
                        MessageBox.Show("Please remove all values from the taxable income field when an employee income csv file is loaded.", "Entry Error");
                        return;
                    }
                    else
                    {
                        calculationCollection.CalculateTaxesForAllEmployees(employeeIncomeData, taxSchedule); // Calculate taxes
                        List<frmTaxCalculationCollection.EmployeeTaxResult> results = calculationCollection.Results; // Access the property
                        DisplayTotalEmployeeTaxDue(results);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating taxes: {ex.Message}");
            }
        }

        private void ReadCsvFile(string filePath, bool isTaxSchedule/*, string[] values*/)
        {
            try
            {
                if (isTaxSchedule)
                {
                    taxSchedule.Clear(); // Clear existing tax schedule data
                }

                List<string[]> rows = new(); // Create a list to store the rows read from the file.

                foreach (var line in File.ReadLines(filePath))
                {
                    string[] values = line.Split(','); // Split the line into an array of values.

                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                        if (string.IsNullOrEmpty(values[i]))
                        {
                            values[i] = "0"; // Replace missing values with "0"
                        }
                    }

                    if (isTaxSchedule)
                    {
                        // Parse and add the tax bracket to the list
                        TaxBracket taxBracket = new();
                        decimal.TryParse(values[1], System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out decimal minIncome);
                        decimal.TryParse(values[2], System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out decimal MaxIncome);
                        decimal.TryParse(values[3], System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out decimal MinTax);
                        decimal.TryParse(values[4], System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.InvariantCulture, out decimal TaxRate);
                        /* taxBracket.LowerBound = decimal.Parse(values[1]);
                        taxBracket.UpperBound = decimal.Parse(values[2]);
                        taxBracket.BaseTax = decimal.Parse(values[3]);
                        taxBracket.TaxRate = decimal.Parse(values[4]);
                        taxBracket.ExcessOver = decimal.Parse(values[5]); */

                        taxBracket.LowerBound = minIncome;
                        taxBracket.UpperBound = MaxIncome;
                        taxBracket.BaseTax = MinTax;
                        taxBracket.TaxRate = TaxRate;

                        taxSchedule.Add(taxBracket);
                    }
                    else
                    {
                        rows.Add(values); // Add the row to the list for employee income data
                    }
                }
                if (!isTaxSchedule)
                { // prevent accessing an empty list
                    int numRows = rows.Count, numCols = rows[0].Length;
                    employeeIncomeData = new string[numRows, numCols]; // Create a 2D array to store the employee income data

                    // Copy the data from the list to the 2D array
                    for (int i = 0; i < numRows; i++)
                        for (int j = 0; j < numCols; j++)
                            employeeIncomeData[i, j] = rows[i][j];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading the file: {ex.Message}");
            }
        }
        private void DisplayTotalEmployeeTaxDue(List<frmTaxCalculationCollection.EmployeeTaxResult> results)
        {

            decimal totalTax = results.Sum(r => r.TaxDue);
            MessageBox.Show($"Total Tax Owed: ${totalTax:N2}", "Total Employee Tax Due");

        }
        private void DisplayIndividualEmployeeTaxDue(string[,] employeeTaxData)
        {
            string msg = "Employee Id     |     Employee Tax Amount\n";

            int rows = employeeTaxData.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                msg += ($"${i}     |     " + $"${employeeTaxData[i, 1]:N2}\n");
            }

            MessageBox.Show(msg, "Individual Employee Tax Amount");
        }

        private void DisplayIndividualEmployeeTaxDue(object sender, EventArgs e)
        {
            if (verifier.IsCsvFileLoaded(taxScheduleFilePath, true, true) && verifier.IsCsvFileLoaded(employeeIncomeFilePath, false, true))
            {
                List<frmTaxCalculationCollection.EmployeeTaxResult> employeeTaxData = calculationCollection.CalculateTaxesForAllEmployees(employeeIncomeData, taxSchedule); ;

                int idColumnWidth = 12;
                int taxColumnWidth = 20;

                string msg = $"{"Employee Id".PadRight(idColumnWidth)}| {"Employee Tax Amount".PadLeft(taxColumnWidth)}\n";
                msg += new string('-', idColumnWidth + taxColumnWidth + 3) + "\n";

                foreach (var employee in employeeTaxData)
                {
                    msg += $"{employee.EmployeeID.ToString().PadRight(idColumnWidth)}| " +
                           $"{employee.TaxDue.ToString("C2").PadLeft(taxColumnWidth)}\n";
                }

                MessageBox.Show(msg, "Individual Employee Tax Amount");
            }
            else
            {
                return;
            }
        }
        private void DisplayLoadedTaxSchedule(object sender, EventArgs e)
        {
            string filePath = taxScheduleFilePath;
            string msg = "";

            if (verifier.IsCsvFileLoaded(filePath, true, true))
            {
                int i = 1;
                msg += "File path: " + filePath + "\n\n";
                msg += "Slice | MinIncome | MaxIncome | MinTax | TaxRate\n";
                foreach (var slice in taxSchedule.OrderBy(s => s.LowerBound))
                {
                    msg += Convert.ToString(i) + " | " + Convert.ToString(slice.LowerBound) +
                    " | " + Convert.ToString(slice.UpperBound) + " | " + Convert.ToString(slice.BaseTax) +
                    " | " + Convert.ToString(slice.TaxRate) + "\n";
                    i++;
                }
                MessageBox.Show(msg, "Loaded Tax Schedule");
            }
        }
        private void txbTaxableIncomeChangeUpdate(object sender, EventArgs e)
        {
            txbIncomeOwed.Clear();
        }

        private void saveEmpolyeeTaxesToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv) | *.csv| All file (*.*) | *.*";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Title = "Save Employee Tax Data";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                frmSaveFile.Save(saveFileDialog.FileName, calculationCollection.Results);
            }
        }
    }

}
