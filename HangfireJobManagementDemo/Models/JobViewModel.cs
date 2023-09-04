using HangfireJobManagementDemo.HangfireJobs;

namespace HangfireJobManagementDemo.Models
{
    public class JobViewModel
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public IJobParameters Parameters { get; set; } = null!;
    }
}
