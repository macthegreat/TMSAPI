using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/courses")]

public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await courseService.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var course = await courseService.GetByCodeAsync(code);
        return course is not null ? Ok(course) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        var createdCourse = await courseService.CreateAsync(course);
        return CreatedAtAction(nameof(GetByCode), new { code = createdCourse.Code }, createdCourse);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        var deleted = await courseService.DeleteAsync(code);
        return deleted ? NoContent() : NotFound();
    }
}