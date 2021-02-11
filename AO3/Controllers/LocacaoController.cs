using AO3.Application;
using AO3.Domain;
using AO3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AO3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoVeiculoApplication _locacaoVeiculoApplication;

        public LocacaoController(ILocacaoVeiculoApplication locacaoVeiculoApplication)
        {
            _locacaoVeiculoApplication = locacaoVeiculoApplication;
        }

        /// <summary>
        /// Busca os veículos que estão liberados para locação a partir da data de hoje.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVeiculo")]
        public async Task<ActionResult<Veiculo>> GetVeiculo()
        {
            var veiculos = await _locacaoVeiculoApplication.GetVeiculo();

            if (veiculos == null)
            {
                return NotFound("Todos os veiculos estão alugados");
            }

            return Ok(veiculos);
        }

        /// <summary>
        /// Valida se o Veículo está locado no mesmo período solicitado.
        /// </summary>
        /// <param name="placa">Informe a placa do veículo</param>
        /// <param name="dtIni">Data Inicial da Locação</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidaVeiculo/{placa}/{dtIni}")]
        public bool ValidaVeiculo(string placa, DateTime dtIni)
        {
            var item = _locacaoVeiculoApplication.ValidaVeiculo(placa, dtIni);

            return item;
        }

        /// <summary>
        /// Valida se o CPF informado já está com um carro locado no mesmo período solicitado.
        /// </summary>
        /// <param name="cpf">Obs: Adicionar a máscara do CPF</param>
        /// <param name="dtIni">Data Inicial da Locação</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidaCpf/{cpf}/{dtIni}")]
        public bool ValidaCpf(string cpf, DateTime dtIni)
        {
            var item = _locacaoVeiculoApplication.ValidaCpf(cpf, dtIni);

            return item;
        }

        /// <summary>
        /// Realiza a solicitação de reserva do veículo.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// {
        ///   "placa": "string",
        ///   "cpf": "string", (Adicionar a máscara do CPF)
        ///   "dtIni": "2021-02-10T13:36:02.487Z",
        ///   "dtFim": "2021-02-10T13:36:02.487Z"
        /// }
        ///
        /// </remarks>
        /// <param name="locacao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReservarVeiculo")]
        public IActionResult ReservarVeiculo(LocacaoRequest locacao)
        {

            RetornoSolicitacao retorno = _locacaoVeiculoApplication.ReservarVeiculo(locacao);

            if (retorno.Sucesso)
            {
                return new OkObjectResult(retorno);
            }
            else
            {
                return new UnprocessableEntityObjectResult(retorno);
            }
        }
    }
}
