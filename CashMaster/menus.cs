namespace CashMaster;

public class menus
{
    public static employee login(register register)
    {
        var employee = getEmployee(register);
        if(enterPassword(employee)) {return(employee);}
        return (null);
    }

    static employee getEmployee(register register)
    {
        while (true)
        {
            Console.Clear();
            printLogo();
            Console.WriteLine("Please enter your employee ID: ");
            try
            {
                int employeeID = int.Parse(Console.ReadLine());
                var employee = register.employees.FirstOrDefault(a => a.employeeID == employeeID);
                Console.WriteLine(employee.employeeName);
                return employee;
            }
            catch
            {
                Console.WriteLine("ID not recognised, please try again. press enter to continue.");
                Console.ReadLine();
            }
        }
    }

    static bool enterPassword(employee employee)
    {
        while (true)
        {
            Console.Clear();
            printLogo();
            Console.WriteLine($"Hello {employee.employeeName}! Please enter your password: ");
            var password = Console.ReadLine();
            if(password == employee.passwordHash) {return(true);}
            Console.WriteLine("Password incorrect, please try again. Press enter to continue.");
            Console.ReadLine();
        }
    }

    public static void printLogo()
    {
        Console.WriteLine("   ___          _                      _            \n  / __\\__ _ ___| |__   /\\/\\   __ _ ___| |_ ___ _ __ \n / /  / _` / __| '_ \\ /    \\ / _` / __| __/ _ \\ '__|\n/ /__| (_| \\__ \\ | | / /\\/\\ \\ (_| \\__ \\ ||  __/ |   \n\\____/\\__,_|___/_| |_\\/    \\/\\__,_|___/\\__\\___|_|   \n                                                    ");
    }
}