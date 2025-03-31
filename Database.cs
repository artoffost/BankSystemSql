using MySql.Data.MySqlClient;

class Database 
{
    private const string ConnectionString = "server=127.0.0.1;uid=root;pwd=password1;database=banksystemsql";
    public void TestConnection()
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                Console.WriteLine("Connection Success");
            }
        }
        catch
        {
            Console.WriteLine("Connection Failed");
        }
    }
    public MySqlConnection Connection = new MySqlConnection(ConnectionString);
    public MySqlDataReader Reader(
        MySqlConnection connection, 
        string query, 
        Dictionary<string, string>? keyValues = null)
    {
        try
        {
            connection.Open();
            var command = new MySqlCommand(query, connection);
            
            if (keyValues is not null)
            {
                foreach (var item in keyValues)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }

            var reader = command.ExecuteReader();
            return reader;    
                
        }
        catch (System.Exception)
        {    
            throw;
        }
    }
    public bool Command(
        string query, 
        Dictionary<string, string>? keyValues = null)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    if (keyValues is not null)
                    {
                        foreach (var item in keyValues)
                        {
                            command.Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        catch (System.Exception ex)
        {
            
            Console.WriteLine(ex);
            return false;
        }
    }
}