using Microsoft.AspNetCore.Mvc;

[Controller]
[Route("api/enrollment")]

public class EnrollmentController(IEnrollmentService enrollmentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollment = await enrollmentService.GetAllAsync();
        return Ok(enrollment);

    }
    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(string id)
    {
        var record = await enrollmentService.GetByIdAsync(id);
        return record is not null ? Ok(record) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> create([FromBody] CreateEnrollmentRequest request)
    {
        var record = await enrollmentService.EnrollUserInCourseAsync(request.StudentId, request.CourseCode);
        return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);

    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await enrollmentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

}

public record CreateEnrollmentRequest(string StudentId, string CourseCode);