using System.ComponentModel.DataAnnotations;

namespace ExamClient.Models
{
    public class Case
    {
        [Key]
        public int Case_Id { get; set; }
        public string? Comment { get; set; }
        public ICollection<Document>? Documents { get; set; }
    }
}
