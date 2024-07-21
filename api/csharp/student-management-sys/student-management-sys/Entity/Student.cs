

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace student_management_sys.Entity
{
    public class Student //: IdentityUser
    {
        [Key]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Fistname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ID number is required")]
        public string IDNumber { get; set; }
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public string Grade { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? ProfilePic { get; set; }

        public virtual Account? Account { get; set; }
    }


}
