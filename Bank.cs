class Bank
{
    private readonly string _username;
    private Database database = new Database();
    public Bank (string username)
    {
        _username = username;
    }

    public void ViewOption()
    {
        Console.WriteLine("[1] View Balance\n[2] Withdraw\n[3] Deposit\n[4] Exit");
        Console.Write("Choose: ");
        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                Console.WriteLine($"Your Balance is {ViewBalance()}");
                ViewOption();
                return;
            case 2:
                Console.Write("Enter amount: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine()!);
                Withdraw(withdrawAmount);
                ViewOption();
                return;
            case 3:
                Console.Write("Enter amount: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine()!);
                Deposit(depositAmount);
                ViewOption();
                return;
            case 4:
                Console.WriteLine("Thank you for using the system");
                return;
        }
    }

    public decimal ViewBalance()
    {
        database.Connection.Close();

        string query = "SELECT amount FROM balance WHERE username = @username";
        var reader = database.Reader(database.Connection, query, new() { ["@username"] = _username });

        if (reader.Read())
        {
            return Convert.ToDecimal(reader["amount"]);
        }
        database.Connection.Close();

        return 0;

        
    }
    public void Deposit(decimal amount)
    {
        
        string query = "UPDATE balance SET amount = amount + @amount WHERE username = @username";

        var keyValues = new Dictionary<string, string>()
        {
            {"@amount", amount.ToString()},
            {"@username", _username}
        };
        if (database.Command(query, keyValues))
        {
            Console.WriteLine("Successfully deposited " + amount);
        }
        else
        {
            Console.WriteLine("Deposit Failed");
        }
    }
    public void Withdraw(decimal amount)
    {
        if (ViewBalance() < amount)
        {
            Console.WriteLine("Not Enough Balance");
            return;
        }

        string query = "UPDATE balance SET amount = amount - @amount WHERE username = @username";

        var keyValues = new Dictionary<string, string>()
        {
            {"@amount", amount.ToString()},
            {"@username", _username}
        };
        if (database.Command(query, keyValues))
        {
            Console.WriteLine("Successfully withdraw " + amount);
        }
        else
        {
            Console.WriteLine("Withdraw Failed");
        }
    }
}