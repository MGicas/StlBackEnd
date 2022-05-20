using FacontaryFunctions.Common;
using FacontaryFunctions.Dto.Negocio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Dao.NegocioDao
{
    class NegocioDao
    {
        public async Task<NegocioDto> ObtenerNegocio(MySqlCommand cmd, long idNegocio)
        {
            ConectMYSQL.PrepareTextCmd(cmd);
            NegocioDto negocio = new NegocioDto();

            cmd.CommandText = @"SELECT 
                                n.ID_NEGOCIO as idNegocio,
                                n.DESCRIPCION as nombre
                                FROM NEGOCIO n 
                                WHERE n.ID_NEGOCIO = @idNegocio";
            cmd.Parameters.AddWithValue("@idNegocio", idNegocio);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    negocio.IdNegocio = Convert.ToInt32(reader["idNegocio"]);
                    negocio.Nombre = Convert.ToString(reader["nombre"]);
                }
            }

            return negocio;
        }

        public Task<NegocioInput> CrearNegocio(MySqlCommand cmd, NegocioDto negocio)
        {
            NegocioInput negocioRes = new NegocioInput();
            ConectMYSQL.PrepareTextCmd(cmd);
            string sql = @"insert into NEGOCIO (DESCRIPCION)
                            values(@descripcion);";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@descripcion", negocio.Nombre);
            cmd.ExecuteNonQuery();
            long lastIdNegocio = cmd.LastInsertedId;
            negocioRes.IdNegocio = lastIdNegocio;
            return Task.FromResult(negocioRes);
        }
    }
}
