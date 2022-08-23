using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_DOCUMENTO", Schema = "STT_O")]
    public class Documento
    {
        [Key]
        [Column("CD_DOCUMENTO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Documento { get; set; }

        [Column("CD_PROCESSO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Processo { get; set; }

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

        [Display(Name = "Documento")]
        [Column("DS_DOCUMENTO", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do Documento")]
        public string Ds_Documento { get; set; }

        [Display(Name = "Tipo")]
        [Column("CD_TIPO_DOCUMENTO", TypeName = "NUMBER(9)")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Informe um tipo")]
        public int Cd_Tipo_Documento { get; set; }

        [Column("CD_FREQUENCIA_ENVIO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Frequencia_Envio { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }


    }
}
