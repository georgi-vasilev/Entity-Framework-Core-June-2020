namespace FootballBetting
{
    using System.Linq;
    using FootballBetting.Data;

    public class StartUp
    {
        public static void Main()
        {
            FootballBettingContext db = new FootballBettingContext();

            var users = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    u.Email,
                    Name = u.Name == null ? "{No name}" : u.Name,
                    u.Balance
                });

            foreach (var user in users)
            {
                System.Console.WriteLine($"{user.Username} -> {user.Email} {user.Name} {user.Balance:f2}$");
            }
        }
    }
}
