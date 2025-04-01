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
    public void ReadData(
        string query, 
        MySqlParameter[] parameters, 
        Action<MySqlDataReader> reader)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(query, connection))
            {
                if (parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (var executeReader = command.ExecuteReader())
                {
                    
                    reader(executeReader);
                    
                }
            }
        }
    }
    public bool TryExecuteQuery(string query, MySqlParameter[] parameters)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        
    }
}