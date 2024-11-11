using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private static List<string> WorkerNames = new List<string> { "Klajd", "Lirak", "Astrit", "Lirim", "Besmir" };

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(WorkerNames);
        }
        
        [HttpPost]
        public IActionResult AddWorker(string workerName)
        {

            if (string.IsNullOrEmpty(workerName))
            {
                return BadRequest("Cannot Add a Null name");
            }

            WorkerNames.Add(workerName);

            return CreatedAtAction(nameof(GetWorkers), new {name = workerName}, workerName);
            //return Ok($"User {workerName} added into the list!");
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteWorker(string name)
        {
            var worker = WorkerNames.FirstOrDefault(w => w.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (worker == null)
            {
                return NotFound($"Worker {name} not found");
            }

            WorkerNames.Remove(worker);

            return Ok($"Worker {name} has been removed successfully");
        }


        public class WorkerUpdateModel
        {
            public string OldName { get; set; }
            public string NewName { get; set; }
        }


        [HttpPut("{name}")]
        public IActionResult UpdateWorkerName(string updatedWorker, string name)
        {
            var worker = WorkerNames.FirstOrDefault(w => w.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (worker == null)
            {
                return NotFound();
            }

            int index = WorkerNames.IndexOf(worker);
            WorkerNames[index] = updatedWorker;



            var result = new WorkerUpdateModel()
            {
                OldName = name,
                NewName = updatedWorker
            };
            return Ok($"Worker {name} has been modified to {updatedWorker} ");
        }

    }
}
