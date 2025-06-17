using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.EngineerModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : ControllerBase
    {
        private readonly IEngineerService _engineerService;

        public EngineersController(IEngineerService engineerService)
        {
            _engineerService = engineerService;
        }

        /// <summary>
        /// Search engineers with specialization, experienceYears
        /// </summary>
        /// <param name="pageIndex">Page index (0-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of engineers</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search(
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10,
         [FromQuery] string? specialization = null,
         [FromQuery] int? experienceYears = null)
        {
            try
            {
                var result = await _engineerService.SearchAsync(
                                 pageNumber, pageSize,
                                 specialization, experienceYears);

                return Ok(new
                {
                    success = true,
                    message = "Engineers retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving engineers: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get engineer by ID
        /// </summary>
        /// <param name="id">Engineer ID</param>
        /// <returns>Engineer details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEngineerById(int id)
        {
            try
            {
                var engineer = await _engineerService.GetByIdAsync(id);
                if (engineer == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Engineer not found."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Engineer retrieved successfully.",
                    data = engineer
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving engineer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Create a new engineer
        /// </summary>
        /// <param name="model">Engineer creation model</param>
        /// <returns>Created engineer details</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEngineer([FromBody] CreateEngineerModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid model data.",
                        errors = ModelState
                    });
                }

                var response = await _engineerService.CreateAsync(model);
                if (!response.Success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = response.Message
                    });
                }

                return CreatedAtAction(nameof(GetEngineerById), new { id = response.Data.Id }, new
                {
                    success = true,
                    message = response.Message,
                    data = response.Data
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error creating engineer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Update an existing engineer
        /// </summary>
        /// <param name="id">Engineer ID</param>
        /// <param name="model">Engineer update model</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEngineer(int id, [FromBody] UpdateEngineerModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid model data.",
                        errors = ModelState
                    });
                }

                model.Id = id;
                var response = await _engineerService.UpdateAsync(model);
                if (!response.Success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = response.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = response.Message,
                    data = response.Data
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error updating engineer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Delete an engineer
        /// </summary>
        /// <param name="id">Engineer ID</param>
        /// <returns>Delete result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineer(int id)
        {
            try
            {
                var response = await _engineerService.DeleteAsync(id);
                if (!response.Success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = response.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = response.Message,
                    data = response.Data
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error deleting engineer: {ex.Message}"
                });
            }
        }
    }
}
