using GolfMatchScore.Shared.Models.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.CourseServices
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseListItem>> GetAllCoursesAsync();
        Task<bool> CreateCourseAsync(CourseCreate model);
        Task<CourseDetail> GetCourseByIdAsync(int courseId);
        Task<bool> EditCourseInfoByIdAsync(CourseEdit model);
        Task<bool> DeleteCourseByIdAsync(int courseId);
        void SetUserId(string userId);

    }
}