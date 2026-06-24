namespace TmsApi.Entities;

public class Enrollment
{
    public int Id { get; set; }

    public string StudentId { get; set; } =null!;

    public string CourseId { get; set; } = null!;

    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public decimal? Grade { get; set; }

    public Student Student { get; set; } = null!;

    public Course Course { get; set; } = null!;
}