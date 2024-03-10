namespace CashMaster;

public class order
{
    public int orderID { get; set; }
    public bool orderComplete { get; set; }

    public order(int orderID)
    {
        this.orderID = orderID;
        orderComplete = false;
    }
}