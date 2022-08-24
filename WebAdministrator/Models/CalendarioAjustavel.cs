using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_CALENDARIO_AJUSTAVEL", Schema = "STT_O")]
    public class CalendarioAjustavel
    {

        [Key]
        [Column("CD_CALENDARIO", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Calendario { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        [StringLength(30)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        [StringLength(30)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [Display(Name = "Data")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe ums data.")]
        [Column("DT_NACIONAL", TypeName = "VARCHAR2(10)")]
        public string Dt_Nacional { get; set; }

        [Display(Name = "Sigla")]
        [Column("SG_UF", TypeName = "VARCHAR2(2)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma sigla de estado.")]
        public string Sg_Uf { get; set; }

        [Display(Name = "Descrição")]
        [Column("DS_TITULO_DATA", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe um título para essa data.")]
        public string Ds_Titulo_Data { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
