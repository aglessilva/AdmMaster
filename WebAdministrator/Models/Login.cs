using System.ComponentModel.DataAnnotations;

namespace WebAdministrator.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio.")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obrigatorio.")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
