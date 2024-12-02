using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValleDeOro.Models;

public partial class Cliente
{
    public int NroDocumento { get; set; }

    [Required]
    public int IdTipoDocumento { get; set; }

    [Required]
    public string Nombres { get; set; }

    [Required]
    public string Apellidos { get; set; }

    [Required]
    public string Celular { get; set; }

    [Required]
    public string Correo { get; set; }

    [Required]
    public string Contrasena { get; set; }

    [Required]
    public bool Estado { get; set; }

    [Required]
    public int IdRol { get; set; }

    
    public virtual Role IdRolNavigation { get; set; }

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
