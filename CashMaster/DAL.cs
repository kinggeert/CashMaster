using System.Data.SqlClient;

namespace CashMaster;

public class DAL
{
    private readonly string connectionString = "Data Source=.;Initial Catalog=CashMaster;Integrated Security=True";

    public DAL()
    {
    }

    public void AddItem(item item)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Item (Name, Brand, Price, Stock, Location) VALUES (@Name, @Brand, @Price, @Stock, @Location)";
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
                item.itemId= id;
            }
        }
    }
    
    ///<summary>
    ///Fetches all items from database.
    ///</summary>
    public List<item> GetAllItems()
    {
        List<item> items = new List<item>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Item";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item item = MapItem(reader);
                        items.Add(item);
                    }
                }
            }
        }
        return items;
    }
    
    private SqlDataReader GetReaderFromQuery(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    return reader;
                }
            }
        }
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
    
}