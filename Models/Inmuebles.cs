using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmuebles
{
    [Key]
    public int idInmueble { get; set; }
    public string direccion { get; set; }
    public int ambientes { get; set; }
    public string tipo { get; set; }
    public string uso { get; set; }
    public double precio { get; set; }
    public bool disponible { get; set; }

    // Foreign Key
    public int idPropietario { get; set; }
    [ForeignKey(nameof(idPropietario))]
    public Propietarios? Propietario { get; set; }
}