using FacontaryFunctions.Common;
using FacontaryFunctions.Dao.NegocioDao;
using FacontaryFunctions.Dao.UsuarioDao;
using FacontaryFunctions.Dto.Negocio;
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
                    usuarioInput.Contrasena = Encriptar(usuarioInput.Contrasena);
                    usuario = await usuarioDao.ObtenerUsuario(cmd, usuarioInput);
                    if (usuario.IdUsuario != 0) {
                        token = jWTservice.StringToken(usuario);
                    }
                }
            }
            return token;
        }

        public async Task<string> CrearUsuario(UsuarioCrearDto usuarioCrearDto)
        {
            string message = string.Empty;
            NegocioInput negocio = new NegocioInput();
            UsuarioInput usuarioExistente = new UsuarioInput();
            using (MySqlConnection mySqlConnection = await ConectMYSQL.ConnAsync())
            {
                using (MySqlCommand cmd = mySqlConnection.CreateCommand())
                {
                    try
                    {
                        UsuarioDao usuarioDao = new UsuarioDao();
                        NegocioDao negocioDao = new NegocioDao();
                        usuarioExistente = await usuarioDao.existeNicknameOEmailIgual(cmd, usuarioCrearDto);
                        if (usuarioExistente.Email == null && usuarioExistente.Usuario == null)
                        {
                            if (usuarioCrearDto.Negocio.IdNegocio != null)
                            {
                                usuarioCrearDto.Pass = Encriptar(usuarioCrearDto.Pass);
                                usuarioDao.CrearUsuario(cmd, usuarioCrearDto);
                                message = "Se creo el Usuario";
                            }
                            if (usuarioCrearDto.Negocio.Nombre != null)
                            {
                                NegocioDto negocioDto = new NegocioDto();
                                negocioDto.Nombre = usuarioCrearDto.Negocio.Nombre;
                                negocio = await negocioDao.CrearNegocio(cmd, negocioDto);
                                usuarioCrearDto.Negocio.IdNegocio = (int?)negocio.IdNegocio;
                                usuarioCrearDto.Pass = Encriptar(usuarioCrearDto.Pass);
                                usuarioDao.CrearUsuario(cmd, usuarioCrearDto);
                                message = "Se creo el Usuario";
                            }
                            return message;
                        }else
                        {
                            message = "El usuario que intenta crear ya existe";
                            return message;
                        }
                    }
                    catch
                    {
                        message = "Ocurrio un error al crear el usuario";
                        return message;
                    }
                }
            }
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
