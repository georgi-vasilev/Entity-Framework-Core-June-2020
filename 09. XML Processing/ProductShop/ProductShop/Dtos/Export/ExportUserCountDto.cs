namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class ExportUserCountDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUserDto[] Users { get; set; }
    }

    [XmlType("User")]
    public class ExportUserDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public ExportProductCountDto SoldProduct { get; set; }
    }

    public class ExportProductCountDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("Products")]
        public ExportProductDto[] Products { get; set; }
    }

    [XmlType("Products")]
    public class ExportProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
