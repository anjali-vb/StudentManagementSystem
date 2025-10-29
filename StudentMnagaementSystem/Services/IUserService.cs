using StudentRecordManagementSystem.Models.Entities;

namespace StudentRecordManagementSystem.Models.Interfaces
{
    public interface IUserService
    {
        User? Authenticate(string username, string password);
    }
}
