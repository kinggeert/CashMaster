namespace CashMaster;

public class orderLine
{
    public item item { get; set; }
    public int quantity { get; set; }

    public orderLine(item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}