using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.ModelsDTO.Brand
{

    public sealed class BrandDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę wpisać prawidłowe imię")]
        public string Name { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            return obj is BrandDTO dTO &&
                   Id == dTO.Id &&
                   Name == dTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
