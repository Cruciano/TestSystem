using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<User> UserRepository { get; }

        public IRepository<Theme> ThemeRepository { get; }

        public IRepository<Test> TestRepository { get; }

        public IRepository<Question> QuestionRepository { get; }

        public IRepository<Answer> AnswerRepository { get; }

        public Task SaveAsync();
    }
}
