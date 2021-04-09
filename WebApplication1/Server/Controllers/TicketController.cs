using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Server.Reportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {

        [HttpGet("RepMes/{titulo}")]
        [AllowAnonymous]
        public IActionResult GetRepMes(string titulo)
        {
           //////////var titulo = "Prueba 3";
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            date.AddMonths(-1);

            //Asi obtenemos el primer dia del mes actual
            DateTime fechaI = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime fechaF = fechaI.AddMonths(1);

            var bytes16 = new Byte[16];
            var Guid0 = new Guid(bytes16);
            byte[] salida = new byte[0];
            
            salida = GenerarTickets.repMes(titulo);
            return File(salida, "application/pdf");
        }

    }
}
