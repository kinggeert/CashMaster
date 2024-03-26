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

    public double getTotalPrice()
    {
        return item.price * quantity;
    }

    public void CreateOrderLine(order order)
    {
        DAL dal = new DAL();
        dal.AddOrderLine(this, order);
    }
}