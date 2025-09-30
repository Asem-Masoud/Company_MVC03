using Company_MVC03.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class CreateDepartmentDto
    {
        //V10
        [Required(ErrorMessage = "Code is Required !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CreateAt is Required !")]
        public DateTime CreateAt { get; set; }


        /// <summary>
        /// ///////
        /// </summary>
        //public List<Employee> Employees { get; set; }

    }
}
