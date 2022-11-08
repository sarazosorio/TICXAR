using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class RegistroMaterias
    {
        [Display(Name = "Id")] public int IDMateria { get; set; }
        [DisplayName("Nombre Materia")][Required(ErrorMessage = "Este Campo Es Obligatorio")] public string? Nombre_materia { get; set; }

    }
}
