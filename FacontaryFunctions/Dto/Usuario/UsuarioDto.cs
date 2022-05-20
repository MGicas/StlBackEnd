using System;
using System.Collections.Generic;
using System.Text;

namespace FacontaryFunctions.Dto.Usuario
{
    class UsuarioDto 
    {
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public int IdNegocio { get; set; }
        public string Nombre { get; set; }

    }
}
