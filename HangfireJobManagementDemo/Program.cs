using Hangfire;
using Hangfire.Dashboard;
using HangfireJobManagementDemo.Context;
using HangfireJobManagementDemo.HangfireJobs.Abstract;
using HangfireJobManagementDemo.HangfireJobs.Concrete;
using HangfireJobManagementDemo.Services.Abstract;
using HangfireJobManagementDemo.Services.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HangfireJobManagementDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<JobDemoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection")));

            builder.Services.AddHangfire(configuration => configuration
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<IMessagePrinterJob, MessagePrinterJob>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext _) => false
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<JobDemoContext>();
                dbContext.Database.Migrate();
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}