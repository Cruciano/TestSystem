using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class TestDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

        public int ThemeId { get; set; }
        public string ThemeTitle { get; set; }
    }
}
