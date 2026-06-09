using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class EnrollmentWorker(IServiceScopeFactory scopeFactory)
{
    public void ProcessBatch()
    {
        var Scope=scopeFactory.CreateScope();

        var svc=Scope.ServiceProvider.GetRequiredService<IEnrollmentService>();


    }
}





















//OLD CODE

// public class EnrollmentWorker : BackgroundService
// {
//     private readonly ILogger<EnrollmentWorker> _logger;
//     private readonly IEnrollmentService _enrollmentService;

//     public EnrollmentWorker(ILogger<EnrollmentWorker> logger, IEnrollmentService enrollmentService)
//     {
//         _logger = logger;
//         _enrollmentService = enrollmentService;
//     }

//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         _logger.LogInformation("EnrollmentWorker is starting.");

//         while (!stoppingToken.IsCancellationRequested)
//         {
//             try
//             {
//                 await ProcessBatch();
//                 await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
//             }
//             catch (OperationCanceledException)
//             {
//                 _logger.LogInformation("EnrollmentWorker is stopping.");
//                 break;
//             }
//         }
//     }

//     public async Task ProcessBatch()
//     {
//         try
//         {
//             var allEnrollments = await _enrollmentService.GetAllAsync();
//             _logger.LogInformation("Processing {count} enrollments", allEnrollments.Count);
            
//             // Add your batch processing logic here
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, "Error processing enrollment batch");
//         }
//     }
// }

