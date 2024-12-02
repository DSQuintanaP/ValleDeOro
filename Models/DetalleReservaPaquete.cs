using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValleDeOro.Models;
public partial class DetalleReservaPaquete
{
    public int DetalleReservaPaquete1 { get; set; }

    [Required]
    public int IdPaquete { get; set; }

    [Required]
    public int IdReserva { get; set; }

    [Required]
    public int? Cantidad { get; set; }

    [Required]
    public decimal? Costo { get; set; }

    public virtual Paquete IdPaqueteNavigation { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; }
}