using System.ComponentModel.DataAnnotations;

namespace CashMaster;

public abstract class Menus
{
    //Functions as a main menu with options for different functions
    public static void MainMenu(Register register, Employee employee)
    {
        while (true)
        {
            ClearConsole();
            Console.WriteLine($"Hello {employee.Name}. Please enter one of the following options: ");
            Console.WriteLine("[1] View orders");
            Console.WriteLine("[2] Make new order");
            Console.WriteLine("[3] Add customer");
            Console.WriteLine("[4] Add item");
            Console.WriteLine("[5] Exit");
            var option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    ViewOrders(register);
                    break;
                case 2:
                    AddOrder(register);
                    break;
                case 3:
                    ShowMessage("add customer");
                    break;
                case 4:
                    AddItem(register);
                    break;
                case 5:
                    return;
            }
        }
    }

    //Functions as a menu for editing an order. It gives options for functions to perform to an order
    private static void OrderMenu(Register register, Order order)
    {
        while (true)
        {
            ClearConsole();
            Console.WriteLine($"Currently editing order {order.Id}. Please choose one of the following options: ");
            Console.WriteLine("[1] Add item to order");
            Console.WriteLine("[2] Exit");
            var option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    AddOrderLine(register, order);
                    break;
                case 2:
                    return;
            }
        }
        
    }

    //Prints a message on screen and waits for user to continue
    private static void ShowMessage(string message)
    {
        ClearConsole();
        Console.WriteLine(message);
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
    }
    
    //Calls the getEmployee and enterPassword functions in order to form a login screen
    public static Employee Login(Register register)
    {
        var employee = GetEmployee(register);
        if(EnterPassword(employee)) return(employee);
        return null;
    }

    //Asks user to enter an employee ID looks ID up in register.employees
    private static Employee GetEmployee(Register register)
    {
        while (true)
        {
            ClearConsole();
            Console.Write("Please enter your employee ID: ");
            try
            {
                int employeeId = int.Parse(Console.ReadLine());
                var employee = register.Employees.FirstOrDefault(a => a.Id == employeeId);
                Console.WriteLine(employee.Name);
                return employee;
            }
            catch
            {
                ShowMessage("ID not recognised, please try again.");
            }
        }
    }

    //Asks user to enter password and checks it.
    private static bool EnterPassword(Employee employee)
    {
        while (true)
        {
            ClearConsole();
            Console.Write($"Hello {employee.Name}! Please enter your password: ");
            var password = Console.ReadLine();
            if(password == employee.PasswordHash) return true;
            ShowMessage("Password incorrect, please try again.");
        }
    }

    private static void AddItem(Register register)
    {
        string name = GetInput("Please enter the name of the item.");
        string brand = GetInput("Please enter the brand of the item.");
        double price = Convert.ToDouble(GetInput("Please enter the price of the item."));
        int stock = Convert.ToInt32(GetInput("Please enter the current stock of the item."));
        string location = GetInput("Please enter the location of the item.");
        Item item = new Item(name, brand, price, stock, location);
        item.CreateItem();
        register.Items.Add(item);
    }
    
    //Creates a new order based on user input
    private static void AddOrder(Register register)
    {
        var customer = GetCustomer(register);
        var order = new Order(customer);
        order.CreateOrder();
        register.Orders.Add(order);
        OrderMenu(register, order);
    }

    //Gets customer from ID from user input
    private static Customer GetCustomer(Register register)
    {
        while (true)
        {
            ClearConsole();
            Console.Write("Please enter the customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            try
            {
                var customer = register.Customers.FirstOrDefault(a => a.Id == customerId);
                Console.WriteLine(customer.Name);
                return customer;
            }
            catch
            {
                ShowMessage("ID not recognised, please try again.");
            }
        }
    }

    //Creates orderLine based on user input
    private static void AddOrderLine(Register register, Order order)
    {
        var item = GetItem(register);
        ClearConsole();
        Console.Write("Please enter the amount of items to be added to the order: ");
        var quantity = int.Parse(Console.ReadLine());
        OrderLine orderLine = new OrderLine(item, quantity);
        orderLine.CreateOrderLine(order);
        order.OrderLines.Add(orderLine);
        ShowMessage($"The item \"{item.Name}\" has been added to the order.");
    }

    //Gets item from ID from user input
    private static Item GetItem(Register register)
    {
        while (true)
        {
            ClearConsole();
            Console.Write("Please enter the item ID: ");
            try
            {
                int itemId = int.Parse(Console.ReadLine());
                var item = register.Items.FirstOrDefault(a => a.Id == itemId);
                Console.WriteLine(item.Name);
                return item;
            }
            catch
            {
                Console.WriteLine("ID not recognised, please try again. press enter to continue.");
                Console.ReadLine();
            }
        }
    }

    //Displays all orders
    private static void ViewOrders(Register register)
    {
        List<Order> orders = Order.GetOrders();
        ClearConsole();
        if (orders.Count == 0)
        {
            ShowMessage("There are currently no orders available to view.");
            return;
        }
        foreach (Order order in orders)
        {
            Console.WriteLine($"ID: {order.Id}, customer name: {order.Customer.Name}, total price: {order.GetTotalPrice()}, items:");
            if (order.OrderLines.Count == 0) Console.WriteLine("This order has no items.");
            else
            {
                foreach (OrderLine orderLine in order.OrderLines)
                {
                    Console.WriteLine($" • {orderLine.Item.Name}, quantity: {orderLine.Quantity}, total price: {orderLine.GetTotalPrice()}");
                }
            }
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    private static string GetInput(string message)
    {
        ClearConsole();
        Console.WriteLine(message);
        return Console.ReadLine();
    }

    //Clears console window and prints logo
    private static void ClearConsole()
    {
        Console.Clear();
        Console.WriteLine("   ___          _                      _            \n  / __\\__ _ ___| |__   /\\/\\   __ _ ___| |_ ___ _ __ \n / /  / _` / __| '_ \\ /    \\ / _` / __| __/ _ \\ '__|\n/ /__| (_| \\__ \\ | | / /\\/\\ \\ (_| \\__ \\ ||  __/ |   \n\\____/\\__,_|___/_| |_\\/    \\/\\__,_|___/\\__\\___|_|   \n                                                    ");
    }
}