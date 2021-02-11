using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AO3.Models
{
    public class Locacao
    {
        public int? Id { get; set; }
        public string Placa { get; set; }
        public string Cpf { get; set; }
        public DateTime DtIni { get; set; }
        public DateTime DtFim { get; set; }
    }
}
