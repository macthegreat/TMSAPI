using Microsoft.AspNetCore.Mvc;


public class CourseController(ICourseService courseService) : ControllerBase
{
    // Get all courses
    [HttpGet("api/course")]
    public async Task<IActionResult> GetAll()
    {
        var courses = await courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

// Get course by code
    [HttpGet("api/course/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var course = await courseService.GetCourseByCodeAsync(code);
        return course is not null ? Ok(course) : NotFound();
    }

    [HttpPost("api/course")]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        // Implementation for creating a new course
        return CreatedAtAction(nameof(GetByCode), new { code = course.Code }, course);
    }

    [HttpDelete("api/course/{code}")]
    public async Task<IActionResult> Delete(string code)
    {
       
       var course = await courseService.GetCourseByCodeAsync(code);
    if (course is null)
        {
            return NotFound(); 
        }
        return  NoContent();
    
    // Implementation for deleting a course
    
    }
}