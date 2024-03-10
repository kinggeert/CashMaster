using System.ComponentModel.DataAnnotations;

namespace CashMaster;

public class menus
{
    public static void mainMenu(register register, employee employee)
    {
        int option = 0;
        while (true)
        {
            Console.Clear();
            printLogo();
            Console.WriteLine($"Hello {employee.employeeName}. Please enter one of the following options: ");
            Console.WriteLine("[1] View orders");
            Console.WriteLine("[2] Make new order");
            Console.WriteLine("[3] Exit");
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
                    return;
            }
        }
    }

    private static void orderMenu(register register, order order)
    {
        int option = 0;
        while (true)
        {
            Console.Clear();
            printLogo();
            Console.WriteLine($"Currently editing order {order.orderID}. Please choose one of the following options: ");
            Console.WriteLine("[1] Add item to order");
            Console.WriteLine("[2] Exit");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    addItem(register, order);
                    break;
                case 2:
                    return;
            }
        }
        
    }
    
    public static employee login(register register)
    {
        var employee = getEmployee(register);
        if(enterPassword(employee)) return(employee);
        return null;
    }

    private static employee getEmployee(register register)
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

    private static bool enterPassword(employee employee)
    {
        while (true)
        {
            Console.Clear();
            printLogo();
            Console.WriteLine($"Hello {employee.employeeName}! Please enter your password: ");
            var password = Console.ReadLine();
            if(password == employee.passwordHash) return true;
            Console.WriteLine("Password incorrect, please try again. Press enter to continue.");
            Console.ReadLine();
        }
    }

    private static void addOrder(register register)
    {
        var customer = getCustomer(register);
        Console.Clear();
        printLogo();
        Console.WriteLine("Please enter an order ID: ");
        var orderID = int.Parse(Console.ReadLine());
        var order = new order(orderID, customer);
        register.orders.Add(order);
        orderMenu(register, order);
    }

    private static customer getCustomer(register register)
    {
        while (true)
        {
            Console.Clear();
            printLogo();
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
                Console.WriteLine("ID not recognised, please try again. press enter to continue.");
                Console.ReadLine();
            }
        }
    }

    private static void addItem(register register, order order)
    {
        var item = getItem(register);
        Console.Clear();
        printLogo();
        Console.WriteLine("Please enter the amount of items to be added to the order: ");
        var quantity = int.Parse(Console.ReadLine());
        order.orderLines.Add(new orderLine(item, quantity));
        Console.WriteLine($"The item \"{item.itemName}\" has been added to the order. Press enter to continue.");
        Console.ReadLine();
    }

    private static item getItem(register register)
    {
        while (true)
        {
            Console.Clear();
            printLogo();
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

    public static void viewOrders(register register)
    {
        if (register.orders.Count == 0)
        {
            Console.WriteLine("There are currently no orders available to view. Press enter to continue.");
            Console.ReadLine();
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

    public static void printLogo()
    {
        Console.WriteLine("   ___          _                      _            \n  / __\\__ _ ___| |__   /\\/\\   __ _ ___| |_ ___ _ __ \n / /  / _` / __| '_ \\ /    \\ / _` / __| __/ _ \\ '__|\n/ /__| (_| \\__ \\ | | / /\\/\\ \\ (_| \\__ \\ ||  __/ |   \n\\____/\\__,_|___/_| |_\\/    \\/\\__,_|___/\\__\\___|_|   \n                                                    ");
    }
}