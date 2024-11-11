using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace NZWalksAPI.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

      private static List<string> students = new List<string> {"John","Jane","Mark","Emily","Joe","Rick"};
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[students.Count];

            for (int i = 0; i < students.Count; i++)
            {
                studentNames[i] = students[i];
            }

            return Ok(studentNames);
        }

        [HttpPost]
        public IActionResult AddStudent(string student) 
        {
            if (string.IsNullOrEmpty(student))
            {
                return BadRequest("Cannot Add a Student into the List!");
            }

            students.Add(student);

            return CreatedAtAction(nameof(AddStudent), student);
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteStudent(string name) 
        {
            var StudentList = students.FirstOrDefault(w => w.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (StudentList == null)
            {
                return NotFound($"Student {name} not found");
            }

            students.Remove(StudentList);

            return Ok($"Student {students} has been removed successfully");

        }

        public class StudentNameChanger
        {
            public string OldName { get; set; }

            public string NewName { get; set; }
        };


        [HttpPut("{name}")]

        public IActionResult ChangeStudentName(string name, [FromBody] string NewName)
        {
            var StudentList = students.FirstOrDefault(w => w.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("The new name cannot be empty!");
            }

            int Index = students.IndexOf(StudentList);
            students[Index] = NewName;

            return Ok($"Student {name} was changed to {NewName}");

        }


    
    
    }

}
