using System.ComponentModel.DataAnnotations;

namespace student_management_sys.Entity
{
    public class Parent
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public string ParentId { get; set; }

        [Required(ErrorMessage = "Fistname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        public string ContactDetails { get; set; }

        //public PhysicalAddress Address { get; set; }

    }
}
