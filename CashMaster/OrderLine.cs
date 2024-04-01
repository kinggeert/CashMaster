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
        return double.Round(Item.Price * Quantity, 2);
    }

    public void CreateOrderLine(Order order)
    {
        Dal.AddOrderLine(this, order);
    }
}