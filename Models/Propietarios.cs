using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Propietarios
{
    [Key]
    public int idPropietario { get; set; }
    public int dni { get; set; }
    public string apellido { get; set; }
    public string nombre { get; set; }
    public string telefono { get; set; }
    public string mail { get; set; }
    public string password { get; set; }
}