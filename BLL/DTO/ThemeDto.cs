using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class ThemeDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<TestDto> Tests { get; set; }
    }
}
