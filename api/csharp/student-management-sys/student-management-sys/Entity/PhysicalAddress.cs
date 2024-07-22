using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace student_management_sys.Entity
{
    public class PhysicalAddress
    {
        [Key]
        public string Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }


        //public virtual Parent Parent { get; set; } // Required navigation property
    }
}
