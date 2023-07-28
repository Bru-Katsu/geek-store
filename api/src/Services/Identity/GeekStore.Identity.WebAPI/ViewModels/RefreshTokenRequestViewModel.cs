using System.ComponentModel.DataAnnotations;

namespace GeekStore.Identity.WebAPI.ViewModels
{
    public class RefreshTokenRequestViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public Guid RefreshToken { get; set; }
    }
}