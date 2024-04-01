namespace CashMaster;

public class Order
{
    public int Id { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public Customer Customer { get; set; }
    public bool OrderComplete { get; set; }

    public Order(int orderId, List<OrderLine> orderLines, Customer customer, bool orderComplete)
    {
        Id = orderId;
        Customer = customer;
        OrderLines = orderLines;
        OrderComplete = orderComplete;
    }

    public Order(Customer customer)
    {
        Customer = customer;
        OrderLines = new List<OrderLine>();
        OrderComplete = false;
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var orderLine in OrderLines)
        {
            total += orderLine.GetTotalPrice();
        }

        return double.Round(total, 2);
    }

    public void CreateOrder()
    {
        Dal dal = new Dal();
        Dal.AddOrder(this);
    }

    public static List<Order> GetOrders()
    {
        Dal dal = new Dal();
        return dal.GetAllOrders();
    }
}