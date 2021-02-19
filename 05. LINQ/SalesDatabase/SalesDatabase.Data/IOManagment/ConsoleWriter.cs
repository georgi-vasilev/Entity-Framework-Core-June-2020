namespace SalesDatabase.Data.IOManagment
{
    using System;
    using SalesDatabase.Data.IOManagment.Contracts;

    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
