
using Microsoft.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAuthentication("Training").AddScheme<AuthenticationSchemeOptions,TrainingAuthHandler>("Training", null);
builder.Services.AddAuthorization();

//
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

builder.Host.UseDefaultServiceProvider(options =>
{
options.ValidateScopes = true;
options.ValidateOnBuild = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
//Register routing in the pipeline where it belongs for your app.
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/api/assessments/results", () => Results.Ok(new
{
    courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
})).RequireAuthorization();

// This endpoint is for testing the EnrollmentWorker background service. It triggers the ProcessBatch method to process a batch of enrollments.
app.MapGet("/api/enrollments/worker-smoke", (EnrollmentWorker worker) =>
{
worker.ProcessBatch();
return Results.Ok("processed"); });


app.Run();
