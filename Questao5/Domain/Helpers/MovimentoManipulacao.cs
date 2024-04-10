using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Helpers
{
    public class MovimentoRequest
    {
        public string IdentificacaoRequisicao { get; set; }
        public int IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
    }

    public class MovimentoResult
    {
        public bool Sucesso { get; set; }
        public int IdMovimento { get; set; }
        public TipoFalha TipoFalha { get; set; }
        public string Mensagem { get; set; }
    }
}
