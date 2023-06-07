using tasks.Interfaces;
using tasks.Services;
using tasks._0.Interfaces;
using tasks._0.Services;

namespace tasks.Utilities
{
    public static class Additions
    {
        public static void AddTask(this IServiceCollection services){
            services.AddSingleton<IConnect,TaskService>();
            services.AddSingleton<IUser,UserService>();
            services.AddTransient<ILogService, LogService>();
            
        }
    }
}