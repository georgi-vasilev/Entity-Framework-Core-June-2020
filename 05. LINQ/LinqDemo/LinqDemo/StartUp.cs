namespace LinqDemo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using LinqDemo.Models;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var dbContext = new MusicXContext();

            //SELECT
            //FROM Songs AS songs
            //JOIN Sources AS sources ON songs.SourceId = sources.Id
            //var joinedSongsWtihSources = dbContext.Songs.Join(
            //    dbContext.Sources,
            //    songs => songs.SourceId,
            //    sources => sources.Id,
            //    (songs, sources) => new
            //    {
            //        SongName = songs.Name,
            //        SourceName = sources.Name
            //    }
            //    ).ToList();


            var songGroups = dbContext.Songs
                .GroupBy(x => x.Name.Substring(0,1))
                .Select(x => new
                {
                    FirstLetter = x.Key,
                    Count = x.Count(),
                    FirstSong = x.Min(s => s.Name)
                })
                .ToList();

            foreach (var group in songGroups)
            {
                Console.WriteLine(group.FirstLetter + " => " + group.Count + " => " + group.FirstSong);
            }

            //var songGroups2 = dbContext.Artists
            //    .Where(a => a.Name.StartsWith("Z"))
            //    .Select(a => a.SongArtists.Select(sa => sa.Song.Name))
            //    .ToList();

            //var allSongs = new List<string>();
            //foreach (var songGroup in songGroups2)
            //{
            //    foreach (var song in songGroup)
            //    {
            //        allSongs.Add(song);
            //    }
            //}

            //the code below is the useage of selectmany
            //instead of havind List<List<T>>
            // we have 1 dimensional collection "compressed to" List<T>
            var songs = dbContext.Artists
                .Where(a => a.Name.StartsWith("Z"))
                .SelectMany(a => a.SongArtists.Select(sa => sa.Song.Name))
                .ToList();

            foreach (var song in songs)
            {
                Console.WriteLine(song);
            }
        }
    } 
}
