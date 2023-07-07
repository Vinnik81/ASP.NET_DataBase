using System.ComponentModel.DataAnnotations;

namespace Authentication.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MinLength(1)]
        public string Login { get; set; }
        [Required]
        [MinLength(1)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MinLength(1)]
        [Required]
        [Compare("Password")]

        [DataType(DataType.Password)]
        public string PasswordAgain { get; set; }
    }
}
