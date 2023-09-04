using HangfireJobManagementDemo.Models;

namespace HangfireJobManagementDemo.Services.Abstract
{
    public interface IJobService
    {
        void Add(JobViewModel jobViewModel);
        void Schedule(string jobId, string cron);
        void Delete(string id);
        List<CustomJob> GetAll();
    }
}
