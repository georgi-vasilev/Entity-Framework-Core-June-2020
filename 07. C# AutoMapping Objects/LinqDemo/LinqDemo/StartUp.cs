namespace LinqDemo
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using LinqDemo.Models;

    public class StartUp
    {
        public static void Main()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Artist, ArtistWithCount>();
                cfg.CreateMap<Song, SongViewModel>()
                    .ForMember(
                        x => x.Artists, //artists referes to SongViewModel and change the default behaviour? of the property
                        opt => opt.MapFrom(x => string.Join(", ", x.SongArtists.Select(y => y.Artist.Name))))
                    .ReverseMap();// all you need in order to have reverse mapping*
            });
            IMapper mapper = config.CreateMapper();


            Console.OutputEncoding = Encoding.Unicode;
            var db = new MusicXContext();


            Artist artist = db.Artists.Where(x => x.Id == 10).FirstOrDefault();
            //ArtistWithCount artistViewModel = mapper.Map<ArtistWithCount>(artist);

            ArtistWithCount artistViewModel = db.Artists
               .Where(x => x.Id == 10)
               .ProjectTo<ArtistWithCount>(config)
               .FirstOrDefault();

            Print(artistViewModel);

            //var artists = db.Songs.Select(x => new SongViewModel
            //{
            //    Name = x.Name,
            //    Artists = string.Join(", ", x.SongArtists.Select(x => x.Artist.Name))
            //}).Take(10).ToList();

            //var songs = db.Songs
            //    .ProjectTo<SongViewModel>(config)
            //    .Take(10)
            //    .ToList();

            //var dbSong = db.Songs.Skip(9).ProjectTo<SongViewModel>(config).FirstOrDefault();

            var inputModel = new SongViewModel
            {
                Name = "test123",
                SourceName = "YouTube"
            };
            var song = mapper.Map<Song>(inputModel);//reverse mapping*

            Print(song);
        }

        public static void Print(object artists)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artists, Formatting.Indented));
        }
    } 
}
