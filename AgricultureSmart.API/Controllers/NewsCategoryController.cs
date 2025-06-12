using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.NewCategoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsCategoryController : ControllerBase
    {
        private readonly INewsCategoryService _service;

        public NewsCategoryController(INewsCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(new { message = "Lấy danh sách thành công", data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = $"Không tìm thấy NewsCategory với ID = {id}" });

            return Ok(new { message = "Lấy thông tin thành công", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsCategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });

            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new
            {
                message = "Tạo NewsCategory thành công",
                data = result
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsCategoryUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });

            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound(new { message = $"Không tìm thấy NewsCategory với ID = {id}" });

            return Ok(new { message = "Cập nhật NewsCategory thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"Không tìm thấy NewsCategory với ID = {id}" });

            return Ok(new { message = "Xóa NewsCategory thành công" });
        }
    }
}
