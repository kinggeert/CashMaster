namespace CashMaster;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "CashMaster";
        Register register = new Register(0, 1);
        initEmployees(register);
        var employee = Menus.Login(register);
        Menus.MainMenu(register, employee);
    }

    static void initEmployees(Register register)
    {
        register.Employees.Add(new Employee(0, "John Tana", "Boursin"));
    }
}