using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_COMARCA", Schema = "STT_O")]
    public class Comarca
    {
        [Key]
        [Column("CD_COMARCA", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Comarca { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Display(Name = "Sigla")]
        [Column("SG_UF", TypeName = "VARCHAR2(2)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma sigla de estado.")]
        public string Sg_Uf { get; set; }

        [Display(Name = "Cidade")]
        [Column("NO_COMARCA")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da cidade")]
        public string No_Comarca { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
