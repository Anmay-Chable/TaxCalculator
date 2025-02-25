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
                MessageBox.Show("You selected " + openFileDialog.FileName);// Check point file succesfully opened
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // created by Ashraf
            // New Comment From Angel
        }
    }
}
