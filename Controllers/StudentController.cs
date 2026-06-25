using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/students")]
public class StudentsController(
    IStudentService studentService,
    ILogger<StudentsController> logger)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation(
            "Retrieving all students");

        var students =
            await studentService.GetAllAsync();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        logger.LogInformation(
            "Retrieving student {StudentId}",
            id);

        var student =
            await studentService.GetByIdAsync(id);

        return student is null
            ? NotFound()
            : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Student student)
    {
        logger.LogInformation(
            "Creating student {StudentId}",
            student.Id);

        var created =
            await studentService.CreateAsync(student);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        logger.LogInformation(
            "Deleting student {StudentId}",
            id);

        var deleted =
            await studentService.DeleteAsync(id);

        return deleted
            ? NoContent()
            : NotFound();
    }



}