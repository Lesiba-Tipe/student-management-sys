using System.Net.NetworkInformation;

namespace student_management_sys.Dto
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public  PhysicalAddress? PhysicalAddress { get; set; }

    }
}
