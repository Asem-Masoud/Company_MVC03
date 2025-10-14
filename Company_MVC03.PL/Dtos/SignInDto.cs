using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Email is reuqires")]
        [EmailAddress]
        public string Email { get; set; }


        // Pa$$w0rd
        [Required(ErrorMessage = "Password is reuqires")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
