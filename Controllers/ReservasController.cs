using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using ValleDeOro.Models;
using ValleDeOro.Models.VistaModelo;


namespace ValleDeOro.Controllers
{
    public class ReservasController : Controller
    {
        private readonly GvglampingContext _context;

        public ReservasController(GvglampingContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var gvglampingContext = _context.Reservas.Include(r => r.IdEstadoReservaNavigation).Include(r => r.MetodoPagoNavigation).Include(r => r.NroDocumentoClienteNavigation);
            return View(await gvglampingContext.ToListAsync());
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // GET: Reservas/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var reserva = await _context.Reservas
        //        .Include(r => r.IdEstadoReservaNavigation)
        //        .Include(r => r.MetodoPagoNavigation)
        //        .Include(r => r.NroDocumentoClienteNavigation)
        //        .FirstOrDefaultAsync(m => m.IdReserva == id);
        //    if (reserva == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(reserva);
        //}

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var reserva = await _context.Reservas
            //    .Include(r => r.IdEstadoReservaNavigation)
            //    .Include(r => r.MetodoPagoNavigation)
            //    .Include(r => r.NroDocumentoClienteNavigation)
            //    .Include(r => r.DetalleReservaPaquetes).ThenInclude(dp => dp.IdPaqueteNavigation.IdPaquete)
            //    .Include(r => r.DetalleReservaServicios).ThenInclude(ds => ds.IdServicioNavigation.IdServicio)
            //    .FirstOrDefaultAsync(m => m.IdReserva == id);
            var reserva = await _context.Reservas
                .Include(r => r.IdEstadoReservaNavigation)
                .Include(r => r.MetodoPagoNavigation)
                .Include(r => r.NroDocumentoClienteNavigation)
                .Include(r => r.DetalleReservaPaquetes).ThenInclude(ds => ds.IdPaquete)
                .Include(r => r.DetalleReservaServicios).ThenInclude(ds => ds.IdServicioNavigation.IdServicio)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: Reservas/Create
        //// public IActionResult Create()
        //// {
        ////     ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva");
        ////     ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago");
        ////     ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento");
        ////     ViewData["IdPaquete"] = new SelectList(_context.Paquetes, "IdPaquete", "IdPaquete");
        ////     ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "IdServicio");
        ////     return View();
        //// }

        //// POST: Reservas/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        ////[HttpPost]
        //// [ValidateAntiForgeryToken]


        //// public async Task<IActionResult> Create([Bind("IdReserva,NroDocumentoCliente,FechaReserva,FechaInicio,FechaFinalizacion,Iva,MontoTotal,MetodoPago,IdEstadoReserva")] Reserva reserva)
        //// {
        ////     if (ModelState.IsValid)
        ////     {
        ////         _context.Add(reserva);
        ////         await _context.SaveChangesAsync();
        ////         return RedirectToAction(nameof(Index));
        ////     }
        ////     ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdEstadoReserva);
        ////     ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "NomMetodoPago", "NomMetodoPago", reserva.MetodoPagoNavigation.NomMetodoPago);
        ////     ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento", reserva.NroDocumentoCliente);
        ////     return View(reserva);
        //// }

        //// POST: Reservas/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "Nombres");
            ViewData["MetodoPagoDisponible"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "NomMetodoPago");
            ViewData["PaquetesDisponibles"] = new SelectList(_context.Paquetes, "IdPaquete", "NomPaquete");
            ViewData["ServiciosDisponibles"] = new SelectList(_context.Servicios, "IdServicio", "NomServicio");
            ViewData["EstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "NombreEstadoReserva");

            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva, int[] serviciosSeleccionados, int[] paquetesSeleccionados)
        {
            if (ModelState.IsValid)
            {
                // Agregar fecha de reserva
                reserva.FechaReserva = DateTime.Now;

                // Guardar la reserva
                _context.Add(reserva);
                await _context.SaveChangesAsync();

                // Relacionar servicios seleccionados con la reserva
                foreach (var idServicio in serviciosSeleccionados)
                {
                    var detalleServicio = new DetalleReservaServicio
                    {
                        IdReserva = reserva.IdReserva,
                        IdServicio = idServicio,
                        Cantidad = 1 // Puedes ajustar esto según tu lógica de negocio
                    };
                    _context.DetalleReservaServicios.Add(detalleServicio);
                }

                // Relacionar paquetes seleccionados con la reserva
                foreach (var idPaquete in paquetesSeleccionados)
                {
                    var detallePaquete = new DetalleReservaPaquete
                    {
                        IdReserva = reserva.IdReserva,
                        IdPaquete = idPaquete,
                        Cantidad = 1 // Ajusta según sea necesario
                    };
                    _context.DetalleReservaPaquetes.Add(detallePaquete);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Cargar datos nuevamente en caso de error
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "Nombres", reserva.NroDocumentoCliente);
            ViewData["MetodoPagoDisponible"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "NomMetodoPago", reserva.MetodoPago);
            ViewData["EstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "NombreEstadoReserva", reserva.IdEstadoReserva);

            return View(reserva);
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public Task<IActionResult> Creation([Bind("IdReserva,NroDocumentoCliente,FechaReserva,FechaInicio,FechaFinalizacion,Iva,MontoTotal,MetodoPago,IdEstadoReserva,IdServicio")] Reserva oReserva, string paqueteSeleccionado, string serviciosSeleccionados)
        {

            ViewBag.PaquetesDisponibles = _context.Paquetes.Where(s => s.Estado == true)
                .ToList(); ;
            //ViewBag.ServiciosDisponibles = _context.Servicios.Where(s => s.Estado == true && (s.IdServicio != 1 && s.IdServicio != 2 && s.IdServicio != 3))
            //    .ToList();
            ViewBag.ServiciosDisponibles = _context.Servicios.Where(s => s.Estado == true)
                .ToList();
            ViewBag.MetodoPagoDisponible = _context.MetodoPagos.Where(s => s.NomMetodoPago != null && s.NomMetodoPago.Any());
            ViewData["Error"] = "True";

            if (string.IsNullOrEmpty(paqueteSeleccionado))
            {
                ModelState.AddModelError("paqueteSeleccionados", "Seleccione un paquete");
                //return View(CargarDatosIniciales());
                return Task.FromResult<IActionResult>(View());
            }

            if (string.IsNullOrEmpty(serviciosSeleccionados) || serviciosSeleccionados == "[]")
            {
                ViewData["ErrorServicio"] = "True";
                //return View(CargarDatosIniciales());
                return Task.FromResult<IActionResult>(View());
            }

            if (!ModelState.IsValid)
            {
                //return View(CargarDatosIniciales());
                return Task.FromResult<IActionResult>(View());
            }

            //if (!Existe((int)oReserva.NroDocumentoCliente))
            //{
            //    ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente no existe");
            //    return View(CargarDatosIniciales());
            //    //return View();
            //}

            var cliente = _context.Clientes.FirstOrDefault(c => c.NroDocumento == oReserva.NroDocumentoCliente);


            //if (cliente.Estado == false)
            //{
            //    ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente esta inhabilitado");
            //    //return View(CargarDatosIniciales());
            //    return View();
            //}

            //if (cliente.Confirmado == false)
            //{
            //    ModelState.AddModelError("oReserva.NroDocumentoCliente", "El cliente no ha confirmado su correo");
            //    //return View(CargarDatosIniciales());
            //}

            //if (!ValidarFechas(oReserva))
            //{
            //    //return View(CargarDatosIniciales());
            //}

            //if (oReserva.Descuento == null)
            //{
            //    oReserva.Descuento = 0;
            //}

            _context.Reservas.Add(oReserva);
            _context.SaveChanges();

            var listaPaqueteSeleccionado = JsonConvert.DeserializeObject<List<dynamic>>(paqueteSeleccionado.ToString());

            if (listaPaqueteSeleccionado != null && listaPaqueteSeleccionado.Any())
            {
                var paquetes = listaPaqueteSeleccionado.Select(paquete => new Paquete
                {
                    IdPaquete = Convert.ToInt32(paquete.Id),
                    Costo = Convert.ToDouble(paquete.costo)
                }).ToList();

                foreach (var paquete in paquetes)
                {
                    var DetalleReservaPaquete = new DetalleReservaPaquete
                    {
                        IdReserva = oReserva.IdReserva,
                        IdPaquete = paquete.IdPaquete,
                        Costo = paquete.Costo
                    };
                    _context.DetalleReservaPaquetes.Add(DetalleReservaPaquete);
                }
            }

            if (!string.IsNullOrEmpty(serviciosSeleccionados))
            {
                var listaServiciosSeleccionados = JsonConvert.DeserializeObject<List<dynamic>>(serviciosSeleccionados.ToString());

                if (listaServiciosSeleccionados != null && listaServiciosSeleccionados.Any())
                {
                    var servicios = listaServiciosSeleccionados.Select(servicio => new Servicio
                    {
                        IdServicio = Convert.ToInt32(servicio.id),
                        NomServicio = servicio.nombre.ToString(),
                        Costo = Convert.ToDouble(servicio.costo)
                    }).ToList();

                    for (int i = 0; i < listaServiciosSeleccionados.Count; i++)
                    {
                        if (listaServiciosSeleccionados[i].cantidad == null)
                        {
                            listaServiciosSeleccionados[i].cantidad = 1;
                        }
                        var DetalleReservaServicio = new DetalleReservaServicio
                        {
                            IdReserva = oReserva.IdReserva,
                            IdServicio = listaServiciosSeleccionados[i].id,
                            Costo = listaServiciosSeleccionados[i].costo,
                            Cantidad = listaServiciosSeleccionados[i].cantidad
                        };
                        _context.DetalleReservaServicios.Add(DetalleReservaServicio);
                    }
                }
            }

            _context.SaveChanges();
            return Task.FromResult<IActionResult>(RedirectToAction("Index", "Reservas"));

        }

        private VMReserva CargarDatosIniciales()
        {
            VMReserva ReservaVM = new VMReserva()
            {
                vmReserva = new Reserva(),
                vmListadoMethodPay = _context.MetodoPagos.Select(reserva => new SelectListItem()
                {
                    Text = reserva.NomMetodoPago,
                    Value = reserva.IdMetodoPago.ToString()
                }).ToList()
            };
            return ReservaVM;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool Existe(int cliente)
        {
            return _context.Clientes.Any(yup => yup.NroDocumento == cliente);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdEstadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento", reserva.NroDocumentoCliente);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,NroDocumentoCliente,FechaReserva,FechaInicio,FechaFinalizacion,Iva,MontoTotal,MetodoPago,IdEstadoReserva")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdEstadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento", reserva.NroDocumentoCliente);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdEstadoReservaNavigation)
                .Include(r => r.MetodoPagoNavigation)
                .Include(r => r.NroDocumentoClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
