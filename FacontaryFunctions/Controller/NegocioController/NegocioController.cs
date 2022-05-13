using FacontaryFunctions.Common;
using FacontaryFunctions.Dto;
using FacontaryFunctions.Dto.Negocio;
using FacontaryFunctions.Dto.Negocio;
using FacontaryFunctions.Manager.NegocioMaganer;
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

namespace FacontaryFunctions.Controller.NegocioController
{
    class NegocioController
    {
        private static readonly IContractResolver BaseFirstResolver = new BaseFirstContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        };

        [FunctionName("ObtenerNegocio")]
        public static async Task<IActionResult> ObtenerNegocio(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] NegocioInput negocioInput,
        ILogger log)
        {
            try
            {
                log.LogInformation($"Ejecucion ObtenerNegocio");
                NegocioManager NegocioMaganer = new NegocioManager();
                NegocioDto negocio = await NegocioMaganer.ObtenerNegocio(negocioInput.IdNegocio);
                log.LogInformation($"Obtuve negocio {negocioInput.IdNegocio}");
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("version", new { idVersion = 1, fecha = DateTime.Now });
                dictionary.Add("negocio", negocio);

                ContratoServiciosRest contrato = new ContratoServiciosRest("OK", null, dictionary);
                log.LogInformation($"Cree el contrato {negocioInput.IdNegocio}");

                return new JsonResult(contrato, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,

                    ContractResolver = BaseFirstResolver,
                });

            }
            catch (Exception e)
            {
                return ReturnError(e, "ObtenerNegocio", log);
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
