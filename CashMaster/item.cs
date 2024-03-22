namespace CashMaster;

public class item
{
    public int itemId { get; set; }
    public string itemName { get; set; }
    public string brand { get; set; }
    public double price { get; set; }
    public int stock { get; set; }
    public string location { get; set; }

    public item(int itemId, string itemName, string brand, double price, int stock, string location)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.brand = brand;
        this.price = price;
        this.stock = stock;
        this.location = location;
    }

    public void CreateItem()
    {
        DAL dal = new DAL();
        dal.AddItem(this);
    }

    public static List<item> GetItems()
    {
        DAL dal = new DAL();
        return dal.GetAllItems();
    }
}