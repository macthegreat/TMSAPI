
public interface IEnrollmentService
{
    Task<EnrollmentRecord> EnrollUserInCourseAsync(string StudentId, string CourseCode);
    Task<EnrollmentRecord?> GetByIdAsync(string ID);
    Task<IReadOnlyList<EnrollmentRecord>> GetAllAsync();
    Task<bool> DeleteAsync(string id);
}
// The EnrollmentService class implements the IEnrollmentService interface and provides methods to manage course enrollments for students.

public class EnrollmentService : IEnrollmentService
{
    private readonly Dictionary<string, EnrollmentRecord> _store = new();
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(ILogger<EnrollmentService> logger)
    {
        _logger = logger;
    }

    public Task<EnrollmentRecord> EnrollUserInCourseAsync(string StudentId, string CourseCode)
    {

        var id = Guid.NewGuid().ToString("N")[..8];
        var record = new EnrollmentRecord(id, StudentId, CourseCode, DateTime.UtcNow);
        _store[id] = record;



        // Check if the student is already enrolled in the course
        var existing = _store.Values.FirstOrDefault(e => e.StudentId == StudentId && e.CourseCode == CourseCode);
        if (existing is not null)
        {
            _logger.LogWarning(
"Duplicate enrollment attempt {StudentId} already in {CourseCode} (record {EnrollmentId})",
StudentId, CourseCode, existing.Id);
            return Task.FromResult(existing);
        }
        // Create a new enrollment record
        var Id = Guid.NewGuid().ToString("N")[..8];
        var record1 = new EnrollmentRecord(id, StudentId, CourseCode, DateTime.UtcNow); _store[id] = record;
        _logger.LogInformation("Enrolled {StudentId} in {CourseCode} (record {EnrollmentId})", StudentId, CourseCode, id);
        return Task.FromResult(record);
    }

    public Task<EnrollmentRecord?> GetByIdAsync(string id)
    {
        _store.TryGetValue(id, out var record); if (record is null)
        {
            _logger.LogWarning("Enrollment {EnrollmentId} not found", id);
        }
        return Task.FromResult(record);
    }


    // The GetByIdAsync method retrieves an enrollment record by its ID. It checks if the record exists in the store and returns it, or null if not found.
    public Task<bool> DeleteAsync(string id)
    {
        var removed = _store.Remove(id);
        if (removed)
        {
            _logger.LogInformation("Deleted enrollment {EnrollmentId}", id);
        }
        else
        {
            _logger.LogWarning("Failed to delete enrollment {EnrollmentId} not found", id);
        }
        return Task.FromResult(removed);
    }

    public Task<IReadOnlyList<EnrollmentRecord>> GetAllAsync()
    {
        IReadOnlyList<EnrollmentRecord> records = _store.Values.ToList().AsReadOnly();
        return Task.FromResult(records);
    }


    public Task<EnrollmentRecord> EnrollAsync(string StudentId, string courseCode)
    {
        var Existing = _store.Values.FirstOrDefault(e => e.StudentId == StudentId && e.CourseCode == courseCode);
        if (Existing is not null)
        {
            _logger.LogWarning("Duplicate enrollment attempt {StudentId} already in {CourseCode} (record {EnrollmentId})", StudentId, courseCode, Existing.Id);
            return Task.FromResult(Existing);

        }

        var id = Guid.NewGuid().ToString("N")[..8];
        var record = new EnrollmentRecord(id, StudentId, courseCode, DateTime.UtcNow);
        _store[id] = record;
        _logger.LogInformation("Enrolled {StudentId} in {CourseCode} record {EnrollmentId}", StudentId, courseCode, id);
        return Task.FromResult(record);



    }
    
    }

    public class TmsDatabaseException(string message) : Exception(message)
    {
        
    }
    


    //Olddddd code
    // The EnrollmentService class implements the IEnrollmentService interface and provides methods to manage course enrollments for students. It uses an in-memory dictionary to store enrollment records and includes logging for key operations.
    // public Task<EnrollmentRecord?> GetByIdAsync(string ID)
    // {
    //     var record = _store.TryGetValue(ID, out var value) ? value : null;
    //     return Task.FromResult(record);
    // }


    // The GetByIdAsync method retrieves an enrollment record by its ID. It checks if the record exists in the store and returns it, or null if not found.



    // The GetAllAsync method returns a read-only list of all enrollment records currently stored in the service.
    //     public Task<bool> DeleteAsync(string id)
    //     {
    //         var removed = _store.Remove(id);
    //         return Task.FromResult(removed);
    //     }
    // }





public record EnrollmentRecord(string Id, string StudentId, string CourseCode, DateTime EnrolledAt);


