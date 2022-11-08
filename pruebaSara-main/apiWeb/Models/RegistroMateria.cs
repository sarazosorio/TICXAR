using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace apiWeb.Models
{
    public class RegistroMateria
    {
        [Display(Name = "Id")] public int IDMateria { get; set; }
        [DisplayName("Nombre Materia")][Required(ErrorMessage = "Este Campo Es Obligatorio")]public string? Nombre_materia { get; set; }
       

    }
}
