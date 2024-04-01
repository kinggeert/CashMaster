namespace CashMaster;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public string Email { get; set; }

    public Customer(int id, string name, Address address, string customerEmail)
    {
        Id = id;
        Name = name;
        Address = address;
        Email = customerEmail;
    }

    public Customer(string name, Address address, string customerEmail)
    {
        Name = name;
        Address = address;
        Email = customerEmail;
    }

    public void CreateCustomer()
    {
        Address.CreateAddress();
        Dal.AddCustomer(this);
    }

    public static List<Customer> GetCustomers()
    {
        Dal dal = new();
        return dal.GetAllCustomers();
    }
}