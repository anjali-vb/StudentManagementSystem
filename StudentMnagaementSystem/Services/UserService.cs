using StudentRecordManagementSystem.Models.Entities;
using StudentRecordManagementSystem.Models.Interfaces;

namespace StudentRecordManagementSystem.Models.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public User? Authenticate(string username, string password)
        {
            return _repo.GetUser(username, password);
        }
    }
}
