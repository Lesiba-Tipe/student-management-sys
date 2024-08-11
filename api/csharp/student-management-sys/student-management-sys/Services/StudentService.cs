using Microsoft.EntityFrameworkCore;
using student_management_sys.Configs;
using student_management_sys.Entity;
using student_management_sys.Repository;

namespace student_management_sys.Services
{
    public class StudentService: GenericCRUDRepository<Student> , IStudentService
    {
        //private readonly StudManSysDBContext context;
        //private readonly UserManager<Student> userManager;

        public StudentService(StudManSysDBContext context) :base(context) 
        {
            //this.context = context;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            student.StudentId = Guid.NewGuid().ToString();
            var results = await Insert(student);
            return results;
        }

        public async Task<Student> FindStudentByIdAsync(string id)
        {
            var student = await FindById(id);
            return student;
        }

        public async Task<Student> FindStudentByIdNumberAsync(string idNumber)
        {
            var student = await context.Students.Where(student => student.IDNumber == idNumber).FirstOrDefaultAsync();
            return student;
        }

        public async Task SaveChangesAsync()
        {
            await CompleteAsync();
        }

        public async Task UpdateAsync(Student student)
        {
             await Update(student);           
        }

        public async Task DeleteAsync(string id)
        {
            await Delete(id);
        }

        public async Task<IEnumerable<Student>> FindAllStudentsByAsync()
        {
            var students = await GetAll();

            return students;
        }
    }
}
