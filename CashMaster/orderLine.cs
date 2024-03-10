namespace CashMaster;

public class orderLine
{
    public int quantity { get; set; }

    public orderLine(int quantity)
    {
        this.quantity = quantity;
    }
}