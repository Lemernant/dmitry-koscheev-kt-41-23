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
            return Regex.IsMatch(Code, @"^\s*\d{2}\s*\.\s*\d{2}\s*\.\s*\d{2}\s*$");
        }

        public bool IsValidSpecialtyTitle()
        {
            return Regex.IsMatch(Code, @"^[А-ЯЁ][а-яё]*$");
        }
    }
}
