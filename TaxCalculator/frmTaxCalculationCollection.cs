using System;
using TaxCalculator;

public class frmTaxCalculationCollection
{
    public List<EmployeeTaxResult> Results { get; private set; } = new();// Public property
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
        Results = new List<EmployeeTaxResult>(); // Initialize the list
        int rows = employeeIncomeData.GetLength(0);

        for (int i = 1; i < rows; i++)
        {
            decimal salary = Convert.ToDecimal(employeeIncomeData[i, 1]);
            decimal tax = CalculateTax(salary, taxSchedule);

            Results.Add(new EmployeeTaxResult
            {
                EmployeeID = Convert.ToString(i),
                //Salary = salary,
                TaxDue = tax
            });
        }
        return Results;
    }
    public class EmployeeTaxResult
    {
        public required string EmployeeID { get; set; }
        //public decimal Salary { get; set; }
        public decimal TaxDue { get; set; }
    }
}
