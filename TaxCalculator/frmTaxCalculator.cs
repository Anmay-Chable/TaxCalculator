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

                    MessageBox.Show("You Selected: " + fileName, "Loading File"); // display only the file name

                    //ReadCsvFile(filePath); // call the method to read the file
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
            // created by Ashraf
            // New Comment From Angel

            /* verifier.dataVerifierCSharpTester(); 
                'debut' test to make sure that the frmTaxCalculatorDataVerifiers.cs methods can be called
            verifier.IsPresent(txbTaxableIncome.Text,-1);       -1 is used for manual entries since the second method input is to show errors at which employee ID
            verifier.IsWithinRange(txbTaxableIncome.Text, -1);
            verifier.IsDecimal(txbTaxableIncome.Text, -1); */

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
    }

}
