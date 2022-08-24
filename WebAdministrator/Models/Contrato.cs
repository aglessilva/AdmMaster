using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_CONTRATO", Schema = "STT_O")]
    public class Contrato
    {
        [Key]
        [Column("CD_CONTRATO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Contrato { get; set; }

        [Column("CD_PROCESSO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Processo { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Display(Name = "Contrato")]
        [Column("DS_CONTRATO", TypeName = "VARCHAR2(500)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome/descrição do contrato.")]
        public string Ds_Contrato { get; set; }

        [Column("CD_TIPO_CONTRATO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Tipo_Contrato { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
