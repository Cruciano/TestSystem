using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class AnswerDto
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}
