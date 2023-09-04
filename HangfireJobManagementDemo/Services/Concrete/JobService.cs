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
                var parameters = GetRecurringJobParameters<MessagePrinterParameters>(item.Id);

                customJobs.Add(new() { Name = item.Id, Type = item.Job.Type.Name, CreatedBy = parameters.CreatedBy, Text = parameters.Text });
            }

            return customJobs;
        }

        public void Schedule(string jobId, string cronExpression)
        {
            var oldParameters = GetRecurringJobParameters<MessagePrinterParameters>(jobId);

            RecurringJob.AddOrUpdate<MessagePrinterJob>(jobId, x => x.Run(oldParameters), cronExpression);
        }

        private T GetRecurringJobParameters<T>(string jobId) where T : class
        {
            using var hangfireConnection = JobStorage.Current.GetConnection();

            var selectedJob = hangfireConnection.GetRecurringJobs(new List<string> { jobId }).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(selectedJob);

            var parametersAsJson = JsonSerializer.Serialize(selectedJob.Job.Args.FirstOrDefault());

            var parameters = JsonSerializer.Deserialize<T>(parametersAsJson);

            ArgumentNullException.ThrowIfNull(parameters);

            return parameters;
        }

    }
}
