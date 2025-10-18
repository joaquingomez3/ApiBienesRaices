using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Text.Json.Serialization;

public class Alquiler
{
    [Key]
    public int idAlquiler { get; set; }
    public double precio { get; set; }
    public DateTime fecha_inicio { get; set; }
    public DateTime fechaFin { get; set; }

    // Foreign Keys
    public int idInquilino { get; set; }
    [ForeignKey(nameof(idInquilino))]
    public Inquilinos? Inquilino { get; set; }

    public int idInmueble { get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(idInmueble))]
    public Inmuebles? Inmueble { get; set; }
    public ICollection<Pagos> pagos { get; set; } // Colección de pagos asociados
}