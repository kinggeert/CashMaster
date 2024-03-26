namespace CashMaster;

public class Register
{
    public int Id { get; set; }
    public int StockNotificationThreshold { get; set; }
    public List<Order> Orders { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Item> Items { get; set; }

    public Register(int registerId, int stockNotificationThreshold)
    {
        this.Id = registerId;
        this.StockNotificationThreshold = stockNotificationThreshold;
        Orders = new List<Order>();
        Customers = Customer.GetCustomers();
        Employees = new List<Employee>();
        Items = Item.GetItems();
    }
}