using FacontaryFunctions.Common;
using FacontaryFunctions.Dto.Usuario;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Dao.UsuarioDao
{
    class UsuarioDao
    {
        public async Task<UsuarioDto> ObtenerUsuario(MySqlCommand cmd, UsuarioInput usuarioInput)
        {
            ConectMYSQL.PrepareTextCmd(cmd);
            UsuarioDto usuario = new UsuarioDto();

            cmd.CommandText = @"SELECT 
                                u.ID_USUARIO as idUsuario,
                                u.NICKNAME as nickname,
                                u.ID_PERSONA as idPersona,
                                u.ID_TIPO_USUARIO as idTipoUsuario,
                                u.ID_NEGOCIO as idNegocio,
                                u.EMAIL as email
                                from USUARIO u 
                                WHERE (u.NICKNAME = @nickname or u.EMAIL = @email) and u.PASS_WORD = @contraseña;";
            cmd.Parameters.AddWithValue("@nickname", usuarioInput.Usuario);
            cmd.Parameters.AddWithValue("@email", usuarioInput.Email);
            cmd.Parameters.AddWithValue("@contraseña", usuarioInput.Contrasena);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    usuario.Email = reader["email"] == DBNull.Value ? null : Convert.ToString(reader["email"]);
                    usuario.IdPersona = reader["idPersona"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPersona"]);
                    usuario.IdTipoUsuario = reader["idTipoUsuario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTipoUsuario"]);
                    usuario.IdUsuario = reader["idUsuario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idUsuario"]);
                    usuario.IdNegocio = reader["idNegocio"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idNegocio"]);
                    usuario.Nickname = reader["nickname"] == DBNull.Value ? null : Convert.ToString(reader["nickname"]);
                }
            }

            return usuario;
        }
    }
}
