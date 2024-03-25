namespace CashMaster;

public class Address
{
    public int id { get; set; }
    public string country { get; set; }
    public string region { get; set; }
    public string city { get; set; }
    public string addressLine { get; set; }
    public string postalCode { get; set; }

    public Address(int id, string country, string region, string city, string addressLine, string postalCode)
    {
        this.id = id;
        this.country = country;
        this.region = region;
        this.city = city;
        this.addressLine = addressLine;
        this.postalCode = postalCode;
    }
    
    public Address(string country, string region, string addressLine, string postalCode)
    {
        this.country = country;
        this.region = region;
        this.addressLine = addressLine;
        this.postalCode = postalCode;
    }

    public void CreateAddress()
    {
        DAL dal = new DAL();
        dal.AddAddress(this);
    }
}