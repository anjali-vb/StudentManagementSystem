using StudentRecordManagementSystem.Models.Entities;
using StudentRecordManagementSystem.Models.Interfaces;
using System.Data;

namespace StudentRecordManagementSystem.Models.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public DataTable GetAllStudents() => _repo.GetAllStudents();
        public Student? GetStudentByRoll(int rollNumber) => _repo.GetStudentByRoll(rollNumber);
        public void AddStudent(Student student) => _repo.AddStudent(student);
        public void UpdateStudent(Student student) => _repo.UpdateStudent(student);
        public void DeleteStudent(int rollNumber) => _repo.DeleteStudent(rollNumber);
        public int GenerateNextRollNumber() => _repo.GenerateNextRollNumber();
    }
}
