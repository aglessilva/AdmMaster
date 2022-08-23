using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAdministrator.Models
{
    [Table("STT_USUARIO", Schema = "STT_O")]
    public class Usuario
    {
        [Key]        
        [Column("CD_USUARIO", TypeName = "NUMBER(9)")]
        public int Cd_Usuario { get; set; }


        [Display(Name = "Nome")]
        [Column("NOME", TypeName = "VARCHAR2(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do usuário.")]
        public string Nome { get; set; }

        [Display(Name = "Login")]
        [Column("LOGIN", TypeName = "VARCHAR2(15)")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Column("PASSWORD ", TypeName = "VARCHAR2(100)")]
        [StringLength(15, ErrorMessage = "Deve conter pelo menos 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string Password { get; set; }
        
        [NotMapped]
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas informada não coincidem.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-mail")]
        [Column("EMAIL", TypeName = "VARCHAR2(100)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o e-mail do usuário.")]
        public string Email { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Criacao { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        [ScaffoldColumn(false)]
        public DateTime Dt_Atualizacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR2(30)")]
        [MaxLength(30)]
        [ScaffoldColumn(false)]
        public string Cd_Usuario_Atualizacao { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
