﻿using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models;
using AgricultureSmart.Services.Models.TicketModels;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/ticket
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _ticketService.GetAllAsync(pageIndex, pageSize);
            return Ok(result);
        }

        // GET: api/ticket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null)
                return NotFound(new { Message = "Ticket not found." });

            return Ok(ticket);
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ticketService.CreateAsync(model);
            if (!result.Success)
                return StatusCode(500, result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        // PUT: api/ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.Id)
                return BadRequest(new { Message = "ID mismatch." });

            var result = await _ticketService.UpdateAsync(model);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // DELETE: api/ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ticketService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Cập nhật trạng thái ticket.
        /// </summary>
        /// <remarks>
        /// Quy luật chuyển đổi trạng thái hợp lệ:
        /// - open → assigned, closed  
        /// - assigned → in_progress, open, closed  
        /// - in_progress → resolved, assigned, closed  
        /// - resolved → closed, in_progress  
        /// - closed → (không thể chuyển trạng thái)
        /// </remarks>

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTicketStatusModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.Id)
                return BadRequest(new { Message = "ID mismatch." });

            var result = await _ticketService.UpdateStatusAsync(model);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        // GET: api/ticket/status-info
        [HttpGet("status-info")]
        public IActionResult GetStatusInfo()
        {
            var statusInfo = new
            {
                ValidStatuses = TicketStatusConstants.ValidStatuses,
                ValidTransitions = TicketStatusConstants.ValidTransitions,
                StatusDescriptions = new Dictionary<string, string>
                {
                    { TicketStatusConstants.Open, "Ticket is newly created and waiting for assignment" },
                    { TicketStatusConstants.Assigned, "Ticket has been assigned to an engineer" },
                    { TicketStatusConstants.InProgress, "Engineer is actively working on the ticket" },
                    { TicketStatusConstants.Resolved, "Issue has been resolved" },
                    { TicketStatusConstants.Closed, "Ticket is closed and no further action needed" }
                }
            };

            return Ok(statusInfo);
        }
    }
}
