using Hangfire;
using Hangfire.Storage;
using HangfireJobManagementDemo.HangfireJobs.Concrete;
using HangfireJobManagementDemo.HangfireJobs.JobParameters;
using HangfireJobManagementDemo.Models;
using HangfireJobManagementDemo.Services.Abstract;
using System.Text.Json;

namespace HangfireJobManagementDemo.Services.Concrete
{
    public class JobService : IJobService
    {
        public void Add(JobViewModel jobViewModel)
        {
            switch (jobViewModel.Type)
            {
                case "MessagePrinter":
                    //var newJob = Job.FromExpression<MessagePrinterJob>(x => x.DeclareJob(new DeclareJobParameters(jobViewModel.Name, jobViewModel.Parameters)));
                    //_backgroundJobClient.Create(newJob, new CreatedState());

                    RecurringJob.AddOrUpdate<MessagePrinterJob>(jobViewModel.Name, x => x.Run(jobViewModel.Parameters), Cron.Never());


                    break;

                default:
                    break;
            }

        }

        public void Delete(string id)
        {
            RecurringJob.RemoveIfExists(id);
        }

        public List<CustomJob> GetAll()
        {
            using var hangfireConnection = JobStorage.Current.GetConnection();
            var recurringJobs = hangfireConnection.GetRecurringJobs();

            List<CustomJob> customJobs = new();

            foreach (var item in recurringJobs)
            {
                var argsArray = item.Job.Arguments;

                foreach (var arg in argsArray)
                {
                    var temp = JsonSerializer.Deserialize<MessagePrinterParameters>(arg);
                    customJobs.Add(new() { Name = item.Id, Type = item.Job.Type.Name, CreatedBy = temp.CreatedBy, Text = temp.Text });
                }
            }


            return customJobs;
        }

        public void Schedule(string jobId, string cron)
        {
            using var hangfireConnection = JobStorage.Current.GetConnection();

            var temp1 = hangfireConnection.GetJobData(jobId);
            var temp2 = hangfireConnection.GetRecurringJobs(new List<string> { jobId }).FirstOrDefault();
        }
    }
}
