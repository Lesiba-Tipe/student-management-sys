using student_management_sys.Entity;

namespace student_management_sys.Dto
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string StudentNumber { get; set; }
        public string ProfilePic { get; set; }
        public string Grade { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
