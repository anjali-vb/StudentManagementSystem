using StudentRecordManagementSystem.Models.Entities;

namespace StudentRecordManagementSystem.Models.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);
    }
}
