using FacontaryFunctions.Common;
using FacontaryFunctions.Dao.UsuarioDao;
using FacontaryFunctions.Dto.Usuario;
using FacontaryFunctions.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Manager.UsuarioManager
{
    class UsuarioManager
    {
        public async Task<string> ObtenerUsuario(UsuarioInput usuarioInput)
        {
            UsuarioDto usuario = new UsuarioDto();
            var jWTservice = new JWTService();
            string token = string.Empty;
            using (MySqlConnection mySqlConnection = await ConectMYSQL.ConnAsync())
            {
                using (MySqlCommand cmd = mySqlConnection.CreateCommand())
                {
                    UsuarioDao usuarioDao = new UsuarioDao();
                    usuarioInput.Contraseña = Encriptar(usuarioInput.Contraseña);
                    usuario = await usuarioDao.ObtenerUsuario(cmd, usuarioInput);
                    if (usuario.IdUsuario != 0) {
                        token = jWTservice.StringToken(usuario);
                    }
                }
            }
            return token;
        }

        /// Encripta una cadena
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
