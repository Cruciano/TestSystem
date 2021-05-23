using System.Collections.Generic;

namespace DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string TaskText { get; set; }
        public int Score { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public Test Test { get; set; }
        public int TestId { get; set; }
    }
}
