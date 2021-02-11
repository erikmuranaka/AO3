using AO3.Application;
using AO3.Domain;
using AO3.InfraData;
using AO3.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AO3.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoApplication _veiculoApplication;


        public VeiculosController(IVeiculoApplication veiculoApplication, ILogManager logger)
        {
            _veiculoApplication = veiculoApplication;
        }

        /// <summary>
        /// Busca os veículos que estão liberados para locação a partir da data de hoje.
        /// </summary>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("GetVeiculo")]
        public ActionResult<Veiculo> GetVeiculo()
        {
            var veiculos = _veiculoApplication.GetVeiculo();

            if (veiculos == null)
            {
                return NotFound("Não foi encontrado nenhum veículo!");
            }

            return Ok(veiculos);
        }

        [DisableCors]
        [HttpPost]
        [Route("ValidaVeiculo/{placa}")]
        public bool ValidaVeiculo(string placa)
        {
            var item = _veiculoApplication.ValidaVeiculo(placa);

            return item;
        }

        [DisableCors]
        [HttpPost]
        [Route("AddVeiculo")]
        public IActionResult AddVeiculo(VeiculoRequest veiculo)
        {
            RetornoSolicitacao retorno = _veiculoApplication.AddVeiculo(veiculo);

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
