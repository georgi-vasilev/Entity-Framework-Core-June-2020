namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarsDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartsDto[] Parts { get; set; }
    }
    [XmlType("partId")]
    public class PartsDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
