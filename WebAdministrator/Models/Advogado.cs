using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_ADVOGADO", Schema = "STT_O")]

    public class Advogado
    {
        [Key]
        [Column("CD_ADVOGADO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Advogado { get; set; }


        [Display(Name = "N° OAB")]
        [Column("NU_OAB", TypeName = "VARCHAR2(30)")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "O número deve conter pelo menos 6 caracteres alfanumérico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o número da AOB do Advogado")]
        [RegularExpression(@"(^\d{4,6}\/\w{2}$)", ErrorMessage = "O número da OAB deve conter entre 4 a 6 digitos e 2 caracteres que se refere a Unidade Federal.")]
        public string Nu_Oab { get; set; }
       
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

        [Display(Name = "Nome")]
        [Column("NO_ADVOGADO", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do Advogado")]
        public string No_Advogado { get; set; }

        [Display(Name = "Tipo")]
        [Column("TIPO_ADVOGADO", TypeName = "NUMBER(9)")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Informe um tipo")]
        public int Tipo_Advogado { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; } = false;

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
