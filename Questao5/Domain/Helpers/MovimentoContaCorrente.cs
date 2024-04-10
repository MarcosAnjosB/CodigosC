namespace Questao5.Domain.Helpers
{
    public class MovimentoContaCorrente
    {
        public int numero { get; set; }
        public string nome { get; set; }
        public DateTime? datahoraconsulta { get; set; }
        public decimal valor { get; set; }
        public string tipomovimento { get; set; }
    }

    public class MovimentoContaCorrenteRetorno
    {
        public int numero { get; set; }
        public string nome { get; set; }
        public DateTime datahoraconsulta { get; set; }
        public decimal valor { get; set; }
    }
}
