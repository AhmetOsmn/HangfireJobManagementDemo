using HangfireJobManagementDemo.HangfireJobs.Abstract;
using HangfireJobManagementDemo.HangfireJobs.JobParameters;
using HangfireJobManagementDemo.Services.Abstract;

namespace HangfireJobManagementDemo.HangfireJobs.Concrete
{
    public class MessagePrinterJob : BaseJob, IMessagePrinterJob
    {
        private readonly IMessageService _messageService;

        public MessagePrinterJob(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override void Run(IJobParameters jobParameters)
        {
            var messagePrinterParameters = (MessagePrinterParameters)jobParameters;

            ArgumentNullException.ThrowIfNull(messagePrinterParameters);

            _messageService.Add(messagePrinterParameters.Text, messagePrinterParameters.CreatedDate, messagePrinterParameters.CreatedBy);
        }
    }
}
