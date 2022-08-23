using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Keyless]
    [Table("STT_DOMINIO_GENERICO", Schema = "STT_O")]
    public class DominioGenerico
    {
        [Column("NO_DOMINIO_GENERICO", TypeName = "VARCHAR2(50)")]
        [ScaffoldColumn(false)]
        public string No_Dominio_Generico { get; set; }

        [Column("CD_DOMINIO_GENERICO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Dominio_Generico { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; }

        [Column("DS_DOMINIO_GENERICO", TypeName = "VARCHAR2(200)")]
        [ScaffoldColumn(false)]
        public string Ds_Dominio_Generico { get; set; }
    }
}
