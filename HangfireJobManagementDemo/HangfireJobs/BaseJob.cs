using System.Security.Cryptography;

namespace HangfireJobManagementDemo.HangfireJobs
{
    public abstract class BaseJob : IBaseJob
    {
        public abstract void Run(IJobParameters jobParameters);
    }
}
