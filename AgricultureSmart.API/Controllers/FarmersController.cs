using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.FarmerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmersController : ControllerBase
    {
        private readonly IFarmerService _farmerService;

        public FarmersController(IFarmerService farmerService)
        {
            _farmerService = farmerService;
        }

        /// <summary>
        /// Search farmers with pagination.
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchFarmers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? farmLocation = null,
            [FromQuery] decimal? farmSize = null,
            [FromQuery] string? cropTypes = null)
        {
            try
            {
                var result = await _farmerService.SearchAsync(
                                 pageNumber, pageSize,
                                 farmLocation, farmSize, cropTypes);

                return Ok(new
                {
                    success = true,
                    message = "Farmers retrieved successfully.",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving farmers: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Get farmer by ID
        /// </summary>
        /// <param name="id">Farmer ID</param>
        /// <returns>Farmer details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFarmerById(int id)
        {
            try
            {
                var farmer = await _farmerService.GetByIdAsync(id);
                if (farmer == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Farmer not found."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Farmer retrieved successfully.",
                    data = farmer
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error retrieving farmer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Create a new farmer
        /// </summary>
        /// <param name="model">Farmer creation model</param>
        /// <returns>Created farmer details</returns>
        [HttpPost]
        public async Task<IActionResult> CreateFarmer([FromBody] CreateFarmerModel model)
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

                var response = await _farmerService.CreateAsync(model);
                if (!response.Success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = response.Message
                    });
                }

                return CreatedAtAction(nameof(GetFarmerById), new { id = response.Data.Id }, new
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
                    message = $"Error creating farmer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Update an existing farmer
        /// </summary>
        /// <param name="id">Farmer ID</param>
        /// <param name="model">Farmer update model</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarmer(int id, [FromBody] UpdateFarmerModel model)
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
                var response = await _farmerService.UpdateAsync(model);
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
                    message = $"Error updating farmer: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Delete a farmer
        /// </summary>
        /// <param name="id">Farmer ID</param>
        /// <returns>Delete result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmer(int id)
        {
            try
            {
                var response = await _farmerService.DeleteAsync(id);
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
                    message = $"Error deleting farmer: {ex.Message}"
                });
            }
        }
    }
}
