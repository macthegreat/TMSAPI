public interface IEnrollmentService
{
    Task<EnrollmentRecord> EnrollUserInCourseAsync(string StudentId, string CourseCode);
    Task<EnrollmentRecord?> GetByIdAsync(string ID);
    Task<IReadOnlyList<EnrollmentRecord>> GetAllAsync();
    Task<bool> DeleteAsync(string id);
}