namespace CashMaster;

public class item
{
    public int itemID { get; set; }
    public string itemName { get; set; }
    public string brand { get; set; }
    public double price { get; set; }
    public int stock { get; set; }
    public string location { get; set; }

    public item(int itemID, string itemName, string brand, double price, int stock)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.brand = brand;
        this.price = price;
        this.stock = stock;
        location = "";
    }
}