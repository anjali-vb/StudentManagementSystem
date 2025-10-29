using StudentRecordManagementSystem.Models.Entities;
using System.Data;

namespace StudentRecordManagementSystem.Models.Interfaces
{
    public interface IStudentRepository
    {
        DataTable GetAllStudents();
        Student? GetStudentByRoll(int rollNumber);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int rollNumber);
        int GenerateNextRollNumber();
    }
}
