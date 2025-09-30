using Company_MVC03.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company_MVC03.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age must be between 18 and 65")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = " Address Must Be ......")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date of Create")]
        public DateTime CreateAt { get; set; }

        //// RelationShip
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
        //public Department Department { get; set; }

    }
}
