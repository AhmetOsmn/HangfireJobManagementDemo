using Hangfire;
using HangfireJobManagementDemo.HangfireJobs.JobParameters;
using HangfireJobManagementDemo.Models;
using HangfireJobManagementDemo.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HangfireJobManagementDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IJobService _jobService;

        public HomeController(IMessageService messageService, IJobService jobService)
        {
            _messageService = messageService;
            _jobService = jobService;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel() { Messages = _messageService.GetAll(), CustomJobs = _jobService.GetAll() });
        }

        public IActionResult SaveCustomJob(SaveJobRequestModel saveJobRequestModel)
        {

            _jobService.Add(new JobViewModel()
            {
                Name = saveJobRequestModel.JobName,
                Type = saveJobRequestModel.JobType,
                Parameters = new MessagePrinterParameters()
                {
                    Text = saveJobRequestModel.MessageText,
                    CreatedBy = saveJobRequestModel.CreatedBy,
                    CreatedDate = saveJobRequestModel.CreatedDate
                }
            });

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ScheduleCustomJob(string jobId, string cronExpression)
        {
            _jobService.Schedule(jobId, cronExpression);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteJob(string id)
        {
            _jobService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteMessage(int id)
        {
            _messageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteJobSchedule(int id)
        {
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}