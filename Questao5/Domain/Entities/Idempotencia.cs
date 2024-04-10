using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Questao5.Domain.Entities
{
    [DataContract(IsReference = true)]
    [Table("Idempotencia")]
    public class Idempotencia
    {
        [Key]
        [DataMember]
        [MaxLength(37, ErrorMessage = "O campo deve conter no máximo 37 caracter")]
        public string chave_idempotencia { get; set; }

        [DataMember]
        [MaxLength(1000, ErrorMessage = "O campo deve conter no máximo 1000 caracter")]
        public string requisicao { get; set; }

        [DataMember]
        [MaxLength(1000, ErrorMessage = "O campo deve conter no máximo 1000 caracter")]
        public string resultado { get; set; }
    }
}
