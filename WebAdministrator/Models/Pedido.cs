using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace WebAdministrator.Models
{
    [Table("STT_TIPO_PEDIDO", Schema = "STT_O")]
    public class Pedido
    {
        [Key]
        [Column("CD_TIPO_PEDIDO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Tipo_Pedido { get; set; }

        [Column("CD_PROCESSO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Processo { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        [StringLength(30)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        [StringLength(30)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Column("DS_TIPO_PEDIDO", TypeName = "VARCHAR2(100)")]
        [Display(Name = "Descrição do Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma descrição para o pedido.")]
        public string Ds_Tipo_Pedido { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
