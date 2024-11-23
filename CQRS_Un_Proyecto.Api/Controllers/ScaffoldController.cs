using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Horizonte.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaffoldController : ControllerBase
    {
        [HttpPost]
        public IActionResult UpdateModel()
        {
            try
            {
                string connectionString = "Server=34.16.53.56;Database=GestionSalud;User=root;";
                string provider = "Microsoft.EntityFrameworkCore.SqlServer";
                string outputDir = "Model";
                string project = "CQRS_Un_Proyecto.Infrastructure";

                string command = $"dotnet ef dbcontext scaffold \"{connectionString}\" {provider} -o {outputDir} --project {project} --force";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C {command}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit();

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                if (process.ExitCode == 0)
                {
                    return Ok(new { message = "Model and DbContext updated successfully.", output });
                }
                else
                {
                    return BadRequest(new { message = "Failed to update model and DbContext.", error });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", exception = ex.Message });
            }
        }
    }
}
