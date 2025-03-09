using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator
{
    class frmSaveFile
    {
        public static void Save(string filePath, List<frmTaxCalculationCollection.EmployeeTaxResult> results)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // write header
                    writer.WriteLine("Employee ID, Taxable Income");

                    // data rows
                    foreach (var employeeTax in results)
                    {
                        writer.WriteLine($"{employeeTax.EmployeeID}, {employeeTax.TaxDue}");
                    }
                }
                MessageBox.Show("Employee tax data saved succesfully.", "Save Successfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}", "Save Error");
            }
        }
    }
}
