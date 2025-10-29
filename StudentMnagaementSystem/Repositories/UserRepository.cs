using System.Data.SqlClient;
using StudentRecordManagementSystem.Models.Entities;
using StudentRecordManagementSystem.Models.Interfaces;

namespace StudentRecordManagementSystem.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            // Fix: Use the correct connection string key from appsettings.json
            _connectionString = configuration.GetConnectionString("MVCConnectionString");
        }

        public User? GetUser(string username, string password)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetUser", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString() ?? "",
                    Password = reader["Password"].ToString() ?? "",
                    Role = reader["Role"].ToString() ?? "",
                    StudentRollNumber = reader["StudentRollNumber"] == DBNull.Value ? null : (int)reader["StudentRollNumber"]
                };
            }
            return null;
        }
    }
}
