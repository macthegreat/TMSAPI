
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using TmsApi.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL database context
builder.Services.AddDbContext<TmsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TmsDatabase")).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication("Training").AddScheme<AuthenticationSchemeOptions, TrainingAuthHandler>("Training", null);
builder.Services.AddAuthorization();
builder.Services.AddOptions<paymentOptions>().BindConfiguration("Payment").ValidateDataAnnotations().ValidateOnStart();

//
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Configure the HTTP request pipeline.
//Register routing in the pipeline where it belongs for your app.
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}



app.MapGet("/api/assessments/results", () => Results.Ok(new
{
    courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
})).RequireAuthorization();

// This endpoint is for testing the EnrollmentWorker background service. It triggers the ProcessBatch method to process a batch of enrollments.
app.MapGet("/api/enrollments/worker-smoke", async (EnrollmentWorker worker) =>
{
    await worker.ProcessBatch();
    return Results.Ok("processed");
});


app.MapGet("/api/error", () =>
{
    throw new TmsDatabaseException("Simulated database failure for ProblemDetails testing");
});





/*
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TmsDbContext>();

    context.Database.Migrate();

    if (!context.Students.Any())
    {
        var students = new List<Student>
        {
            new() { RegistrationNumber = "TMS-2026-0001", Name = "AliceSmith", GPA = 3.8m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0002", Name = "Bob Jones", GPA = 2.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0003", Name = "Charlie Brown", GPA = 3.4m, IsActive = false },
            new() { RegistrationNumber = "TMS-2026-0004", Name = "DianaPrince", GPA = 3.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0005", Name = "EvanWright", GPA = 2.5m, IsActive = true }
        };

        context.Students.AddRange(students);

        var courses = new List<Course>
        {
            new() { Code = "CS-101", Title = "Introduction to Computer Science", Capacity = 30 },
            new() { Code = "MATH-201", Title = "Calculus I", Capacity = 25 },
            new() { Code = "ENG-150", Title = "English Literature", Capacity = 20 }
        };

        context.Courses.AddRange(courses);
        context.SaveChanges();

        var enrollments = new List<Enrollment>
      {
     new() { StudentId = students[0].Id, CourseId = courses[0].Id, Grade = 4.0m },
     new() { StudentId = students[0].Id, CourseId = courses[1].Id, Grade = 3.6m },
    new() { StudentId = students[1].Id, CourseId = courses[0].Id, Grade = 2.8m },
    new() { StudentId = students[3].Id, CourseId = courses[1].Id, Grade = 3.9m }
    };
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
     }
}
*/
app.Run();

