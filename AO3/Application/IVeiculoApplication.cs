using AO3.Domain;
using AO3.Models;
using System.Collections.Generic;

namespace AO3.Application
{
    public interface IVeiculoApplication
    {
        List<Veiculo> GetVeiculo();

        bool ValidaVeiculo(string placa);

        RetornoSolicitacao AddVeiculo(VeiculoRequest veiculo);
    }
}
