namespace CashMaster;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public string Email { get; set; }
    public CustomerCard CustomerCard { get; set; }

    public Customer(int id, string name, Address address, string customerEmail, CustomerCard customerCard)
    {
        Id = id;
        Name = name;
        Address = address;
        Email = customerEmail;
        CustomerCard = customerCard;
    }

    public Customer(string name, Address address, string customerEmail, CustomerCard customerCard)
    {
        Name = name;
        Address = address;
        Email = customerEmail;
        CustomerCard = customerCard;
    }

    public void CreateCustomer()
    {
        Address.CreateAddress();
        Dal dal = new Dal();
        Dal.AddCustomer(this);
    }

    public static List<Customer> GetCustomers()
    {
        Dal dal = new Dal();
        return dal.GetAllCustomers();
    }
}