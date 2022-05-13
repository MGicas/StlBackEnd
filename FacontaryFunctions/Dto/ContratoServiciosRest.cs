using System;
using System.Collections.Generic;
using System.Text;

namespace FacontaryFunctions.Dto
{
    class ContratoServiciosRest
    {
        public ContratoServiciosRest(string estado, Exception e, Object data)
        {
            Estado = estado;
            E = e;
            Data = data;
        }

        public string Estado { get; set; }
        public Exception E { get; set; }
        public Object Data { get; set; }
    }
}
