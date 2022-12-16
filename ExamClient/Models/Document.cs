using System.ComponentModel.DataAnnotations;

namespace ExamClient.Models
{
    public class Document
    {
        [Key]
        public int Document_Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
        public string? Path { get; set; }
    }
}
