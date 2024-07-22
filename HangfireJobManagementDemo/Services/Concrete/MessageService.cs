using HangfireJobManagementDemo.Context;
using HangfireJobManagementDemo.Entities;
using HangfireJobManagementDemo.Services.Abstract;

namespace HangfireJobManagementDemo.Services.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly JobDemoContext _dbContext;

        public MessageService(JobDemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(string text, DateTime? jobCreatedDate, string createdBy)
        {
            _dbContext.Add(new Message { Text = text, CreatedBy = createdBy, JobCreatedDate = jobCreatedDate, CreatedDate = DateTime.Now });
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var selectedMessage = _dbContext.Messages.Find(id);

            ArgumentNullException.ThrowIfNull(selectedMessage);

            _dbContext.Messages.Remove(selectedMessage);
            _dbContext.SaveChanges();
        }

        public List<Message> GetAll()
        {
            return _dbContext.Messages.ToList();
        }
    }
}
