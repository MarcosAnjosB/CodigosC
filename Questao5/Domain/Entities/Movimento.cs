using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Questao5.Domain.Entities
{
    [DataContract(IsReference = true)]
    [Table("Movimento")]
    public class Movimento
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(37, ErrorMessage = "O campo deve conter no máximo 37 caracter")]
        public string idmovimento { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [ForeignKey("ContaCorrente")]
        [DataMember]
        [MaxLength(37, ErrorMessage = "O campo deve conter no máximo 37 caracter")]
        public string idcontacorrente { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataMember]
        [MaxLength(25, ErrorMessage = "O campo deve conter no máximo 25 caracter")]
        public string datamovimento { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataMember]
        [MaxLength(1, ErrorMessage = "O campo deve conter no máximo 1 caracter")]
        public string tipomovimento { get; set; }

        [Required(ErrorMessage = "O campo Saldo é obrigatório")]
        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public virtual ContaCorrente ContaCorrente { get; set; }
    }
}
