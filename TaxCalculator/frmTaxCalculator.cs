using System.IO;

namespace TaxCalculator
{
    public partial class frmTaxCalculator : Form
    {
        /* This List store a collection of TaxBracket objects
         each TaxBracket object represents a tax bracket in the tax schedule */
        List<TaxBracket> taxSchedule = new List<TaxBracket>();
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

        // Method to read the tax schedule from the csv file
        private void taxScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFile dialog to allow the user to select the tax schedule
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv) | *.csv| All file (*.*) | *.*"; // Filter to show only csv files
            openFileDialog.Title = "Select Tax Schedule CSV File";

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

        // Method to read the employee income from the csv file
        private void employeeIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFile dialog to allow the user to select the tax schedule
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv) | *.csv| All file (*.*) | *.*"; // Filter to show only csv files
            openFileDialog.Title = "Select Employee Income CSV File";

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
        private void ReadCsvFile(string filePath)
        {
            taxSchedule.Clear(); // clear the list before reading the file
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)// read the file line by line
                    {
                        // split the line into an array of values
                        string[] values = line.Split(',');
                        TaxBracket taxBracket = new TaxBracket();
                        taxBracket.LowerBound = decimal.Parse(values[0]);
                        taxBracket.UpperBound = decimal.Parse(values[1]);
                        taxBracket.BaseTax = decimal.Parse(values[2]);
                        taxBracket.TaxRate = decimal.Parse(values[3]);
                        taxBracket.ExcessOver = decimal.Parse(values[4]);
                        taxSchedule.Add(taxBracket);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the file: " + ex.Message); //
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // created by Ashraf
            // New Comment From Angel
        }


    }
}
