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
                                u.ID_PERSONA as idPersona,
                                u.ID_TIPO_USUARIO as idTipoUsuario,
                                u.ID_NEGOCIO as idNegocio,
                                replace(concat_ws('',
                                if(p.PRIMER_NOMBRE is not null, concat(trim(p.PRIMER_NOMBRE), ' '), ''),
                                if(p.PRIMER_APELLIDO is not null, concat(trim(p.PRIMER_APELLIDO), ' ') , '')), '..', ' ') as nombre
                                from USUARIO u 
                                join PERSONA p on p.ID_PERSONA = u.ID_PERSONA 
                                WHERE (u.NICKNAME = @nickname or u.EMAIL = @email) and u.PASS_WORD = @contraseña;";
            cmd.Parameters.AddWithValue("@nickname", usuarioInput.Usuario);
            cmd.Parameters.AddWithValue("@email", usuarioInput.Email);
            cmd.Parameters.AddWithValue("@contraseña", usuarioInput.Contrasena);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    usuario.IdPersona = reader["idPersona"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPersona"]);
                    usuario.IdTipoUsuario = reader["idTipoUsuario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idTipoUsuario"]);
                    usuario.IdUsuario = reader["idUsuario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idUsuario"]);
                    usuario.IdNegocio = reader["idNegocio"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idNegocio"]);
                    usuario.Nombre = Convert.ToString(reader["nombre"]);
                }
            }

            return usuario;
        }

        public void CrearUsuario(MySqlCommand cmd, UsuarioCrearDto usuarioCrearDto)
        {
            ConectMYSQL.PrepareTextCmd(cmd);
            string sql = @"insert into PERSONA (PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, DOCUMENTO)
                            values (@primerNombre, @segundoNombre, @primerApellido, @segundoApellido, @documento);";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@primerNombre", usuarioCrearDto.Persona.PrimerNombre);
            cmd.Parameters.AddWithValue("@segundoNombre", usuarioCrearDto.Persona.SegundoNombre);
            cmd.Parameters.AddWithValue("@primerApellido", usuarioCrearDto.Persona.PrimerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", usuarioCrearDto.Persona.SegundoApellido);
            cmd.Parameters.AddWithValue("@documento", usuarioCrearDto.Persona.Documento);
            cmd.ExecuteNonQuery();
            long lastIdPersona = cmd.LastInsertedId;

            ConectMYSQL.PrepareTextCmd(cmd);
            sql = @"insert into USUARIO (NICKNAME, PASS_WORD, ID_PERSONA, ID_TIPO_USUARIO, ID_NEGOCIO, EMAIL)
                            values (@nickname, @pass, @idPersona, @idTipoPersona, @idNegocio, @email);";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@nickname", usuarioCrearDto.Nickname);
            cmd.Parameters.AddWithValue("@pass", usuarioCrearDto.Pass);
            cmd.Parameters.AddWithValue("@idPersona", lastIdPersona);
            cmd.Parameters.AddWithValue("@idTipoPersona", usuarioCrearDto.IdTipoUsuario);
            cmd.Parameters.AddWithValue("@idNegocio", usuarioCrearDto.Negocio.IdNegocio);
            cmd.Parameters.AddWithValue("@email", usuarioCrearDto.Email);
            cmd.ExecuteNonQuery();
        }

        public async Task<UsuarioInput> existeNicknameOEmailIgual(MySqlCommand cmd, UsuarioCrearDto usuarioCrearDto)
        {
            UsuarioInput usuarioExistente = new UsuarioInput();
            ConectMYSQL.PrepareTextCmd(cmd);
            string sql = @"select
                            u.NICKNAME as nickname,
                            u.EMAIL as email
                            from USUARIO u
                            where u.NICKNAME = @nickname or u.EMAIL = @email";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@nickname", usuarioCrearDto.Nickname);
            cmd.Parameters.AddWithValue("@email", usuarioCrearDto.Email);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    usuarioExistente.Usuario = Convert.ToString(reader["nickname"]);
                    usuarioExistente.Email = Convert.ToString(reader["email"]);
                }
            }

            return usuarioExistente;
        }
    }
}
