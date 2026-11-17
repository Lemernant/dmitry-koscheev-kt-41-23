using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace dmitry_koscheev_kt_41_23.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
        public ICollection<Group>? Groups { get; set; } = new List<Group>();

        public bool IsValidSpecialtyCode()
        {
            return Regex.IsMatch(Code, @"^\d{2}\.\d{2}\.\d{2}$");
        }
    }
}
