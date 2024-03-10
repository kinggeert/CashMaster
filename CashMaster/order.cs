namespace CashMaster;

public class order
{
    public int orderID { get; set; }
    public List<orderLine> orderLines { get; set; }
    public customer customer { get; set; }
    public bool orderComplete { get; set; }

    public order(int orderID, customer customer)
    {
        this.orderID = orderID;
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
}