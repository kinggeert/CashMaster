using System.Data.SqlClient;

namespace CashMaster;

public class DAL
{
    private readonly string connectionString = "Data Source=.;Initial Catalog=CashMaster;Integrated Security=True";

    public DAL()
    {
    }

    public void AddCustomer(Customer customer)
    {
        string query = "INSERT INTO Customer (Name, AddressId, Email, CustomerCardId) VALUES (@Name, @AddressId, @Email, @CustomerCardId)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", customer.customerName);
                command.Parameters.AddWithValue("@AddressId", customer.customerAddress.id);
                command.Parameters.AddWithValue("@Email", customer.customerEmail);
                
                //Get ID from database
                command.CommandText = "SELECT CAST(@@Identity as INT);";
                int id = (int)command.ExecuteScalar();
                customer.customerID= id;
            }
        }
    }
    
    public List<Customer> GetAllCustomers()
    {
        List<Customer> customers = new List<Customer>();
        string query = "SELECT * FROM Customer";

        using (SqlDataReader reader = GetReaderFromQuery(query))
        {
            while (reader.Read())
            {
                Customer customer = MapCustomer(reader);
                customers.Add(customer);
            }
        }

        return customers;
    }

    private Customer MapCustomer(SqlDataReader reader)
    {
        Address address = GetAddressFromId(Convert.ToInt32(reader["AddressId"]));

        Customer customer = new Customer(
            Convert.ToInt32(reader["Id"]),
            reader["Name"].ToString(),
            address,
            reader["Email"].ToString(),
            new customerCard(1)
            );
        return customer;
    }

    public void AddAddress(Address address)
    {
        string query = "INSERT INTO Address (Country, Region, AddressLine, PostalCode, City) VALUES (@Country, @Region, @AddressLine, @PostalCode, @City)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Country", address.country);
                command.Parameters.AddWithValue("@Region", address.region);
                command.Parameters.AddWithValue("AddressLine", address.addressLine);
                command.Parameters.AddWithValue("@PostalCode", address.postalCode);
                command.Parameters.AddWithValue("@City", address);
                
                //Get ID from database
                command.CommandText = "SELECT CAST(@@Identity as INT);";
                int id = (int)command.ExecuteScalar();
                address.id= id;
            }
        }
    }
    
    public List<Address> GetAllAddresses()
    {
        List<Address> addresses = new List<Address>();
        string query = "SELECT * FROM Address;";

        using (SqlDataReader reader = GetReaderFromQuery(query))
        {
            while (reader.Read())
            {
                Address address = MapAddress(reader);
                addresses.Add(address);
            }
        }
        return addresses;
    }

    public Address GetAddressFromId(int id)
    {
        using (SqlDataReader reader = GetReaderFromQuery($"SELECT * FROM Address WHERE Id = {id};"))
        {
            reader.Read();
            return MapAddress(reader);
        }
    }

    private Address MapAddress(SqlDataReader reader)
    {
        Address address = new Address(
            Convert.ToInt32(reader["Id"]),
            reader["Country"].ToString(),
            reader["Region"].ToString(),
            reader["City"].ToString(),
            reader["AddressLine"].ToString(),
            reader["PostalCode"].ToString()
        );
        return address;
    }
    
    public void AddItem(item item)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query =
                "INSERT INTO Item (Name, Brand, Price, Stock, Location) VALUES (@Name, @Brand, @Price, @Stock, @Location)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", item.itemName);
                command.Parameters.AddWithValue("@Brand", item.brand);
                command.Parameters.AddWithValue("@Price", item.price);
                command.Parameters.AddWithValue("@Stock", item.stock);
                command.Parameters.AddWithValue("@Location", item.location);
                command.ExecuteNonQuery();

                //Get ID from database
                command.CommandText = "SELECT CAST(@@Identity as INT);";
                int id = (int)command.ExecuteScalar();
                item.itemId = id;
            }
        }
        
    }
        
    ///<summary>
    ///Fetches all items from database.
    ///</summary>
    public List<item> GetAllItems()
    {
        List<item> items = new List<item>();
        string query = "SELECT * FROM Item;";

        using (SqlDataReader reader = GetReaderFromQuery(query))
        {
            while (reader.Read())
            {
                item item = MapItem(reader);
                items.Add(item);
            }
        }
        return items;
    }
    
    private item MapItem(SqlDataReader reader)
    {
        item item = new item(
            Convert.ToInt32(reader["Id"]),
            reader["Name"].ToString(),
            reader["Brand"].ToString(),
            Convert.ToDouble(reader["Price"]),
            Convert.ToInt32(reader["Stock"]),
            reader["Location"].ToString()
        );
        return item;
    }
    
    private SqlDataReader GetReaderFromQuery(string query)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteReader();
    }
}