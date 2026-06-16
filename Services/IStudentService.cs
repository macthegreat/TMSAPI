public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(string id);

    Task<Student> CreateAsync(Student student);

    Task<bool> DeleteAsync(string id);
}