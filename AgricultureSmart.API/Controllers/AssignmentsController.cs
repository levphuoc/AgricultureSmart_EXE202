using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.AssignmentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IEngineerFarmerAssignmentService _assignmentService;

        public AssignmentsController(IEngineerFarmerAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Get all assignments with pagination
        /// </summary>
        /// <param name="pageIndex">Page index (0-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of assignments</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAssignments([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var assignments = await _assignmentService.GetAllAsync(pageIndex, pageSize);
                return Ok(new
                {
                    success = true,
                    message = "Assignments retrieved successfully.",
                    data = assignments
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving assignments: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get assignment by ID
        /// </summary>
        /// <param name="id">Assignment ID</param>
        /// <returns>Assignment details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            try
            {
                var assignment = await _assignmentService.GetByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Assignment not found."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Assignment retrieved successfully.",
                    data = assignment
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving assignment: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get assignments by engineer ID
        /// </summary>
        /// <param name="engineerId">Engineer ID</param>
        /// <param name="pageIndex">Page index (0-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of assignments for the engineer</returns>
        [HttpGet("engineer/{engineerId}")]
        public async Task<IActionResult> GetAssignmentsByEngineerId(int engineerId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var assignments = await _assignmentService.GetByEngineerIdAsync(engineerId, pageIndex, pageSize);
                return Ok(new
                {
                    success = true,
                    message = "Engineer assignments retrieved successfully.",
                    data = assignments
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving engineer assignments: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get assignments by farmer ID
        /// </summary>
        /// <param name="farmerId">Farmer ID</param>
        /// <param name="pageIndex">Page index (0-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paginated list of assignments for the farmer</returns>
        [HttpGet("farmer/{farmerId}")]
        public async Task<IActionResult> GetAssignmentsByFarmerId(int farmerId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var assignments = await _assignmentService.GetByFarmerIdAsync(farmerId, pageIndex, pageSize);
                return Ok(new
                {
                    success = true,
                    message = "Farmer assignments retrieved successfully.",
                    data = assignments
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving farmer assignments: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Create a new assignment
        /// </summary>
        /// <param name="model">Assignment creation model</param>
        /// <returns>Created assignment details</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentModel model)
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

                var response = await _assignmentService.CreateAsync(model);
                if (!response.Success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = response.Message
                    });
                }

                return CreatedAtAction(nameof(GetAssignmentById), new { id = response.Data.Id }, new
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
                    message = $"Error creating assignment: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Update an existing assignment
        /// </summary>
        /// <param name="id">Assignment ID</param>
        /// <param name="model">Assignment update model</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] UpdateAssignmentModel model)
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
                var response = await _assignmentService.UpdateAsync(model);
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
                    message = $"Error updating assignment: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Delete an assignment
        /// </summary>
        /// <param name="id">Assignment ID</param>
        /// <returns>Delete result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                var response = await _assignmentService.DeleteAsync(id);
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
                    message = $"Error deleting assignment: {ex.Message}"
                });
            }
        }
    }
}
