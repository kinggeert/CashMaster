namespace CashMaster;

public class OrderLine
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public OrderLine(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public double GetTotalPrice()
    {
        return Item.Price * Quantity;
    }

    public void CreateOrderLine(Order order)
    {
        Dal.AddOrderLine(this, order);
    }
}