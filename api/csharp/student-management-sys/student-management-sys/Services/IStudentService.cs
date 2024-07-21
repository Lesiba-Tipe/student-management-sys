using student_management_sys.Dto;
using student_management_sys.Entity;

namespace student_management_sys.Services
{
    public interface IStudentService
    {
        Task<Student> CreateAsync(Student student);
        Task<Student> FindStudentByIdAsync(string id);
        Task<IEnumerable<Student>> FindAllStudentsByAsync();
        Task<Student> FindStudentByIdNumberAsync(string idNumber);
        Task UpdateAsync(Student student);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
        //Task<Student> GetRolesAsync(string id);
    }
}
