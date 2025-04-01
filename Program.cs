
using MySql.Data.MySqlClient;

var database = new Database();
database.TestConnection();

Main();
void Main()
{
    Console.WriteLine("Welcome to Simple Bank Application!\n[1] Login\n[2] Signup\n[3] Exit");
    Console.Write("Choose: ");
    int choice = int.Parse(Console.ReadLine()!);

    switch (choice)
    {
        case 1:
            Login();
            break;
        case 2:
            SignUp();
            break;
        case 3:
            Console.Write("Thank you!");
            break;
    }

}

void Login()
{
    Console.WriteLine("Login");
    var user =  new User();
    Console.Write("Enter Username: ");
    user.Username = Console.ReadLine()!;


    Console.Write("Enter Password: ");
    user.Password = Console.ReadLine()!;

    string query = "SELECT password FROM Users WHERE username = @username";

    var parameters = new[]
    {
        new MySqlParameter("@username", user.Username)
    };

    database.ReadData(query, parameters, reader => {
        if (reader.Read())
        {
            string hashPassword = reader["password"].ToString()!;
            if (BCrypt.Net.BCrypt.Verify(user.Password, hashPassword))
            {
                Console.WriteLine("Login Successful");
                var bank = new Bank(user.Username);
                bank.ViewOption();
            }
            else
            {
                Console.WriteLine("Login Failed! Invalid Credentials\n");
            }
        }
        else
        {
            Console.WriteLine("cant read\n");
        }
    });

}


void SignUp()
{
    Console.WriteLine("Signup");
    var newUser =  new User();
    Console.Write("Enter Firstname: ");
    newUser.FirstName = Console.ReadLine()!;

    Console.Write("Enter LastName: ");
    newUser.LastName = Console.ReadLine()!;

    Console.Write("Enter Username: ");
    newUser.Username = Console.ReadLine()!;

    Console.Write("Enter Password: ");
    string rawPassword = Console.ReadLine()!;
    newUser.Password = BCrypt.Net.BCrypt.HashPassword(rawPassword);


    // Saving credentials to the balance table
    string userQuery = "INSERT INTO users (username, password, firstname, lastname) " +
                    "VALUES (@username, @password, @firstname, @lastname)";

    var userParameters = new[]
    {
        new MySqlParameter("@username", newUser.Username),
        new MySqlParameter("@password", newUser.Password),
        new MySqlParameter("@firstname", newUser.FirstName),
        new MySqlParameter("@lastname", newUser.LastName)
    };


    // Saving credentials to the balance table
    string balanceQuery = "INSERT INTO balance (username) VALUES (@username)";
    var balanceParameters = new[]
    {
        new MySqlParameter("@username", newUser.Username),
    };
 
    if (database.TryExecuteQuery(userQuery, userParameters) && 
        database.TryExecuteQuery(balanceQuery, balanceParameters))
    {
        Console.WriteLine($"Welcome user {newUser.Username}\n");
        Main();
    }
    else
    {
        Console.WriteLine("Failed to signup\n");
    }
}