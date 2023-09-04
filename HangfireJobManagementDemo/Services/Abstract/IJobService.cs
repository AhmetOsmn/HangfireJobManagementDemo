using HangfireJobManagementDemo.Models;

namespace HangfireJobManagementDemo.Services.Abstract
{
    public interface IJobService
    {
        void Add(JobViewModel jobViewModel);
        void Schedule(string jobId, string cronExpression);
        void Delete(string id);
        List<CustomJob> GetAll();
    }
}
