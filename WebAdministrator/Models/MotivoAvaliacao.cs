using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_MOTIVO_AVALIACAO", Schema = "STT_O")]
    public class MotivoAvaliacao
    {
        [Key]
        [Column("CD_MODTIVO_AVALIACAO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Motivo_Avaliacao { get; set; }

        [Display(Name = "Motivo")]
        [Column("DS_MOTIVO")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo é obrigatório.")]
        public string Ds_Motivo { get; set; }

        [Display(Name = "TIPO")]
        [Column("TIPO")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Informe um tipo.")]
        public int Tipo { get; set; }

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

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
