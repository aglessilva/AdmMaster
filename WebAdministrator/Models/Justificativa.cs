using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAdministrator.Models
{
    [Table("STT_JUSTIFICATIVA_SENTENCA", Schema = "STT_O")]
    public class Justificativa
    {
        [JsonPropertyName("cd_tipo_pedido")]
        [Column("CD_TIPO_PEDIDO", TypeName = "NUMBER(9)")]
        public int Cd_Tipo_Pedido { get; set; }

        [JsonPropertyName("cd_sentenca")]
        [Column("CD_SENTENCA", TypeName = "NUMBER(9)")]
        [Display(Name = "Tipo")]
        [Range(typeof(int), "1", "100", ErrorMessage = "Selecione um tipo da justificativa.")]
        public int Cd_Sentenca { get; set; }

        [Key]
        [JsonPropertyName("cd_justificativa_sentenca")]
        [Column("CD_JUSTIFICATIVA_SENTENCA", TypeName = "NUMBER(9)")]
        [ScaffoldColumn(false)]
        public int Cd_Justificativa_Sentenca { get; set; }

        [JsonPropertyName("dt_criacao")]
        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [JsonPropertyName("cd_usuario_criacao")]
        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [JsonPropertyName("dt_atualizacao")]
        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [JsonPropertyName("ds_justificativa_sentenca")]
        [Display(Name = "Justificativa")]
        [Column("DS_JUSTIFICATIVA_SENTENCA", TypeName = "VARCHAR2(1000)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma descrição da justificativa.")]
        public string Ds_Justificativa_Sentenca { get; set; }

        [JsonPropertyName("status")]
        [Column("STATUS")]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
