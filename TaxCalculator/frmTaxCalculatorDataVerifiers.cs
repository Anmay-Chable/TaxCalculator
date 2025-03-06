using System;

public class frmTaxCalculatorDataVerifiers
{
	public frmTaxCalculatorDataVerifiers()
	{

	}

	public void dataVerifierCSharpTester ()
	{
		MessageBox.Show("frmTaxCalculatorDataVerifiers Loaded. ","Entry Validater");
	}
    public bool IsDecimal(string inpValue, int employeeID)
    {
        string msg = "";
        if (!decimal.TryParse(inpValue, out _))
        {
            msg = "Taxable income must be a valid decimal value";
            if (employeeID != -1) { msg += " \n Error Found at ID: " + employeeID; }
            MessageBox.Show(msg, "Entry Error");
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsPresent(string inpValue, int employeeID) //use -1 for manual entries
    {
        string msg = "";
        if (inpValue == "")
        {
            msg = "Taxable income is a required field.";
            if (employeeID != -1) { msg += " \n Error Found at ID: " + employeeID;}
            MessageBox.Show(msg, "Entry Error");
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsWithinRange(string inpValue, int employeeID)
    {
        string msg = "";
        if (decimal.TryParse(inpValue, out decimal number))
        {
            if (number < 0 )
            {
                msg = "Taxable income must be greater than zero.";
                if (employeeID != -1) { msg += " \n Error Found at ID: " + employeeID; }
                MessageBox.Show(msg, "Entry Error");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }

    }
}
