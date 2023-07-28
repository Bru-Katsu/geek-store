using System.ComponentModel.DataAnnotations;

namespace GeekStore.Identity.WebAPI.ViewModels
{
    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "o Campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; }
    }
}