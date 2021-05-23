using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;


namespace BLL.Initiator
{
    public static class BllServiceInitiator
    {
        public static IServiceCollection InitBllServices(this IServiceCollection services)
        {
            services.AddTransient<IThemeService, ThemeService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IUserTestingService, UserTestingService>();

            return services;
        }
    }
}
