namespace StudentRecordManagementSystem.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int RollNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Maths { get; set; }
        public int Physics { get; set; }
        public int Chemistry { get; set; }
        public int English { get; set; }


        public int Programming { get; set; }
    }
}

