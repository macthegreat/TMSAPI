public class CourseService : ICourseService
{
    private readonly List<Course> _courses = new();

    public Task<IEnumerable<Course>> GetAllAsync()
    {
        return Task.FromResult(_courses.AsEnumerable());
    }

    public Task<Course?> GetByCodeAsync(string code)
    {
        var course = _courses.FirstOrDefault(c => c.Code == code);
        return Task.FromResult(course);
    }

    public Task<Course> CreateAsync(Course course)
    {
        _courses.Add(course);
        return Task.FromResult(course);
    }

    public Task<bool> DeleteAsync(string code)
    {
        var course = _courses.FirstOrDefault(c => c.Code == code);
        if (course is null)
        {
            return Task.FromResult(false);
        }

        _courses.Remove(course);
        return Task.FromResult(true);
    }
}