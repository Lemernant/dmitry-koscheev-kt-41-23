using Azure.Core.Pipeline;

namespace dmitry_koscheev_kt_41_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
        public bool isDeleted { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
