using AO3.Domain;
using AO3.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AO3.Application
{
    public interface ILocacaoVeiculoApplication
    {
        Task<List<Veiculo>> GetVeiculo();
        bool ValidaVeiculo(string placa, DateTime dtIni);
        bool ValidaCpf(string cpf, DateTime dtIni);
        RetornoSolicitacao ReservarVeiculo(LocacaoRequest locacao);
    }
}
