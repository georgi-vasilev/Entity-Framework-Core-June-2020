namespace SalesDatabase.Data.IOManagment
{
    using System;
    using SalesDatabase.Data.IOManagment.Contracts;

    class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
