namespace LinqDemo
{
    class SongViewModel
    {
        public string Name { get; set; }

        public string SourceName { get; set; }//thanks to automapper using the convetion it joins source table and retrieves the data. 

        public string Artists { get; set; }
    }
}
