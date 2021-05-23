using System;

namespace DAL.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        public string TestTitle { get; set; }

        public User user { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
    }
}
