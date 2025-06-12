using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ReviewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(page, pageSize);
            return Ok(new { message = "Lấy danh sách đánh giá thành công", data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound(new { message = "Không tìm thấy đánh giá" }) : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { message = "Tạo đánh giá thành công", data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? Ok(new { message = "Cập nhật đánh giá thành công" }) : NotFound(new { message = "Không tìm thấy đánh giá để cập nhật" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok(new { message = "Xóa đánh giá thành công" }) : NotFound(new { message = "Không tìm thấy đánh giá để xóa" });
        }
    }
}
