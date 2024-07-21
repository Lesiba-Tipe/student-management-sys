using Microsoft.AspNetCore.Identity;
using student_management_sys.Inputs;
using System.ComponentModel.DataAnnotations;

namespace student_management_sys.Entity
{
    public class Account : IdentityUser
    {
        //public string Id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string LastName { get; set; }
        public string? IdNumber { get; set; }    //Optional


        public string? StudentId { get; set; } // Nullable foreign key
        public virtual Student? Student { get; set; }

        //public int? ParentId { get; set; } // Nullable foreign key
        //public virtual Parent Parent { get; set; }

    }
}
