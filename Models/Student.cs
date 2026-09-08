namespace dmitry_koscheev_kt_41_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
