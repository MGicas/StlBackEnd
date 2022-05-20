using FacontaryFunctions.Common;
using FacontaryFunctions.Dto.Negocio;
using FacontaryFunctions.Dao.NegocioDao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacontaryFunctions.Manager.NegocioMaganer
{
    class NegocioManager
    {
        public async Task<NegocioDto> ObtenerNegocio(long idNegocio)
        {
            NegocioDto negocio = new NegocioDto();
            using (MySqlConnection mySqlConnection = await ConectMYSQL.ConnAsync())
            {
                using (MySqlCommand cmd = mySqlConnection.CreateCommand())
                {
                    NegocioDao NegocioDao = new NegocioDao();
                    negocio = await NegocioDao.ObtenerNegocio(cmd, idNegocio);
                }
            }
            return negocio;
        }
    }
}
