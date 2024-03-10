namespace CashMaster;

public class address
{
    public string country { get; set; }
    public string region { get; set; }
    public string addressLine { get; set; }
    public string postalCode { get; set; }

    public address(string country, string region, string addressLine, string postalCode)
    {
        this.country = country;
        this.region = region;
        this.addressLine = addressLine;
        this.postalCode = postalCode;
    }
}