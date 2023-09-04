namespace HangfireJobManagementDemo.HangfireJobs
{
    public interface IBaseJob
    {
        void Run(IJobParameters jobParameters);
    }
}
