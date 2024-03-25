namespace CashMaster;

public class register
{
    public int registerID { get; set; }
    public int stockNotificationThreshold { get; set; }
    public List<order> orders { get; set; }
    public List<Customer> customers { get; set; }
    public List<employee> employees { get; set; }
    public List<item> items { get; set; }

    public register(int registerID, int stockNotificationThreshold)
    {
        this.registerID = registerID;
        this.stockNotificationThreshold = stockNotificationThreshold;
        orders = new List<order>();
        customers = Customer.GetCustomers();
        employees = new List<employee>();
        items = item.GetItems();
    }
}