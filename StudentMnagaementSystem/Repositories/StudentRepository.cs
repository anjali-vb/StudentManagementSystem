using System.Data;
using System.Data.SqlClient;
using StudentRecordManagementSystem.Models.Entities;
using StudentRecordManagementSystem.Models.Interfaces;

namespace StudentRecordManagementSystem.Models.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;
        public StudentRepository(IConfiguration configuration)
        {
            
            _connectionString = configuration.GetConnectionString("MVCConnectionString");
        }

        public DataTable GetAllStudents()
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetAllStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using SqlDataAdapter da = new(cmd);
            DataTable dt = new();
            da.Fill(dt);
            return dt;
        }

        public Student? GetStudentByRoll(int rollNumber)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetStudentByRoll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            con.Open();
            using SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                return new Student
                {
                    Id = (int)r["Id"],
                    RollNumber = (int)r["RollNumber"],
                    Name = r["Name"].ToString() ?? "",
                    Maths = (int)r["Maths"],
                    Physics = (int)r["Physics"],
                    Chemistry = (int)r["Chemistry"],
                    English = (int)r["English"],
                    Programming = (int)r["Programming"]
                };
            }
            return null;
        }

        public void AddStudent(Student s)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_AddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", s.RollNumber);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Maths", s.Maths);
            cmd.Parameters.AddWithValue("@Physics", s.Physics);
            cmd.Parameters.AddWithValue("@Chemistry", s.Chemistry);
            cmd.Parameters.AddWithValue("@English", s.English);
            cmd.Parameters.AddWithValue("@Programming", s.Programming);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateStudent(Student s)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_UpdateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", s.RollNumber);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Maths", s.Maths);
            cmd.Parameters.AddWithValue("@Physics", s.Physics);
            cmd.Parameters.AddWithValue("@Chemistry", s.Chemistry);
            cmd.Parameters.AddWithValue("@English", s.English);
            cmd.Parameters.AddWithValue("@Programming", s.Programming);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteStudent(int rollNumber)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_DeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public int GenerateNextRollNumber()
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetMaxRollNumber", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value) return 10000;
            return Convert.ToInt32(result) + 1;
        }
    }
}
