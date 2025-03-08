using Microsoft.Extensions.Primitives;
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
        string[] taxHeaders = new string[] { "LowerBound", "UpperBound", "BaseTax", "ExcessOver" };
        string[] employeeHeaders = new string[] { "EmployeeID", "Salary" };

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

        // Method to load the tax schedule from a CSV file
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
                    ReadCsvFile(filePath, isTaxSchedule, isTaxSchedule ? taxHeaders : employeeHeaders); // call the method to read the file
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
                if (taxSchedule.Count == 0)
                {
                    MessageBox.Show("Please load a tax schdule first.", "Entry Error");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txbTaxableIncome.Text))
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
                else if (employeeIncomeData != null && employeeIncomeData.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(txbTaxableIncome.Text))
                    {
                        MessageBox.Show("Please remove all values from the taxable income field when an employee income csv file is loaded.", "Entry Error");
                        return;
                    }
                    else
                    {
                        List<frmTaxCalculationCollection.EmployeeTaxResult> results = calculationCollection.CalculateTaxesForAllEmployees(employeeIncomeData, taxSchedule);
                        DisplayResults(results);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating taxes: {ex.Message}");
            }
        }

        private void ReadCsvFile(string filePath, bool isTaxSchedule, string[] values)
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
                    stringvalues = line.Split(','); // Split the line into an array of values.

                    if (isTaxSchedule)
                    {
                        // Parse and add the tax bracket to the list
                        TaxBracket taxBracket = new();
                        taxBracket.LowerBound = decimal.Parse(values[0]);
                        taxBracket.UpperBound = decimal.Parse(values[1]);
                        taxBracket.BaseTax = decimal.Parse(values[2]);
                        taxBracket.TaxRate = decimal.Parse(values[3]);
                        taxBracket.ExcessOver = decimal.Parse(values[4]);
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
        private void DisplayResults(List<frmTaxCalculationCollection.EmployeeTaxResult> results)
        {
            //dataGridView1.DataSource = results;
            decimal totalTax = results.Sum(r => r.TaxDue);
            txbIncomeOwed.Text = $"Total Tax Owed: ${totalTax}";
        }
    }

}
