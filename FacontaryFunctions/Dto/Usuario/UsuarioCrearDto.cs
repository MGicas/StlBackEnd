using FacontaryFunctions.Dto.Negocio;
using FacontaryFunctions.Dto.Persona;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacontaryFunctions.Dto.Usuario
{
    class UsuarioCrearDto
    {
        public int IdUsuario { get; set; }
        public string Nickname { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public NegocioDto Negocio = new NegocioDto();

        public PersonaDto Persona = new PersonaDto();
    }
}
