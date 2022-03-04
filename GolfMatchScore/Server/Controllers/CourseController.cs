using GolfMatchScore.Server.Data;
using GolfMatchScore.Server.Services.CourseServices;
using GolfMatchScore.Shared.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null)
                return null;

            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _courseService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses.ToList());
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> Course(int courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound();
            
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!SetUserIdInService())
                return Unauthorized();

            bool courseCreated = await _courseService.CreateCourseAsync(model);

            if (courseCreated)
                return Ok();
            return UnprocessableEntity();
        }

        [HttpPut("edit/{courseId}")]
        public async Task<IActionResult> Edit(int courseId, CourseEdit model)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            if (model == null || !ModelState.IsValid)
                return BadRequest();

            if (model.CourseId != courseId)
                return BadRequest();

            bool editedCourse = await _courseService.EditCourseInfoByIdAsync(model);

            if (editedCourse)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("delete/{courseId}")]
        public async Task<IActionResult> Delete(int courseId)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var selectCourse = await _courseService.GetCourseByIdAsync(courseId);
            if (selectCourse == null)
                return NotFound();

            bool deleteCourse = await _courseService.DeleteCourseByIdAsync(courseId);
            if (deleteCourse)
                return Ok();

            return BadRequest();
        }
     
    }
}
