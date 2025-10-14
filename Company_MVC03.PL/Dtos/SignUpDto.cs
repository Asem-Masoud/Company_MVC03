using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "FirstName is reuqired")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is reuqired")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "UserName is reuqired")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is reuqired")]
        [EmailAddress]
        public string Email { get; set; }


        // Pa$$w0rd
        [Required(ErrorMessage = "Password is reuqired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is reuqired")]
        [Compare(nameof(Password), ErrorMessage = "Paswword is not matched")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}
