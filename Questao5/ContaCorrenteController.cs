using Microsoft.AspNetCore.Mvc;
using Questao5.Business.Contract;
using Questao5.Business.Implementation;
using Questao5.Domain.Entities;
using Questao5.Domain.Helpers;

namespace Questao5
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController
    {
        private readonly IMovimentoContaCorrenteManagement _ContaCorrente;

        public ContaCorrenteController(IMovimentoContaCorrenteManagement ContaCorrente)
        {
            _ContaCorrente = ContaCorrente;
        }

        [HttpGet("{numeroConta}")]
        public IActionResult ConsultarSaldo(int numeroConta)
        {
            try
            {
                decimal valorSaldo = 0;
                MovimentoContaCorrenteRetorno retornoConta = new MovimentoContaCorrenteRetorno();

                //Acessa método para realizar busca
                var contaMovimento = _ContaCorrente.ConsultarSaldo(numeroConta);

                if (contaMovimento == null)
                {
                    // Se os dados estiverem inconsistentes, retorna um erro HTTP 400
                    return new BadRequestObjectResult(new { mensagem = "Ocorreu um erro ao tentar buscar a conta! Tente novamente" });
                }

                //Faz os cálculos quando há débitos ou créditos
                foreach (var conta in contaMovimento)
                {
                    retornoConta.numero = conta.numero;
                    retornoConta.nome = conta.nome;
                    if (conta.tipomovimento == "C")
                    {
                        valorSaldo = valorSaldo + conta.valor;
                    }
                    else if (conta.tipomovimento == "D")
                    {
                        valorSaldo = valorSaldo - conta.valor;
                    }
                    else
                    {
                        valorSaldo = 0;
                    }
                }
                retornoConta.datahoraconsulta = DateTime.Now;
                retornoConta.valor = valorSaldo;

                //Processamento bem sucedido
                return new OkObjectResult(retornoConta);
            }
            catch (Exception ex)
            {
                // Capturar a exceção de argumento nulo e retornar um BadRequest
                return new BadRequestObjectResult(new { Tipo = "Falha", Mensagem = "Falha no processamento, tent novamente mais tarde!" });
            }
        }

        [HttpPost]
        public IActionResult MovimentarConta(string idRequisicao, string idcontacorrente, decimal vlmovimentacao, string tipomovimento)
        {
            try
            {
                // Chamar a lógica de serviço para processar a requisição
                var result = _ContaCorrente.ProcessarMovimento(idRequisicao, idcontacorrente, vlmovimentacao, tipomovimento);

                // Verificar se houve sucesso ou falha no processamento
                if (result.Sucesso)
                {
                    // Se o processamento foi bem-sucedido, retornar HTTP 200 OK
                    return new OkObjectResult(new { });
                }
                else
                {
                    // Se houve falha no processamento, retornar HTTP 400 Bad Request
                    return new BadRequestObjectResult(new { Tipo = result.TipoFalha, Mensagem = result.Mensagem });
                }
            }
            catch (Exception ex)
            {
                // Capturar a exceção de argumento nulo e retornar um BadRequest
                return new BadRequestObjectResult(new { Tipo = "Falha", Mensagem = "Falha no processamento, tent novamente mais tarde!" });
            }
        }
    }
}

