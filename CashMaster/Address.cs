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
        this.Id = id;
        this.Country = country;
        this.Region = region;
        this.City = city;
        this.AddressLine = addressLine;
        this.PostalCode = postalCode;
    }
    
    public Address(string country, string region, string addressLine, string postalCode)
    {
        this.Country = country;
        this.Region = region;
        this.AddressLine = addressLine;
        this.PostalCode = postalCode;
    }

    public void CreateAddress()
    {
        Dal dal = new Dal();
        Dal.AddAddress(this);
    }
}