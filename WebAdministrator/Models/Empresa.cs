using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_EMPRESA", Schema = "STT_O")]
    public class Empresa
    {
        [Key]
        [Column("CD_EMPRESA", TypeName = "NUMBER(9)")]
        public int Cd_Empresa { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO",TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_UsuarioAtualizacao { get; set; }

        [Display(Name = "CNPJ")]
        [Column("NU_CNPJ",TypeName ="VARCHAR2(14)")]
        [RegularExpression(@"(^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$)", ErrorMessage = "CNPJ inválidos.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o CNPJ da empresa.")]
        public string Nu_Cnpj { get; set; }

        [Display(Name = "Empresa")]
        [Column("NO_EMPRESA", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do empresa")]
        public string No_Empresa { get; set; }


        //[Display(Name = "Familia")]
        //[Column("NO_EMPRESA")]
        //[Range(typeof(int), "1", "100", ErrorMessage = "Informe uma categoria")]
        //public int Familia { get; set; }

        [Column("FG_EMPRESA_MONITORADA", TypeName = "CHAR(1)")]
        [ScaffoldColumn(false)]
        public string FgEmpresaMonitorada { get; set; } = "S";

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
