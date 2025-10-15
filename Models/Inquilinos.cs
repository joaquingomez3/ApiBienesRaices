using System.ComponentModel.DataAnnotations;

public class Inquilinos
{
    [Key]
    public int idInquilino { get; set; }
    public int dni { get; set; }
    public string apellido { get; set; }
    public string nombre { get; set; }
    public string direccion { get; set; }
    public string telefono { get; set; }
}