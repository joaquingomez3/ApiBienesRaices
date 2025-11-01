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
    public decimal precio { get; set; }
    public int superficie { get; set; }
    public decimal latitud { get; set; }
    public decimal longitud { get; set; }
    public bool disponible { get; set; }
    public string? imagen { get; set; }

    [NotMapped]
    public string? imagenLocal { get; set; }

    // Foreign Key
    public int idPropietario { get; set; }
    [ForeignKey(nameof(idPropietario))]

    public Propietarios? Propietario { get; set; }

    // Relación con alquiler
    public ICollection<Alquiler>? Alquileres { get; set; }
}