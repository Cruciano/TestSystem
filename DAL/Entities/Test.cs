using System.Collections.Generic;

namespace DAL.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Theme Theme { get; set; }
        public int ThemeId { get; set; }
    }
}
