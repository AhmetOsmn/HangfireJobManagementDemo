using Hangfire.States;

namespace HangfireJobManagementDemo.HangfireJobs.States
{
    public class CreatedState : IState
    {
        public string Name => "Created";
        public string? Reason { get; set; }
        public bool IsFinal => false;
        public bool IgnoreJobLoadException => false;

        public Dictionary<string, string> SerializeData()
        {
            return new Dictionary<string, string>();
        }
    }
}
