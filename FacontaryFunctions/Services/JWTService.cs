using FacontaryFunctions.Dto.Usuario;
using Jose;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacontaryFunctions.Services
{
    class JWTService
    {
        byte[] secretKey = Encoding.UTF8.GetBytes("PRUW2365-sw47dccJGWp=w2xA");


        public string DecodeToken(string token)
        {
            return Jose.JWT.Decode(token, secretKey);
        }
        public string StringToken(UsuarioDto usuario)
        {
            var payload = new Dictionary<string, object>()
                {
                    { "idUsuario", usuario.IdUsuario},
                    {"idPersona", usuario.IdPersona},
                    {"idTipoUsuario", usuario.IdTipoUsuario},
                    {"idNegocio", usuario.IdNegocio},
                    {"nombre", usuario.Nombre},
                    { "expiredDate", DateTime.Now },
                };
            return Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
        }
    }
}
