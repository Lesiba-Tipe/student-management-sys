using student_management_sys.Entity;

namespace student_management_sys.Services
{
    public interface IStudentService
    {
        Task<Student> CreateAsync(Student student);
    }
}
