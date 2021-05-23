using System;

namespace DAL.Entities
{
    public class Result
    {
        public int Id;
        public int Score;
        public DateTime DateTime;
        public string TestTitle;

        public User user;
        public int UserId;
        public int TestId;
    }
}
