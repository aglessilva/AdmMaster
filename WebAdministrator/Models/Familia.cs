using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
     [Table("STT_GRUPO_FAMILIA", Schema = "STT_O")]
    public class Familia
    {
        [Key]
        [Column("CD_GRUPO_FAMILIA")]
        [ScaffoldColumn(false)]
        public int Cd_Grupo_Familia { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_UsuarioAtualizacao { get; set; }

        [Display(Name = "Família")]
        [Column("DS_GRUPO_FAMILIA")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da família")]
        public string Ds_Grupo_Familia { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
