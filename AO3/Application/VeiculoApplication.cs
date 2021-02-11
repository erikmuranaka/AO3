using AO3.Domain;
using AO3.InfraData;
using AO3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AO3.Application
{
    public class VeiculoApplication: IVeiculoApplication
    {
        private readonly VeiculoDbContext _context;
        private readonly ILogManager logger;
        private readonly LogModel logModel;

        public VeiculoApplication(VeiculoDbContext context, ILogManager logger)
        {
            _context = context;
            this.logger = logger;
            this.logger.Configure(this.GetType().FullName);
            logModel = new LogModel();
        }

        public List<Veiculo> GetVeiculo()
        {
            RetornoSolicitacao retornoSolicitacao = new RetornoSolicitacao();

            var veiculos = _context.Veiculos.OrderBy(c => c.Modelo).ToList();

            return veiculos;
        }

        public bool ValidaVeiculo(string placa)
        {
            var item = _context.Veiculos.Where(c => c.Placa == placa).Count();

            return (item > 0);
        }

        public RetornoSolicitacao AddVeiculo(VeiculoRequest veiculo)
        {
            logModel.InPut = veiculo;
            logModel.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            logModel.Message = "Iniciando para adicionar o veiculo";
            logger.LogInfo(logModel);

            logModel.Clean();

            RetornoSolicitacao retornoSolicitacao = new RetornoSolicitacao();

            if (veiculo == null)
            {
                retornoSolicitacao.Mensagem = "Não foi informados dados para inclusão do veículo.";
                
                logModel.Message = retornoSolicitacao.Mensagem;
                logger.LogInfo(logModel);

                return retornoSolicitacao;
            }

            if (ValidaVeiculo(veiculo.Placa))
            {
                retornoSolicitacao.Mensagem = "Veiculo com a placa " + veiculo.Placa + " já existe.";

                logModel.Message = retornoSolicitacao.Mensagem;
                logger.LogInfo(logModel);

                return retornoSolicitacao;
            }

            try
            {
                Veiculo item = new Veiculo();

                item.Modelo = veiculo.Modelo;
                item.Marca = veiculo.Marca;
                item.Placa = veiculo.Placa;
                item.AnoModelo = veiculo.AnoModelo;
                item.AnoFabricacao = veiculo.AnoFabricacao;

                _context.Veiculos.Add(item);

                _context.SaveChanges();

                retornoSolicitacao.Sucesso = true;
                retornoSolicitacao.Mensagem = "Veiculo cadastrado com sucesso!";
            }
            catch (Exception e)
            {
                retornoSolicitacao.Sucesso = false;
                retornoSolicitacao.Mensagem = e.Message;
            }

            logModel.Message = retornoSolicitacao.Mensagem;
            logger.LogInfo(logModel);

            return retornoSolicitacao;
        }
    }
}
