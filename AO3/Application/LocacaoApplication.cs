using AO3.Domain;
using AO3.InfraData;
using AO3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace AO3.Application
{
    public class LocacaoApplication: ILocacaoVeiculoApplication
    {
        private readonly LocacaoDbContext _contextLocacao;
        private readonly VeiculoDbContext _contextVeiculo;
        private readonly IVeiculoApplication _veiculoApplication;
        private readonly ILogManager logger;
        private readonly LogModel logModel;

        public LocacaoApplication(LocacaoDbContext contextLocacao, VeiculoDbContext contextVeiculo, IVeiculoApplication veiculoApplication, ILogManager logger)
        {
            _contextLocacao = contextLocacao;
            _contextVeiculo = contextVeiculo;
            _veiculoApplication = veiculoApplication;
            this.logger = logger;
            this.logger.Configure(this.GetType().FullName);
            logModel = new LogModel();
        }

        public async Task<List<Veiculo>> GetVeiculo()
        {
            RetornoSolicitacao retornoSolicitacao = new RetornoSolicitacao();

            List<string> locacaoPlaca = _contextLocacao.LocacaoVeiculo.Where(y => y.DtFim >= DateTime.Now).Select(x => x.Placa).ToList();

            var veiculos = await _contextVeiculo.Veiculos.Where(p => !locacaoPlaca.Contains(p.Placa)).ToListAsync();

            return veiculos;
        }

        public bool ValidaVeiculo(string placa, DateTime dtIni)
        {
            var item = _contextLocacao.LocacaoVeiculo.Where(y => y.Placa == placa && y.DtFim >= dtIni).Count();

            return (item > 0);
        }

        public bool ValidaCpf(string cpf, DateTime dtIni)
        {
            var item = _contextLocacao.LocacaoVeiculo.Where(y => y.Cpf == cpf && y.DtFim >= dtIni).Count();

            return (item > 0);
        }

        public RetornoSolicitacao ReservarVeiculo(LocacaoRequest locacao)
        {
            logModel.InPut = locacao;
            logModel.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            logModel.Message = "Iniciando Reservar veiculo";
            logger.LogInfo(logModel);

            logModel.Clean();

            RetornoSolicitacao retornoSolicitacao = new RetornoSolicitacao();

            var veiculo = _veiculoApplication.ValidaVeiculo(locacao.Placa);

            if (!veiculo)
            {
                retornoSolicitacao.Sucesso = true;
                retornoSolicitacao.Mensagem = "Veiculo informado não existe para locação!";

                logModel.Message = retornoSolicitacao.Mensagem;
                logger.LogInfo(logModel);

                return retornoSolicitacao;
            }

            if (ValidaCpf(locacao.Cpf, locacao.DtIni))
            {
                retornoSolicitacao.Sucesso = true;
                retornoSolicitacao.Mensagem = "Já existe uma locação para o CPF informado!";

                logModel.Message = retornoSolicitacao.Mensagem;
                logger.LogInfo(logModel);

                return retornoSolicitacao;
            }


            if (ValidaVeiculo(locacao.Placa, locacao.DtIni))
            {
                retornoSolicitacao.Sucesso = true;
                retornoSolicitacao.Mensagem = "Veiculo informado já esta locado!";

                logModel.Message = retornoSolicitacao.Mensagem;
                logger.LogInfo(logModel);

                return retornoSolicitacao;
            }

            try
            {
                Locacao item = new Locacao();

                item.Cpf = locacao.Cpf;
                item.Placa = locacao.Placa;
                item.DtIni = locacao.DtIni;
                item.DtFim = locacao.DtFim;

                _contextLocacao.LocacaoVeiculo.Add(item);

                _contextLocacao.SaveChanges();

                retornoSolicitacao.Sucesso = true;
                retornoSolicitacao.Mensagem = "Veiculo locado com sucesso!";
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
