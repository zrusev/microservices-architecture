namespace Customer.Web.Infrastructure
{
    using Customer.Data;
    using Customer.Data.Models;
    using Customer.Data.Models.SeedModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    [ObsoleteAttribute("Custom xlm seeder is replaced by a sql file migration")]
    public static class ApplicationBuilderExtensions
    {
        private const string DATAPATH = @"../Customer.Data/products.xml";

        public static IApplicationBuilder UseDataSeed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                var db = serviceProvider.GetRequiredService<CustomerDbContext>();

                if (!File.Exists(DATAPATH))
                {
                    return app;
                }

                SeedProducts products = ReadData();
                
                if (!db.Categories.Any())
                {
                    foreach (var category in GetCategories(products))
                    {
                        db.Categories.Add(new Category { Name = category, Description = "Category description about " + category });
                    }
                }

                if (!db.Manufacturers.Any())
                {
                    foreach (var manifacturer in GetManifacturers(products))
                    {
                        db.Manufacturers.Add(new Manufacturer { Name = manifacturer });
                    }
                }

                if (!db.Products.Any())
                {
                    var categories = db.Categories.ToList();
                    var manufacturers = db.Manufacturers.ToList();

                    foreach (var product in products.ProductsList.Where(c => c.Category != string.Empty).Where(m => m.Manufacturer != string.Empty))
                    {
                        var cID = categories.Where(c => c.Name == product.Category).Select(c => c.Id).FirstOrDefault();
                        var mID = manufacturers.Where(m => m.Name == product.Manufacturer).Select(m => m.Id).FirstOrDefault();

                        db.Products.Add(new Product
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Price = decimal.Parse(product.Price),
                            Url = product.Url,
                            Image_url = product.Image_url,
                            CategoryId = cID,
                            ManufacturerId = mID
                        });
                    }
                }

                db.SaveChanges();
            }

            return app;
        }

        private static SeedProducts ReadData()
        {
            FileStream xmlStream = new FileStream(DATAPATH, FileMode.Open);
            XmlSerializer xml = new XmlSerializer(typeof(SeedProducts));
            var products = (SeedProducts)xml.Deserialize(xmlStream);

            foreach (var product in products.ProductsList)
            {
                var ind = product.Category.IndexOf('>');

                product.Category = product.Category.Remove(0, ind > -1 ? ind + 2 : 0);
            }

            return products;
        }

        private static List<string> GetCategories(SeedProducts products)
            => products
                .ProductsList
                .GroupBy(c => c.Category)
                .Where(c => c.Key != string.Empty)
                .Select(c => c.Key)
                .ToList();

        private static List<string> GetManifacturers(SeedProducts products)
            => products
                .ProductsList
                .GroupBy(c => c.Manufacturer)
                .Where(c => c.Key != string.Empty)
                .Select(c => c.Key)
                .ToList();
    }
}