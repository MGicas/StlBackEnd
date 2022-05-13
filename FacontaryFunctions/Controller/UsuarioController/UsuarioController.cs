using FacontaryFunctions.Common;
using FacontaryFunctions.Dto;
using FacontaryFunctions.Dto.Usuario;
using FacontaryFunctions.Manager.UsuarioManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Controller.UsuarioController
{
    class UsuarioController
    {
        private static readonly IContractResolver BaseFirstResolver = new BaseFirstContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        };

        [FunctionName("login")]
        public static async Task<IActionResult> ObtenerUsuario(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] UsuarioInput usuarioInput,
        ILogger log)
        {
            try
            {
                log.LogInformation($"Ejecucion login");
                UsuarioManager usuarioManager = new UsuarioManager();
                string token = await usuarioManager.ObtenerUsuario(usuarioInput);
                log.LogInformation($"Obtuve Usuario {(usuarioInput.Usuario == null ? usuarioInput.Email : usuarioInput.Usuario)}");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("version", new { idVersion = 1, fecha = DateTime.Now });
                dictionary.Add("token", token);

                ContratoServiciosRest contrato = new ContratoServiciosRest("OK", null, dictionary);
                log.LogInformation($"Cree el contrato {(usuarioInput.Usuario == null ? usuarioInput.Email : usuarioInput.Usuario)}");

                return new JsonResult(contrato, new JsonSerializerSettings()
                {
                    //NullValueHandling = NullValueHandling.Ignore,

                    ContractResolver = BaseFirstResolver,
                });

            }
            catch (Exception e)
            {
                return ReturnError(e, "ObtenerUsuario", log);
            }
        }

        private static IActionResult ReturnError(Exception e, String metodo, ILogger log)
        {
            log.LogError(e, $"error executing method {metodo}");
            ContratoServiciosRest contrato = new ContratoServiciosRest("ERROR", e, null);
            JsonResult result = new JsonResult(contrato, new JsonSerializerSettings()
            {
                ContractResolver = BaseFirstResolver,
            });

            result.StatusCode = StatusCodes.Status500InternalServerError; ;
            return result;
        }
    }
}
