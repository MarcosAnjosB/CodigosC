using Questao5.Domain.Helpers;

namespace Questao5.Business.Contract
{
    public interface IMovimentoContaCorrenteManagement
    {
        List<MovimentoContaCorrente> ConsultarSaldo(int idContaCorrente);
        MovimentoResult ProcessarMovimento(string idRequisicao, string idcontacorrente, decimal vlmovimentacao, string tipomovimento);
    }
}
