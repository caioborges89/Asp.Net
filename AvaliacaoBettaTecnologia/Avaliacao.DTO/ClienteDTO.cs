using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Avaliacao.Enum;
using System.ComponentModel.DataAnnotations;

namespace Avaliacao.DTO
{
    /// <summary>
    /// Classe onde estão todos os objetos referentes ao Cliente.
    /// </summary>
    public class ClienteDTO
    {   
        
        public Int64 Id { get; set; }

        [MaxLength(100)]
        [StringLength(100,ErrorMessage = "Tamanho máximo de 100 caracteres.") ]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o telefone")]
        [DisplayFormat(DataFormatString = "{0:(##)#####-####}")]        
        public string Telefone { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres.")]
        public string Email { get; set; }

        public bool Removido { get; set; }

        public TipoCliente TipoCliente { get; set;}

        public int IdTipoCliente { get; set; }

        [DisplayName("Tipo de Pessoa")]
        public string FisicaJuridica { get; set; }
        
    }
}
