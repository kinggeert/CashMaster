using System.Data.SqlClient;

namespace CashMaster;

public class Dal
{
    private const string ConnectionString = "Data Source=.;Initial Catalog=CashMaster;Integrated Security=True";

    public static void AddCustomer(Customer customer)
    {
        const string query = "INSERT INTO Customer (Name, AddressId, Email, CustomerCardId) VALUES (@Name, @AddressId, @Email, @CustomerCardId)";
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", customer.Name);
        command.Parameters.AddWithValue("@AddressId", customer.Address.Id);
        command.Parameters.AddWithValue("@Email", customer.Email);
                
        //Get ID from database
        command.CommandText = "SELECT CAST(@@Identity as INT);";
        var id = (int)command.ExecuteScalar();
        customer.Id= id;
    }
    
    public List<Customer> GetAllCustomers()
    {
        List<Customer> customers = [];
        const string query = "SELECT * FROM Customer";

        using SqlDataReader reader = GetReaderFromQuery(query);
        while (reader.Read())
        {
            Customer customer = MapCustomer(reader);
            customers.Add(customer);
        }
        
        return customers;
    }

    public Customer GetCustomerFromId(int id)
    {
        using SqlDataReader reader = GetReaderFromQuery($"SELECT * FROM Customer WHERE Id = {id}");
        reader.Read();
        return MapCustomer(reader);
    }

    private Customer MapCustomer(SqlDataReader reader)
    {
        Address address = GetAddressFromId(Convert.ToInt32(reader["AddressId"]));

        Customer customer = new Customer(
            Convert.ToInt32(reader["Id"]),
            reader["Name"].ToString(),
            address,
            reader["Email"].ToString(),
            new CustomerCard(1)
            );
        
        return customer;
    }

    public static void AddAddress(Address address)
    {
        const string query = "INSERT INTO Address (Country, Region, AddressLine, PostalCode, City) VALUES (@Country, @Region, @AddressLine, @PostalCode, @City)";
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Country", address.Country);
        command.Parameters.AddWithValue("@Region", address.Region);
        command.Parameters.AddWithValue("AddressLine", address.AddressLine);
        command.Parameters.AddWithValue("@PostalCode", address.PostalCode);
        command.Parameters.AddWithValue("@City", address);
                
        //Get ID from database
        command.CommandText = "SELECT CAST(@@Identity as INT);";
        var id = (int)command.ExecuteScalar();
        address.Id= id;
    }
    
    public List<Address> GetAllAddresses()
    {
        List<Address> addresses = [];
        const string query = "SELECT * FROM Address;";

        using SqlDataReader reader = GetReaderFromQuery(query);
        while (reader.Read())
        {
            Address address = MapAddress(reader);
            addresses.Add(address);
        }

        return addresses;
    }

    private Address GetAddressFromId(int id)
    {
        using SqlDataReader reader = GetReaderFromQuery($"SELECT * FROM Address WHERE Id = {id};");
        reader.Read();
        return MapAddress(reader);
    }

    private static Address MapAddress(SqlDataReader reader)
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
    
    public static void AddItem(Item item)
    {
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        
        const string query = "INSERT INTO Item (Name, Brand, Price, Stock, Location) VALUES (@Name, @Brand, @Price, @Stock, @Location)";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", item.Name);
        command.Parameters.AddWithValue("@Brand", item.Brand);
        command.Parameters.AddWithValue("@Price", item.Price);
        command.Parameters.AddWithValue("@Stock", item.Stock);
        command.Parameters.AddWithValue("@Location", item.Location);
        command.ExecuteNonQuery();

        //Get ID from database
        command.CommandText = "SELECT CAST(@@Identity as INT);";
        var id = (int)command.ExecuteScalar();
        item.Id = id;
    }

    public static void AddOrder(Order order)
    {
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        
        const string query = "INSERT INTO [Order] (CustomerId, OrderComplete) VALUES (@CustomerId, @OrderComplete)";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CustomerId", order.Customer.Id);
        command.Parameters.AddWithValue("@OrderComplete", order.OrderComplete);
        command.ExecuteNonQuery();
                
        //Get ID from database
        command.CommandText = "SELECT CAST(@@Identity as INT);";
        var id = (int)command.ExecuteScalar();
        order.Id = id;
    }

    public static void AddOrderLine(OrderLine orderLine, Order order)
    {
        using SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        
        const string query = "INSERT INTO OrderLine (OrderId, ItemId, Quantity) VALUES (@OrderId, @ItemId, @Quantity)";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@OrderId", order.Id);
        command.Parameters.AddWithValue("@ItemId", orderLine.Item.Id);
        command.Parameters.AddWithValue("Quantity", orderLine.Quantity);
        command.ExecuteNonQuery();
    }
        
    ///<summary>
    ///Fetches all items from database.
    ///</summary>
    public List<Item> GetAllItems()
    {
        List<Item> items = [];
        const string query = "SELECT * FROM Item;";

        using SqlDataReader reader = GetReaderFromQuery(query);
        while (reader.Read())
        {
            Item item = MapItem(reader);
            items.Add(item);
        }

        return items;
    }

    public Item GetItemFromId(int id)
    {
        using SqlDataReader reader = GetReaderFromQuery($"SELECT * FROM Item WHERE Id = {id};");
        reader.Read();
        return MapItem(reader);
    }
    
    private static Item MapItem(SqlDataReader reader)
    {
        Item item = new Item(
            Convert.ToInt32(reader["Id"]),
            reader["Name"].ToString(),
            reader["Brand"].ToString(),
            Convert.ToDouble(reader["Price"]),
            Convert.ToInt32(reader["Stock"]),
            reader["Location"].ToString()
        );
        return item;
    }

    public List<Order> GetAllOrders()
    {
        List<Order> orders = [];

        using SqlDataReader reader = GetReaderFromQuery("SELECT * FROM [Order]");
        while (reader.Read())
        {
            Order order = MapOrder(reader);
            orders.Add(order);
        }

        return orders;
    }

    private Order MapOrder(SqlDataReader reader)
    {
        List<OrderLine> orderLines = GetOrderLinesFromOrder(Convert.ToInt32(reader["Id"]));
        Customer customer = GetCustomerFromId(Convert.ToInt32(reader["CustomerId"]));
        Order order = new Order(
            Convert.ToInt32(reader["Id"]),
            orderLines,
            customer,
            Convert.ToBoolean(reader["OrderComplete"])
        );
        return order;
    }

    private List<OrderLine> GetOrderLinesFromOrder(int id)
    {
        List<OrderLine> orderLines = [];
        using SqlDataReader reader = GetReaderFromQuery($"SELECT * FROM OrderLine WHERE OrderId = {id}");
        while (reader.Read())
        {
            OrderLine orderLine = MapOrderLine(reader);
            orderLines.Add(orderLine);
        }

        return orderLines;
    }

    private OrderLine MapOrderLine(SqlDataReader reader)
    {
        Item item = GetItemFromId(Convert.ToInt32(reader["ItemId"]));
        OrderLine orderLine = new OrderLine(item, Convert.ToInt32(reader["Quantity"]));
        return orderLine;
    }
    
    private SqlDataReader GetReaderFromQuery(string query)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(query, connection);
        return command.ExecuteReader();
    }
}