using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        [Required]
        public string TaskText { get; set; }
        [Required]
        public int Score { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }

        public int TestId { get; set; }
        public string TestTitle { get; set; }
    }
}
