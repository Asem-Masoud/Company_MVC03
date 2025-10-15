using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class ForgetPasswordDto
    {

        [Required(ErrorMessage = "Email is reuqired")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
