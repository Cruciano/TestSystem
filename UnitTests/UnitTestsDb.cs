using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Entities;

namespace UnitTests
{
    static class UnitTestsDb
    {
        public static DbContextOptions<AppDbContext> GetDbOptions()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            FillData(context);
            return options;
        }

        private static void FillData(AppDbContext context)
        {
            User user1 = new User() { Id = 1, FirstName = "Name_1", LastName = "SurName_1" };
            User user2 = new User() { Id = 2, FirstName = "Name_2", LastName = "SurName_2" };

            context.Users.Add(user1);
            context.Users.Add(user2);

            context.Themes.Add(new Theme() { Id = 1, Tests = new List<Test>(), Title = "Theme_1" });
            context.Themes.Add(new Theme() { Id = 2, Tests = new List<Test>(), Title = "Theme_2" });

            context.Tests.Add(new Test() { Id = 1, Questions = new List<Question>(), Title = "Test_1", ThemeId = 1 });
            context.Tests.Add(new Test() { Id = 2, Questions = new List<Question>(), Title = "Test_2", ThemeId = 2 });

            context.Questions.Add(new Question()
                { Id = 1, Answers = new List<Answer>(), Score = 4, TaskText = "Question_1", TestId = 1});

            context.Questions.Add(new Question()
                { Id = 2, Answers = new List<Answer>(), Score = 5, TaskText = "Question_2", TestId = 2 });

            context.Answers.Add(new Answer() { Id = 1, IsCorrect = true, Text = "Answer_1" ,QuestionId = 1 });
            context.Answers.Add(new Answer() { Id = 2, IsCorrect = false, Text = "Answer_2", QuestionId = 2 });

            context.Results.Add(new Result()
                { 
                    Id = 1,
                    DateTime = new DateTime(2021, 01, 01),
                    Score = 5,
                    TestId = 1,
                    TestTitle = "Test_1",
                    UserId = 1,
                    user = user1
                });

            context.Results.Add(new Result()
            {
                Id = 2,
                DateTime = new DateTime(2021, 01, 01),
                Score = 4,
                TestId = 2,
                TestTitle = "Test_2",
                UserId = 2,
                user = user2
            });

            context.SaveChanges();
        }
    }
}
