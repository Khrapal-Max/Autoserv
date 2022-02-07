using ApplicationCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public sealed class Brand : IBaseEntity
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        //public ICollection<Category> Categories { get; private set; } = new List<Category>();

        //public ICollection<Product> Products { get; private set; } = new List<Product>();

        //public ICollection<Provider> Providers { get; private set; } = new List<Provider>();

        //[Newtonsoft.Json.JsonIgnore]
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<BrandsCategories> BrandsCategories { get; private set; } = new List<BrandsCategories>();

        //[Newtonsoft.Json.JsonIgnore]
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<BrandsProviders> BrandsProviders { get; private set; } = new List<BrandsProviders>();
    }
}
