using ApiOpenpayAutorizacion.Models;
using ApiOpenpayAutorizacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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

        /// <summary>
        /// Genera un número de autorización
        /// Tabla: openpay_autorizacion2
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        [HttpPost("Autorizacion")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]//Basic Auth
        public async Task<IActionResult> Autorizacion([FromBody] JsonElement ent)
        {
            int autorizacion_no = 0;
            try
            {
                var json = ent.GetRawText();
                //Grabar en bitácora el Json enviado
                log.Info("JSON en Método Autorización POST: " + json);
                AutorizacionRequest auto = JsonConvert.DeserializeObject<AutorizacionRequest>(json);
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

        /// <summary>
        /// Verifica el numero de autorización y fecha-hora no mayor a 16 minutos (hora del servidor) para poder llevar a cabo la cancelación
        /// Tabla: openpay_autorizacion2
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        [HttpDelete("Autorizacion")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]//Basic Auth        
        public async Task<IActionResult> Cancelacion([FromBody] JsonElement ent)
        {
            bool cancelo = false;
            try
            {
                //if (auto.Folio.Equals("TESTSTABC123456782"))
                //{
                var json = ent.GetRawText();
                //Grabar en bitácora el Json enviado
                log.Info("JSON en Método Autorización DELETE: " + json);
                AutorizacionRequest auto = JsonConvert.DeserializeObject<AutorizacionRequest>(json);
                cancelo = _AutorizacionService.cancelar(auto);
                //log.Info("Cancelacion exitosa.");
                if (cancelo)
                    {
                    log.Info("Cancelacion exitosa.");
                    return Ok();
                    }
                    else {
                    log.Info("Cancelacion NO exitosa.");
                    return BadRequest();
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
