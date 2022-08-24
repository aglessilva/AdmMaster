using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_QUESTIONAMENTO", Schema = "STT_O")]
    public class Questionamento
    {
        [Key]
        [Column("CD_QUESTAO", TypeName = "NUMBER(9)")]
        public int Cd_Questao { get; set; }

        [Column("CD_PROCESSO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Processo { get; set; } = 0;

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

        [Display(Name ="Questionário")]
        [Column("DS_QUESTAO", TypeName = "VARCHAR2(500)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe um Questionamento")]
        public string Ds_Questao { get; set; }


        [Display(Name = "Tipo")]
        [Column("CD_TIPO_QUESTIONAMENTO", TypeName = "NUMBER(9)")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Informe um tipo")]
        public int Cd_Tipo_Questionamento { get; set; }

        [Display(Name = "Tipo de Demanda")]
        [Column("CD_TIPO_CONTRATO", TypeName = "NUMBER(9)")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Informe um tipo de contrato")]
        public int Cd_Tipo_Contrato { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; } = false;

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
