namespace EfDemo
{
    using System;
    using System.Linq;
    using EfDemo.Models;

    public class StartUp
    {
        public static void Main()
        {
            var db = new MusicXContext();

            var artists = db.Artists
                .Where(x => x.Name.StartsWith("D"))
                .Take(20)
                .OrderBy(x => x.SongArtists.Count())
                .ThenBy(x => x.Name)
                .ToList();

            foreach (var artist in artists)
            {
                Console.WriteLine($"{artist.Name}");
            }
        }
    }
}
