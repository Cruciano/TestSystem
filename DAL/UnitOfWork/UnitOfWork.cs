using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;

        private IRepository<User> _userRepository;
        public IRepository<User> UserRepository => _userRepository ??= new Repository<User>(_context);

        private IRepository<Theme> _themeRepository;
        public IRepository<Theme> ThemeRepository => _themeRepository ??= new Repository<Theme>(_context);

        private IRepository<Test> _testRepository;
        public IRepository<Test> TestRepository => _testRepository ??= new Repository<Test>(_context);

        private IRepository<Question> _questionRepository;
        public IRepository<Question> QuestionRepository => _questionRepository ??= new Repository<Question>(_context);

        private IRepository<Answer> _answerRepository;
        public IRepository<Answer> AnswerRepository => _answerRepository ??= new Repository<Answer>(_context);

        private IRepository<Result> _resultRepository;
        public IRepository<Result> ResultRepository => _resultRepository ??= new Repository<Result>(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
