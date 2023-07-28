using System.ComponentModel.DataAnnotations;

namespace GeekStore.Identity.WebAPI.ViewModels
{
    public class RefreshTokenRequestViewModel
    {
        [Required(ErrorMessage = "O Campo {0} � obrigat�rio")]
        public Guid RefreshToken { get; set; }
    }
}