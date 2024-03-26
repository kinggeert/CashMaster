namespace CashMaster;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public string Location { get; set; }

    public Item(int itemId, string itemName, string brand, double price, int stock, string location)
    {
        Id = itemId;
        Name = itemName;
        Brand = brand;
        Price = price;
        Stock = stock;
        Location = location;
    }

    public void CreateItem()
    {
        Dal.AddItem(this);
    }

    public static List<Item> GetItems()
    {
        Dal dal = new Dal();
        return dal.GetAllItems();
    }
}