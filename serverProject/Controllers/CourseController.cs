using Microsoft.AspNetCore.Mvc;
using serverProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace serverProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private static List<Category> categories = new List<Category> {
                new Category { id = 1, name = "Programming", icon = "code" },
                new Category { id = 2, name = "Design", icon = "paint-brush" },
                new Category { id = 3, name = "Business", icon = "briefcase" },
                new Category { id = 4, name = "Language", icon = "language" },
                new Category { id = 5, name = "Health", icon = "heart" }
        };
        private static List<Course> courses = new List<Course>  {
                new Course(1, 1, "Introduction to Programming", categories[0], 12, new DateTime(2022, 3, 15), new List<string> {"Basic concepts", "Data types", "Control structures"}, Study.Online, "intro-programming.jpg"),
                new Course(2, 2, "Artificial Intelligence Fundamentals", categories[1], 10, new DateTime(2022, 4, 10), new List<string> {"Machine learning", "Neural networks", "Deep learning"}, Study.Hybrid, "ai-fundamentals.jpg"),
                new Course(3, 3, "Financial Management",categories[2], 8, new DateTime(2022, 4, 25), new List<string> {"Budgeting", "Investment analysis", "Risk management"}, Study.Offline, "financial-management.jpg"),
                new Course(4, 4, "Introduction to Psychology", categories[3], 15, new DateTime(2022, 5, 5), new List<string> {"Behavioral psychology", "Cognitive psychology", "Developmental psychology"}, Study.Online, "intro-psychology.jpg"),
                new Course(5, 5, "Graphic Design Basics", categories[4], 6, new DateTime(2022, 5, 20), new List<string> {"Color theory", "Typography", "Layout design"}, Study.Offline, "graphic-design.jpg")
            };
        private static int counter = 0;
        // GET: api/<CourseController>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return courses;
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return courses.Find(c=>c.id==id);
        }

        [HttpGet("categories")]
        public IEnumerable<Category> GetCourse()
        {
            return categories;
        }
        // POST api/<CourseController>
        [HttpPost]
        public void Post([FromBody] Course value)
        {
            value.id = ++counter;
            courses.Add(value);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Course value)
        {
            var c = courses.Find(c => c.id == id);
            if (c is null)
            {
                c.id = ++counter;
                courses.Add(c);
            }
            else
            {
                c.lecturer = value.lecturer;
                c.name = value.name;
                c.start = value.start;
                c.countOfLessons = value.countOfLessons;
                c.category = value.category;
                c.image = value.image;
                c.study = value.study;
                c.syllabus = value.syllabus;
            }
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var c= courses.Find(c => c.id == id);
            courses.Remove(c);
        }
    }
}
