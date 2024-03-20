namespace CashMaster;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "CashMaster";
        register register = new register(0, 1);
//        initItems(register);
        initCustomers(register);
        initEmployees(register);
        var employee = menus.login(register);
        menus.mainMenu(register, employee);
    }
    
    static void initItems(register register)
    {
        register.items.Add(new item(0, "Frikandel", "Mora", 2.95, 30, ""));
        register.items.Add(new item(1, "Kroket", "Mora", 2.50, 24, ""));
        register.items.Add(new item(2, "Bami Blok", "Indo Deepfry Special", 3.50, 40, ""));
        register.items.Add(new item(3, "Hamburger", "Hamburger Speciaalzaak", 6.25, 10, ""));
        register.items.Add(new item(4, "Bitterballen", "Mora", 4, 25, ""));
    }

    static void initCustomers(register register)
    {
        register.customers.Add(new customer(0, "Joost Klein", new address("Netherlands", "Friesland", "Europapalaan 24", "8663PA"), "kleine@gmail.com", new customerCard(0)));
        register.customers.Add(new customer(1, "Leonke", new address("Belgium", "Flanders", "Kangaroestraat 3", "9476KA"), "leonke@gmail.com", new customerCard(1)));
    }

    static void initEmployees(register register)
    {
        register.employees.Add(new employee(0, "John Tana", "Boursin"));
    }
}