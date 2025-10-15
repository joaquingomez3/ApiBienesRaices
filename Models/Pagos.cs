using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

public class Pagos
{
    [Key]
    public int idPago { get; set; }
    public int nroPago { get; set; }
    public DateTime fecha { get; set; }
    public double importe { get; set; }

    // Foreign Key
    public int idAlquiler { get; set; }
    [ForeignKey(nameof(idAlquiler))]
    public Alquiler? Alquiler { get; set; }
}