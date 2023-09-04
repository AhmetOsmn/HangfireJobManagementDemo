namespace HangfireJobManagementDemo.HangfireJobs
{
    public class DeclareJobParameters
    {
        public string JobName { get; set; }

        public IJobParameters? RunParameters { get; set; }
        public DeclareJobParameters(string jobName, IJobParameters? runParameters)
        {
            JobName = jobName;
            RunParameters = runParameters;
        }
    }
}
