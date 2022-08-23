using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_EMPRESA_ENVOLV_ORIG", Schema = "STT_O")]
    public class Empresa_Envolvida_Origem
    {
        [Key]
        [Column("CD_EMPRESA")]
        [ScaffoldColumn(false)]
        public int Cd_Empresa { get; set; }

        [Column("DT_CRIACAO")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Column("NO_EMPRESA")]
        [ScaffoldColumn(false)]
        public string No_Empresa { get; set; }

        [Column("STATUS")]
        [ScaffoldColumn(false)]
        public bool Status { get; set; }
    }
}
