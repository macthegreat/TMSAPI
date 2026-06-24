public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllAsync();

    Task<Course?> GetByCodeAsync(string code);

    Task<Course> CreateAsync(Course course);

    Task<bool> DeleteAsync(string code);
}   
    
