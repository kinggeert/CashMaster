namespace CashMaster;

public class customer
{
    public int customerID { get; set; }
    public string customerName { get; set; }
    public address customerAddress { get; set; }
    public string customerEmail { get; set; }
    public customerCard customerCard { get; set; }

    public customer(int customerID, string customerName, address customerAddress, string customerEmail, customerCard customerCard)
    {
        this.customerID = customerID;
        this.customerName = customerName;
        this.customerAddress = customerAddress;
        this.customerEmail = customerEmail;
        this.customerCard = customerCard;
    }
}