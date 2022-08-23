using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_CENTRO_CUSTO", Schema = "STT_O")]
    public class OrigemEnvolvida
    {
        [Key]
        [Column("CD_CENTRO_CUSTO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Centro_Custo { get; set; }

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

        [Column("CD_EMPRESA_ORIGEM")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma empresa da lista suspença")]
        public int? Cd_Empresa_Origem { get; set; } = 0;

        [Column("CD_EMPRESA_ENVOLVIDA")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma empresa da lista suspença")]
        [ScaffoldColumn(false)]
        public int? Cd_Empresa_Envolvida { get; set; } = 0;

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }

        [NotMapped]
        [Display(Name = "Empresa Origem")]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "Informe a empresa de origiem")]
        public string Ds_Empresa_Origem { get; set; }

        [NotMapped]
        [Display(Name = "Empresa Envolvida")]
        //[StringLength(100, MinimumLength = 10, ErrorMessage = "Selecione uma empresa da lista suspença")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Informe a empresa envolvida")]
        public string Ds_Empresa_Envolvida { get; set; }
    }
}
