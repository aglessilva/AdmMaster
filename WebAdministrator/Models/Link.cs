using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_URL_SISTEMA", Schema = "STT_O")]
    public class Link
    {

        [Key]
        [Display(Name = "Sistema")]
        [Column("CD_SISTEMA", TypeName = "NUMBER(9)")]
        public int Cd_Sistema { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Display(Name = "Sistema")]
        [Column("NO_SISTEMA", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do sistema.")]
        public string No_Sistema { get; set; }

        [Display(Name = "https://")]
        [Column("TX_URL_SISTEMA", TypeName = "VARCHAR2(400)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o Link do sistema.")]
        [Url(ErrorMessage = "Verifique se o link do sistema foi digirado corretamente.")]
        public string Tx_Url_Sistema { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }

    }
}
