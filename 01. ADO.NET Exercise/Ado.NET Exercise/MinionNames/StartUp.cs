namespace ADO.NETExercise
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Text;

    public class StartUp
    {
        private const string ConnectionString = @"Server=./;Database=MinionsDb;Integrated Security=true;";

        public static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection
                (ConnectionString);

            sqlConnection.Open();

            int VillainId = int.Parse(Console.ReadLine());

            string result = GetMinionsInfoAboutVillain(sqlConnection, VillainId);

            Console.WriteLine(result);
        }

        private static string GetMinionsInfoAboutVillain(SqlConnection sqlConnection, int VillainId)
        {
            StringBuilder sb = new StringBuilder();
            string VillainName = GetVillainName(sqlConnection, VillainId);


            if (VillainName == null)
            {
                sb.AppendLine($"No Villain with ID {VillainId} exists in the database");
            }
            else
            {
                sb.AppendLine($"Villain: {VillainName}");

                string getMinionsInfoQueryText =
                                @"SELECT m.[Name], m.[Age] FROM Villains AS v
                                       LEFT JOIN MinionsVillains AS mv ON v.[Id] = mv.[VillainId]
                                       LEFT JOIN Minions AS m ON mv.[MinionId] = m.[Id]
                                       WHERE v.[Name] = @VillainName
                                       ORDER BY m.[Name]";

                using SqlCommand getMinionsInfoCommand =
                        new SqlCommand(getMinionsInfoQueryText, sqlConnection);
                getMinionsInfoCommand.Parameters.AddWithValue(@"VillainName", VillainName);

                using SqlDataReader reader = getMinionsInfoCommand
                    .ExecuteReader();

                if (reader.HasRows)
                {
                    int rowNum = 1;

                    while (reader.Read())
                    {
                        string minionName = reader["Name"]?.ToString();
                        string minionAge = reader["Age"]?.ToString();

                        sb.AppendLine($"{rowNum}. {minionName} {minionAge}");

                        if (minionName == "" && minionAge == "")
                        {
                            sb.AppendLine("(no minions)");
                        }

                        rowNum++;
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        private static string GetVillainName(SqlConnection sqlConnection, int VillainId)
        {
            string getVillainNameQueryText = @"SELECT [Name] FROM Villains
                                                WHERE Id = @VillainId";
            using SqlCommand getVillainNameCommand = new SqlCommand
                               (getVillainNameQueryText, sqlConnection);
            getVillainNameCommand.Parameters.AddWithValue(@"VillainId", VillainId);

            string VillainName = getVillainNameCommand
                .ExecuteScalar()? //return object, the ? is used in case the function returns null
                .ToString();

            return VillainName;
        }
    }
}
