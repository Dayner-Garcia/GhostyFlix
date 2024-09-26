using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.App_Management.ViewModels
{
    public class SeriesViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la serie es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El link de la imagen de portada es obligatoria.")]
        [Url(ErrorMessage = "Debe ser un URL valido. ")]
        public string CoverImage { get; set; }

        [Required(ErrorMessage = "El link del video es obligatorio.")]
        [Url(ErrorMessage = "Debe ser un URL valido.")]
        public string LinkVideo { get; set; }

        [Required(ErrorMessage = "El productor de la serie es obligatorio.")]
        public int ProducerId { get; set; }

        public List<SelectListItem>? Producers { get; set; }
        public string? ProducerName { get; set; }

        [Required(ErrorMessage = "El genero primario es obligatorio.")]
        public int PrimaryGenreId { get; set; }

        public List<SelectListItem>? PrimaryGenres { get; set; }
        public string? PrimaryGenreName { get; set; }
        public int? SecondaryGenreId { get; set; }
        public List<SelectListItem>? SecondaryGenres { get; set; }
        public string? SecondaryGenreName { get; set; }
    }
}