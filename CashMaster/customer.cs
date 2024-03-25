namespace CashMaster;

public class Customer
{
    public int customerID { get; set; }
    public string customerName { get; set; }
    public Address customerAddress { get; set; }
    public string customerEmail { get; set; }
    public customerCard customerCard { get; set; }

    public Customer(int customerID, string customerName, Address customerAddress, string customerEmail, customerCard customerCard)
    {
        this.customerID = customerID;
        this.customerName = customerName;
        this.customerAddress = customerAddress;
        this.customerEmail = customerEmail;
        this.customerCard = customerCard;
    }

    public Customer(string customerName, Address customerAddress, string customerEmail, customerCard customerCard)
    {
        this.customerName = customerName;
        this.customerAddress = customerAddress;
        this.customerEmail = customerEmail;
        this.customerCard = customerCard;
    }

    public void CreateCustomer()
    {
        customerAddress.CreateAddress();
        DAL dal = new DAL();
        dal.AddCustomer(this);
    }

    public static List<Customer> GetCustomers()
    {
        DAL dal = new DAL();
        return dal.GetAllCustomers();
    }
}