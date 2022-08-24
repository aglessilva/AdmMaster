using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_SENTENCA", Schema = "STT_O")]
    public class Sentenca
    {
        [Key]
        [Column("CD_SENTENCA", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Sentenca { get; set; }
        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Column("DS_SENTENCA", TypeName = "VARCHAR2(40)")]
        [ScaffoldColumn(false)]
        public string Ds_Sentenca { get; set; }
    }
}
