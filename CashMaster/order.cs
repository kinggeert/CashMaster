namespace CashMaster;

public class order
{
    public int orderID { get; set; }
    public List<orderLine> orderLines { get; set; }
    public Customer customer { get; set; }
    public bool orderComplete { get; set; }

    public order(int orderID, List<orderLine> orderLines, Customer customer, bool orderComplete)
    {
        this.orderID = orderID;
        this.customer = customer;
        this.orderLines = orderLines;
        this.orderComplete = orderComplete;
    }

    public order(Customer customer)
    {
        this.customer = customer;
        orderLines = new List<orderLine>();
        orderComplete = false;
    }

    public double getTotalPrice()
    {
        double total = 0;
        foreach (var orderLine in orderLines)
        {
            total += orderLine.getTotalPrice();
        }

        return total;
    }

    public void CreateOrder()
    {
        DAL dal = new DAL();
        dal.AddOrder(this);
    }

    public static List<order> GetOrders()
    {
        DAL dal = new DAL();
        return dal.GetAllOrders();
    }
}