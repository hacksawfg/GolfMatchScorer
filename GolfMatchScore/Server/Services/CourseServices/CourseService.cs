using GolfMatchScore.Server.Data;
using GolfMatchScore.Server.Models;
using GolfMatchScore.Shared.Models.Course;
using GolfMatchScore.Shared.Models.GolfRound;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfMatchScore.Server.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;

        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateCourseAsync(CourseCreate model)
        {
            var courseEntity = new Course
            {
                OwnerId = _userId,
                CourseName = model.CourseName,
                CourseCity = model.CourseCity,
                CourseState = model.CourseState,
                CoursePar = model.CoursePar,
                CourseSlope = model.CourseSlope,
                CourseDifficultyRating = model.CourseDifficultyRating,
                CourseLength = model.CourseLength
            };

            _context.Courses.Add(courseEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteCourseByIdAsync(int courseId)
        {
            var entity = await _context.Courses.FindAsync(courseId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.Courses.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> EditCourseInfoByIdAsync(CourseEdit model)
        {
            if (model == null)
                return false;

            var entity = await _context.Courses.FindAsync(model.CourseId);
            if (entity?.OwnerId != _userId)
                return false;

            entity.CourseName = model.CourseName;
            entity.CourseCity = model.CourseCity;
            entity.CourseState = model.CourseState;
            entity.CoursePar = model.CoursePar;
            entity.CourseSlope = model.CourseSlope;
            entity.CourseDifficultyRating = model.CourseDifficultyRating;
            entity.CourseLength = model.CourseLength;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<CourseListItem>> GetAllCoursesAsync()
        {
            var courseQuery = await _context.Courses.Select(n => new CourseListItem
            {
                CourseId = n.CourseId,
                CourseName = n.CourseName,
                CourseCity = n.CourseCity,
                CourseState = n.CourseState
            }).ToListAsync();

            return courseQuery;
        }

        public async Task<CourseDetail> GetCourseByIdAsync(int courseId)
        {
            var courseQueryId = await _context.Courses
                .Include(r => r.Rounds)
                .FirstOrDefaultAsync(entity => entity.CourseId == courseId);

            return courseQueryId is null ? null : new CourseDetail
            {
                CourseId = courseQueryId.CourseId,
                CourseName = courseQueryId.CourseName,
                CourseCity = courseQueryId?.CourseCity,
                CourseState = courseQueryId?.CourseState,
                CoursePar = courseQueryId.CoursePar,
                CourseDifficultyRating = courseQueryId.CourseDifficultyRating,
                CourseSlope = courseQueryId.CourseSlope,
                CourseLength = courseQueryId.CourseLength,
                Rounds = courseQueryId.Rounds.Select(r => new RoundListItem
                {
                    RoundId = r.RoundId,
                    MatchDate = r.MatchDate
                }).ToList()
            };
        }
    }
}
