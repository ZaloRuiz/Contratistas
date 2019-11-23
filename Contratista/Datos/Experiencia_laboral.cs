using System;
using System.Collections.Generic;
using System.Text;

namespace Contratista.Datos
{
    public class Experiencia_laboral
    {
        public int id_laboral { get; set; }
        public string cargo { get; set; }
        public string empresa { get; set; }
        public string duracion { get; set; }
        public string descripcion { get; set; }
        public int id_profesional { get; set; }
    }
}
