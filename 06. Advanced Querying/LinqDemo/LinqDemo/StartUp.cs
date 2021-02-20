namespace LinqDemo
{
    using System;
    using System.Text;
    using System.Linq;
    using Z.EntityFramework.Plus;
    using Microsoft.EntityFrameworkCore;
    using LinqDemo.Models;

    public class StartUp
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var dbContext = new MusicXContext();

            //var songsToDelete = dbContext.SongMetadata
            //    .Where(x => x.Id <= 10)
            //    .Delete();

            //var songsToUpdate = dbContext.Songs
            //    .Where(x => x.Name.Contains("а") || x.Name.Contains("е"))
            //    .Update(song => new Song { Name = song.Name + "(BG)" });

            var song = dbContext.Songs
                .FirstOrDefault(x => x.Name.StartsWith("Осъдени души"));
            Console.WriteLine(song.Name);
            Console.WriteLine(song.Source.Name); // null reference exception
                                                 //how to solve it: 
                                                 //projection with anonymous object
                                                 //another 3 ways are to use lazy/eager/explicit loading
                                                 //note: use projection rather than any of these loadings

            //Demo for Explicit loading (works only on one entity) 1 navigation property = 1 object
            var songExplicitLoadingDemo = dbContext.Songs
                .FirstOrDefault(x => x.Name.StartsWith("Осъдени души"));
            dbContext.Entry(song).Reference(x => x.Source).Load(); //reference for single obj
            dbContext.Entry(song).Collection(x => x.SongMetadata).Load();//collection when it is collection of objs duh

            //Demo for Eager loading (used more often than Explicit loading)
            //It will return information for all songs not only 1 obj
            var songEagerLoadingDemo = dbContext.Songs
                .Include(x => x.Source)
                .Include(x => x.SongMetadata)
                .ToList();

            //Demo for Lazy loading
            //download nuget packege efcore.proxies then in the context after UseSqlServer() just add UseLazyLoadingProxies();
            //lazy loading creates N+1 problem


        }
    } 
}
