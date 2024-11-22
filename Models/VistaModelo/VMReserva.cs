using Microsoft.AspNetCore.Mvc.Rendering;

namespace ValleDeOro.Models.VistaModelo
{
    public class VMReserva
    {
        public Reserva vmReserva { get; set; }
        public List<SelectListItem> vmListState { get; set; }
        public List<SelectListItem> vmListadoMethodPay { get; set; }
    }
}
