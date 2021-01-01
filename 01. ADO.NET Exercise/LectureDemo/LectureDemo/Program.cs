using Microsoft.Data.SqlClient;
using System;

namespace LectureDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // .
            // localhost
            // 127.0.0.1
            // DESKTOP-.... (pc name0
            // all 4 of these are equivalent
            string connectionString = "Server=./;Database=SoftUni;Integrated Security=true";
            //if we want to use credentials we use - User ID= ... and Password=...
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string command = "SELECT [FirstName], [LastName], [Salary] FROM Employees WHERE [FirstName] LIKE 'N%'";
                var sqlCommand = new SqlCommand(command, sqlConnection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        decimal salary = (decimal)reader["Salary"];
                        Console.WriteLine(firstName + " " + lastName + " => " + salary);
                    }
                }
                 

                command = "UPDATE Employees SET [Salary] = [Salary] * 1.10";
                SqlCommand updateSalaryCommand = new SqlCommand(command, sqlConnection);
                var updatedRows = updateSalaryCommand.ExecuteNonQuery();
                Console.WriteLine($"Salary updated for {updatedRows} employees");

                var reader2 = sqlCommand.ExecuteReader();
                using (reader2)
                {
                    while (reader2.Read())
                    {
                        string firstName = (string)reader2["FirstName"];
                        string lastName = (string)reader2["LastName"];
                        decimal salary = (decimal)reader2["Salary"];
                        Console.WriteLine(firstName + " " + lastName + " => " + salary);
                    }
                }
            }
        }
    }
}
