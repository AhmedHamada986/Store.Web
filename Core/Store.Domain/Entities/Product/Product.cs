using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Product
{
    public class Product: BaseEntity <int>   
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicturalUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }//FK
        public ProductBrand Brand { get; set; }
        public int TypeId { get; set; }//Fk 
        public ProductType Type { get; set; }
    }
}
