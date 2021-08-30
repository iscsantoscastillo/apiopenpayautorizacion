using ApiOpenpayAutorizacion.Models;
using ApiOpenpayAutorizacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiOpenpayAutorizacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutorizacionController : ControllerBase
    {
        Logger log = LogManager.GetCurrentClassLogger();

        private IAutorizacionService _AutorizacionService;
        public AutorizacionController(IAutorizacionService autorizacionService) {
            this._AutorizacionService = autorizacionService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            log.Info("ApiOpenpayAutorizacion V. " + version);
            return new string[] { "ApiOpenpayAutorizacion V. " + version };
        }


        [HttpPost("Autorizacion")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]//Basic Auth
        public async Task<IActionResult> Autorizacion([FromBody] AutorizacionRequest auto)
        {
            int autorizacion_no = 0;
            try
            {
                if (auto.Folio.Equals("TESTSTABC123456782"))
                {
                    autorizacion_no = _AutorizacionService.autorizar(auto);
                    log.Info("Autorizacion exitosa.");
                    return Ok(new
                    {
                        response_code = 0,
                        authorization_number = autorizacion_no
                    });
                }
                else {
                    return Ok(new
                    {
                        response_code = 93,
                        authorization_number = autorizacion_no,
                        error_description = "Adquiriente inválido"
                    }) ;
                }
               
            }
            catch (Exception ex) {
                log.Error("Error inesperado. " + ex.Message);
                return NotFound(new
                {
                    response_code = 0,
                    authorization_number = autorizacion_no
                });
            }
           
        }


        [HttpDelete("Autorizacion")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]//Basic Auth
        public async Task<IActionResult> Cancelacion([FromBody] AutorizacionRequest auto)
        {
            bool cancelo = false;
            try
            {
                //if (auto.Folio.Equals("TESTSTABC123456782"))
                //{
                    cancelo = _AutorizacionService.cancelar(auto);
                log.Info("Cancelacion exitosa.");
                if (cancelo)
                    {
                        return Ok();
                    }
                    else {
                        return NotFound();
                    }
                   
                //}
                

            }
            catch (Exception ex)
            {
                log.Error("Error inesperado. " + ex.Message);
                return NotFound();
            }

        }
    }
}
