using student_management_sys.Entity;
using System.ComponentModel.DataAnnotations;

namespace student_management_sys.Inputs
{
    public class StudentInput
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ID Number is required")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required (Enter F- Female M or A)")]
        public Gender Gender { get; set; }

        public string StudentNumber { get; set; }

        public string ProfilePic { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public string Grade { get; set; }
    }
}
