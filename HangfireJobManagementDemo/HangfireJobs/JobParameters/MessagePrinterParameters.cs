namespace HangfireJobManagementDemo.HangfireJobs.JobParameters
{
    public class MessagePrinterParameters : IJobParameters
    {
        public string Text { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
    }
}
