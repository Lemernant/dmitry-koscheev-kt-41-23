using System.Text.Json.Serialization;

namespace dmitry_koscheev_kt_41_23.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name{ get; set; }
        public bool isDeleted { get; set; }
        [JsonIgnore]
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
