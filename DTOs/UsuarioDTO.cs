using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mvpApi.DTOs
{
    public class UsuarioDTO
    {
        [NotMapped]
        public int? IdUsuario { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Nome do usuário obrigatório")]
        public string nome { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "cpf / cnpj obrigatório")]
        public string cpf_cnpj { get; set; }        
        [NotMapped]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string email { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Senha obrigatória")]
        public string senha { get; set; }
    }
}
