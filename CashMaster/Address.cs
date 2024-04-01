namespace CashMaster;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string AddressLine { get; set; }
    public string PostalCode { get; set; }

    public Address(int id, string country, string region, string city, string addressLine, string postalCode)
    {
        Id = id;
        Country = country;
        Region = region;
        City = city;
        AddressLine = addressLine;
        PostalCode = postalCode;
    }
    
    public Address(string country, string region, string city, string addressLine, string postalCode)
    {
        Country = country;
        Region = region;
        City = city;
        AddressLine = addressLine;
        PostalCode = postalCode;
    }

    public void CreateAddress()
    {
        Dal.AddAddress(this);
    }
}