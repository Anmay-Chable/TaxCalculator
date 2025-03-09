using System;
using TaxCalculator;

public class frmTaxCalculationCollection
{
    public decimal CalculateTax(decimal employeeSalary, List<frmTaxCalculator.TaxBracket> taxSchedule)
    {
        decimal taxDue = 0;

        foreach (var slice in taxSchedule.OrderBy(s => s.LowerBound))
        {
            if (employeeSalary <= slice.LowerBound) break;

            decimal incomeInSlice = Math.Min(employeeSalary, slice.UpperBound) - slice.LowerBound;
            if (incomeInSlice <= 0) continue;

            taxDue += slice.BaseTax + (incomeInSlice * (slice.TaxRate / 100));
        }
        return taxDue;
    }
    public List<EmployeeTaxResult> CalculateTaxesForAllEmployees(string[,] employeeIncomeData, List<frmTaxCalculator.TaxBracket> taxSchedule)
    {
        List<EmployeeTaxResult> results = new List<EmployeeTaxResult>();
        int rows = employeeIncomeData.GetLength(0);

        for (int i = 1; i < rows; i++)
        {
            decimal salary = Convert.ToDecimal(employeeIncomeData[i, 1]);
            decimal tax = CalculateTax(salary, taxSchedule);

            results.Add(new EmployeeTaxResult
            {
                EmployeeID = Convert.ToString(i),
                Salary = salary,
                TaxDue = tax
            });
        }
        return results;
    }
    public class EmployeeTaxResult
    {
        public string EmployeeID { get; set; }
        public decimal Salary { get; set; }
        public decimal TaxDue { get; set; }
    }
}
