namespace Customer.Data.Models.SeedModels
{
    using System.Xml.Serialization;

    public class SeedProduct
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("category")]
        public string Category { get; set; }

        [XmlElement("image_url")]
        public string Image_url { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("manufacturer")]
        public string Manufacturer { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}
