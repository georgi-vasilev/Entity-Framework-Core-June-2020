namespace IncreaseAgedStoredProcedure
{
    using System;
    using System.Data;
    using System.Text;
    using Microsoft.Data.SqlClient;

    public class STartUp
    {
        private const string ConnectionString = @"Server=./;Database=MinionsDb;Integrated Security=true;";
        public static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection
              (ConnectionString);

            sqlConnection.Open();

            int minionId = int.Parse(Console.ReadLine());

            string result = IncreaseMinionAgeById(sqlConnection, minionId);
            Console.WriteLine(result);
        }

        private static string IncreaseMinionAgeById(SqlConnection sqlConnection, int minionId)
        {
            StringBuilder sb = new StringBuilder();

            string procedureName = "usp_GetOlder";
            using SqlCommand increaseAgeCmd = 
                new SqlCommand(procedureName, sqlConnection);
            increaseAgeCmd.CommandType = CommandType.StoredProcedure;
            increaseAgeCmd.Parameters.AddWithValue("@minionId", minionId);
            increaseAgeCmd.ExecuteNonQuery();

            string getMinionInfoQueryText = @"SELECT [Name], [Age] FROM Minions
	                                            WHERE [Id] = @minionId;";
            using SqlCommand getMinionInfoCmd =
                new SqlCommand(getMinionInfoQueryText, sqlConnection);

            getMinionInfoCmd.Parameters.AddWithValue("@minionId", minionId);

            using SqlDataReader reader = getMinionInfoCmd.ExecuteReader();
            reader.Read();

            string minionName = reader["Name"]?.ToString().TrimEnd();
            string minionAge = reader["Age"]?.ToString().TrimEnd();

            sb.AppendLine($"{minionName} - {minionAge} years old");

            return sb.ToString().TrimEnd();
        }
    }
}
