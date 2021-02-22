namespace LinqDemo
{
    using System;

    class ArtistWithCount
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int SongArtistsCount { get; set; }
    }
}
