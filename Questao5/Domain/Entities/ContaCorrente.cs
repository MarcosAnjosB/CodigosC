using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Questao5.Domain.Entities
{
    [DataContract(IsReference = true)]
    [Table("ContaCorrente")]
    public class ContaCorrente
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(37, ErrorMessage = "O campo deve conter no máximo 37 caracteres")]
        public string idcontacorrente { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataMember]
        [MaxLength(10, ErrorMessage = "O campo deve conter no máximo 10 caracteres")]
        public int numero { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataMember]
        [MaxLength(100, ErrorMessage = "O campo deve conter no máximo 100 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataMember]
        [MaxLength(1, ErrorMessage = "O campo deve conter no máximo 1 caracter")]
        public int ativo { get; set; }
    }
}
