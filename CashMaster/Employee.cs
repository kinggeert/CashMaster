namespace CashMaster;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }

    public Employee(int employeeId, string employeeName, string passwordHash)
    {
        Id = employeeId;
        Name = employeeName;
        PasswordHash = passwordHash;
    }
}