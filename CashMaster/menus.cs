using System.ComponentModel.DataAnnotations;

namespace CashMaster;

public class menus
{
    //Functions as a main menu with options for different functions
    public static void mainMenu(register register, employee employee)
    {
        int option = 0;
        while (true)
        {
            clearConsole();
            Console.WriteLine($"Hello {employee.employeeName}. Please enter one of the following options: ");
            Console.WriteLine("[1] View orders");
            Console.WriteLine("[2] Make new order");
            Console.WriteLine("[3] Add customer");
            Console.WriteLine("[4] Add item");
            Console.WriteLine("[5] Exit");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    viewOrders(register);
                    break;
                case 2:
                    addOrder(register);
                    break;
                case 3:
                    showMessage("add customer");
                    break;
                case 4:
                    showMessage("add item");
                    break;
                case 5:
                    return;
            }
        }
    }

    //Functions as a menu for editing an order. It gives options for functions to perform to an order
    private static void orderMenu(register register, order order)
    {
        int option = 0;
        while (true)
        {
            clearConsole();
            Console.WriteLine($"Currently editing order {order.orderID}. Please choose one of the following options: ");
            Console.WriteLine("[1] Add item to order");
            Console.WriteLine("[2] Exit");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    addOrderLine(register, order);
                    break;
                case 2:
                    return;
            }
        }
        
    }

    //Prints a message on screen and waits for user to continue
    private static void showMessage(string message)
    {
        clearConsole();
        Console.WriteLine(message);
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
    }
    
    //Calls the getEmployee and enterPassword functions in order to form a login screen
    public static employee login(register register)
    {
        var employee = getEmployee(register);
        if(enterPassword(employee)) return(employee);
        return null;
    }

    //Asks user to enter an employee ID looks ID up in register.employees
    private static employee getEmployee(register register)
    {
        while (true)
        {
            clearConsole();
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
                showMessage("ID not recognised, please try again.");
            }
        }
    }

    //Asks user to enter password and checks it.
    private static bool enterPassword(employee employee)
    {
        while (true)
        {
            clearConsole();
            Console.WriteLine($"Hello {employee.employeeName}! Please enter your password: ");
            var password = Console.ReadLine();
            if(password == employee.passwordHash) return true;
            showMessage("Password incorrect, please try again.");
        }
    }

    //Creates a new order based on user input
    private static void addOrder(register register)
    {
        var customer = getCustomer(register);
        clearConsole();
        Console.WriteLine("Please enter an order ID: ");
        var orderID = int.Parse(Console.ReadLine());
        var order = new order(orderID, customer);
        register.orders.Add(order);
        orderMenu(register, order);
    }

    //Gets customer from ID from user input
    private static customer getCustomer(register register)
    {
        while (true)
        {
            clearConsole();
            Console.WriteLine("Please enter the customer ID: ");
            try
            {
                int customerID = int.Parse(Console.ReadLine());
                var customer = register.customers.FirstOrDefault(a => a.customerID == customerID);
                Console.WriteLine(customer.customerName);
                return customer;
            }
            catch
            {
                showMessage("ID not recognised, please try again.");
            }
        }
    }

    //Creates orderLine based on user input
    private static void addOrderLine(register register, order order)
    {
        var item = getItem(register);
        clearConsole();
        Console.WriteLine("Please enter the amount of items to be added to the order: ");
        var quantity = int.Parse(Console.ReadLine());
        order.orderLines.Add(new orderLine(item, quantity));
        showMessage($"The item \"{item.itemName}\" has been added to the order.");
    }

    //Gets item from ID from user input
    private static item getItem(register register)
    {
        while (true)
        {
            clearConsole();
            Console.WriteLine("Please enter the item ID: ");
            try
            {
                int itemID = int.Parse(Console.ReadLine());
                var item = register.items.FirstOrDefault(a => a.itemID == itemID);
                Console.WriteLine(item.itemName);
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
    public static void viewOrders(register register)
    {
        if (register.orders.Count == 0)
        {
            showMessage("There are currently no orders available to view.");
            return;
        }
        foreach (var order in register.orders)
        {
            Console.WriteLine($"ID: {order.orderID}, customer name: {order.customer.customerName}, total price: {order.getTotalPrice()}, items:");
            if (order.orderLines.Count == 0) Console.WriteLine("This order has no items.");
            else
            {
                foreach (var orderLine in order.orderLines)
                {
                    Console.WriteLine($" • {orderLine.item.itemName}, quantity: {orderLine.quantity}, total price: {orderLine.getTotalPrice()}");
                }
            }
        }
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    //Clears console window and prints logo
    private static void clearConsole()
    {
        Console.Clear();
        Console.WriteLine("   ___          _                      _            \n  / __\\__ _ ___| |__   /\\/\\   __ _ ___| |_ ___ _ __ \n / /  / _` / __| '_ \\ /    \\ / _` / __| __/ _ \\ '__|\n/ /__| (_| \\__ \\ | | / /\\/\\ \\ (_| \\__ \\ ||  __/ |   \n\\____/\\__,_|___/_| |_\\/    \\/\\__,_|___/\\__\\___|_|   \n                                                    ");
    }
}