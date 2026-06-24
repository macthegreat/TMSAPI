public class StudentService : IStudentService
{
    private readonly List<Student> _students = [];

    public Task<IEnumerable<Student>> GetAllAsync()
    {
        return Task.FromResult(_students.AsEnumerable());
    }

    public Task<Student?> GetByIdAsync(string id)
    {
        return Task.FromResult(
            _students.FirstOrDefault(x => x.Id == id));
    }

    public Task<Student> CreateAsync(Student student)
    {
        _students.Add(student);
        return Task.FromResult(student);
    }

    public Task<bool> DeleteAsync(string id)
    {
        var student =
            _students.FirstOrDefault(x => x.Id == id);

        if (student is null)
            return Task.FromResult(false);
            
             _students.Remove(student);

        return Task.FromResult(true);
    }
}