using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Avaliacao.DTO
{
    public class PedidoDTO
    {
        public ClienteDTO Cliente { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "Preencha a data do pedido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicial { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFinal { get; set; }

        [DisplayName("Descrição")]
        [MaxLength(100)]
        [StringLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres.")]
        public string Descricao { get; set; }

        [DisplayName("Nro.Pedido")]
        public Int64 Id { get; set; }

        public decimal Valor { get; set; }
    }
}
