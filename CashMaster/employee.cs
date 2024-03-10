namespace CashMaster;

public class employee
{
    public int employeeID { get; set; }
    public string passwordHash { get; set; }

    public employee(int employeeID, string passwordHash)
    {
        this.employeeID = employeeID;
        this.passwordHash = passwordHash;
    }
}