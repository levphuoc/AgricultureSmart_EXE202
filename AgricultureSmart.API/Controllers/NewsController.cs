using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.NewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(new { message = "Lấy danh sách tin tức thành công", data = result });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.SearchAsync(title, author, categoryId, page, pageSize);
            return Ok(new { message = "Tìm kiếm thành công", data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null
                ? NotFound(new { message = $"Không tìm thấy bản tin với ID = {id}" })
                : Ok(new { message = "Lấy tin tức thành công", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { message = "Tạo tin tức thành công", data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success
                ? Ok(new { message = "Cập nhật tin tức thành công" })
                : NotFound(new { message = $"Không tìm thấy bản tin với ID = {id}" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success
                ? Ok(new { message = "Xóa tin tức thành công" })
                : NotFound(new { message = $"Không tìm thấy bản tin với ID = {id}" });
        }
    }
}
