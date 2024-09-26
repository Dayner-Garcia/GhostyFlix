using System.ComponentModel.DataAnnotations;

namespace Application.App_Management.ViewModels
{
    public class ProducersViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Productor es obligatorio.")]
        [MinLength(5, ErrorMessage = "El nombre del Productor no puede ser mayor a 5 caracteres.")]
        public string Name { get; set; }
    }
}