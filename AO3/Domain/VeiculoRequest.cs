using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AO3.Domain
{
    public class VeiculoRequest
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int AnoModelo { get; set; }
        public int AnoFabricacao { get; set; }
    }
}
