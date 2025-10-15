using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is reuqired")]
        [DataType(DataType.Password)]
        public string NwePassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is reuqired")]
        [Compare(nameof(NwePassword), ErrorMessage = "Paswword is not matched")]
        public string ConfirmPassword { get; set; }
    }
}
