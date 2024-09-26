using System.ComponentModel.DataAnnotations;

namespace Application.App_Management.ViewModels
{
    public class GendersViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del genero es obligatorio.")]
        [MinLength(5, ErrorMessage = "El nombre no puede tener menos de 5 caracteres.")]
        public string Name { get; set; }
    }
}