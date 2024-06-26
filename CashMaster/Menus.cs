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
            Console.WriteLine("[5] View Items");
            Console.WriteLine("[0] Exit");
            var option = int.Parse(Console.ReadLine());
            try
            {
                switch (option)
                {
                    case 0:
                        return;
                    case 1:
                        ViewOrders();
                        break;
                    case 2:
                        AddOrder(register);
                        break;
                    case 3:
                        AddCustomer();
                        break;
                    case 4:
                        AddItem();
                        break;
                    case 5:
                        viewItems();
                        break;
                }
            }
            catch
            {
                
            }
        }
    }

    //Functions as a menu for editing an order. It gives options for functions to perform to an order
    private static void OrderMenu(Order order)
    {
        while (true)
        {
            ClearConsole();
            Console.WriteLine($"Currently editing order {order.Id}. This order contains the following items:");
            foreach (OrderLine orderLine in order.OrderLines)
            {
                Console.WriteLine($" • {orderLine.Item.Name}, quantity: {orderLine.Quantity}, total price: {orderLine.GetTotalPrice()}, Location: {orderLine.Item.Location}");
            }
            Console.WriteLine("");
            
            Console.WriteLine("Please select one of the following options:");
            Console.WriteLine("[1] Add item to order");
            Console.WriteLine("[0] Exit");
            
            var option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 0:
                    return;
                case 1:
                    AddOrderLine(order);
                    break;
            }
        }
        
    }
    
    //Displays all orders
    private static void ViewOrders()
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
            Console.WriteLine($"ID: {order.Id}, customer name: {order.Customer.Name}, total price: {order.GetTotalPrice()}");
        }
        
        Console.WriteLine("Press enter to continue or enter order id to edit order.");
        try
        {
            int orderId = int.Parse(Console.ReadLine());
            var order = orders.FirstOrDefault(a => a.Id == orderId);
            OrderMenu(order);
        }
        catch
        {
            
        }
    }

    private static void viewItems()
    {
        Dal dal = new();
        var items = dal.GetAllItems();

        ClearConsole();
        foreach (Item item in items)
        {
            Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Brand: {item.Brand}, Price: {item.Price}, Stock: {item.Stock}, Location: {item.Location}");
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
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

    private static void AddCustomer()
    {
        string name = GetInput("Please enter the customer name.");
        string email = GetInput("Please enter the customer Email.");
        Address address = AddAddress();
        Customer customer = new(name, address, email);
        customer.CreateCustomer();
    }
    
    private static Address AddAddress()
    {
        string country = GetInput("Please enter the country.");
        string region = GetInput("Please enter the region.");
        string city = GetInput("Please enter the city.");
        string addressLine = GetInput("Please enter the address line.");
        string postalCode = GetInput("Please enter the postal code.");
        return new Address(country, region, city, addressLine, postalCode);
    }

    private static void AddItem()
    {
        string name = GetInput("Please enter the name of the item.");
        string brand = GetInput("Please enter the brand of the item.");
        double price = Convert.ToDouble(GetInput("Please enter the price of the item."));
        int stock = Convert.ToInt32(GetInput("Please enter the current stock of the item."));
        string location = GetInput("Please enter the location of the item.");
        Item item = new Item(name, brand, price, stock, location);
        item.CreateItem();
    }
    
    //Creates a new order based on user input
    private static void AddOrder(Register register)
    {
        var customer = GetCustomer();
        var order = new Order(customer);
        order.CreateOrder();
        OrderMenu(order);
    }

    //Gets customer from ID from user input
    private static Customer GetCustomer()
    {
        while (true)
        {
            ClearConsole();
            Console.Write("Please enter the customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            try
            {
                Dal dal = new Dal();
                var customer = dal.GetCustomerFromId(customerId);
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
    private static void AddOrderLine(Order order)
    {
        var item = GetItem();
        ClearConsole();
        Console.Write("Please enter the amount of items to be added to the order: ");
        var quantity = int.Parse(Console.ReadLine());
        OrderLine orderLine = new OrderLine(item, quantity);
        orderLine.CreateOrderLine(order);
        order.OrderLines.Add(orderLine);
        ShowMessage($"The item \"{item.Name}\" has been added to the order.");
    }

    //Gets item from ID from user input
    private static Item GetItem()
    {
        while (true)
        {
            ClearConsole();
            Console.Write("Please enter the item ID: ");
            try
            {
                int itemId = int.Parse(Console.ReadLine());
                Dal dal = new();
                var item = dal.GetItemFromId(itemId);
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