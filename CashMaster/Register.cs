namespace CashMaster;

public class Register
{
    public int Id { get; set; }
    public int StockNotificationThreshold { get; set; }
    public List<Employee> Employees { get; set; }

    public Register(int registerId, int stockNotificationThreshold)
    {
        this.Id = registerId;
        this.StockNotificationThreshold = stockNotificationThreshold;
        Employees = new List<Employee>();
    }
}