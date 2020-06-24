namespace Customer.Data.Models.SeedModels
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("products")]
    public class SeedProducts
    {
        [XmlElement("product")]
        public List<SeedProduct> ProductsList { get; set; }
    }
}