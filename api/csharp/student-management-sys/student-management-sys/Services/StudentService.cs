using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using student_management_sys.Configs;
using student_management_sys.Controllers;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Repository;

namespace student_management_sys.Services
{
    public class StudentService: GenericCRUDRepository<Student> , IStudentService
    {
        private readonly StudManSysDBContext context;
        private readonly UserManager<Student> userManager;

        public StudentService(StudManSysDBContext context) :base(context) 
        {
            this.context = context;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            var results = await Insert(student);

            //Pull student's account using ID
            var account = context.Accounts.Where(acc => acc.Email == student.StudentId).FirstOrDefault();
            //Find account by ID Number

            //create account
            return results;
        }

        public async Task<Student> FindUserByIdAsync(string id)
        {
            var student = await userManager.FindByIdAsync(id);
            return student;
        }
    }
}
