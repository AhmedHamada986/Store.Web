using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Product;
using Store.Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Presistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        //private readonly StoreDbContext _context;

        //public DbInitializer(StoreDbContext context)
        //{
        //    _context = context;
        //}


        public async Task InitializeAsync()
        {
            // Create DB if not Create 
            // Update DataBase Migrtation 
            // Data seeding 
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) {
               await _context.Database.MigrateAsync();

            }
            // Seeding 
            if (!_context.ProductBrands.Any()) {

                // Product Brand 
                // 1. Read All Data from Json File brands.json 
                // C:\Users\GEEKS\source\repos\Store.Web\Infrastructure\Store.Presistence\Data\DataSeeding\brands.json
                var branddata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Presistence\Data\DataSeeding\brands.json");
                // Conver Json String to List <product Brand >
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(branddata);
                // 3. Add List to the data base 
                if (brands is not null && brands.Count > 0)
                {

                    await _context.ProductBrands.AddRangeAsync(brands);
                }

            }


            // Product Type 
            if (!_context.ProductTypes.Any()) {

                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Presistence\Data\DataSeeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);
                if (types is not null && types.Count > 0) {

                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

            // Product 
            if (!_context.Products.Any()) {

                var productdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Presistence\Data\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productdata);
                if (products is not null && products.Count > 0) {

                    await _context.Products.AddRangeAsync(products);
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
