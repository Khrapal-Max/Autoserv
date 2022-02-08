using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.ModelsDTO.Brand
{
    public sealed class NewBrandDTO
    {
        [Required(ErrorMessage = "Proszę podać nazwę")]
        public string Name { get; set; } = string.Empty;
    }
}
