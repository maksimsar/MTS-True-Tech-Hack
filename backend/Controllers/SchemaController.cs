using Microsoft.AspNetCore.Mvc;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Backend.Models.Dtos;
using MTSTrueTechHack.Backend.Services;

namespace MTSTrueTechHack.Backend.Controllers
{
    [ApiController]
    [Route("api/schemas")]
    public sealed class SchemaController : ControllerBase
    {
        private readonly ISchemaService _service;

        public SchemaController(ISchemaService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(SchemaResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<SchemaResponse>> Create([FromBody] CreateSchemaRequest request)
        {
            // 1) сервис → DTO
            SchemaDto created = await _service.CreateAsync(request);

            // 2) DTO → публичный контракт
            var response = new SchemaResponse(created.Id, created.Name, created.JsonSchema);

            // 3) 201 Created + Location
            return CreatedAtRoute(nameof(GetById),
                                  new { id = response.Id },
                                  response);
        }
        
        [HttpGet("{id:int}", Name = nameof(GetById))]
        [ProducesResponseType(typeof(SchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            SchemaDto? dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();

            var resp = new SchemaResponse(dto.Id, dto.Name, dto.JsonSchema);
            return Ok(resp);
        }
        
        [HttpPost("{id:int}/chat")]
        [ProducesResponseType(typeof(ChatMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Chat(int id, [FromBody] ChatRequest request)
        {
            try
            {
                ChatMessageResponse reply = await _service.ChatAsync(id, request);
                return Ok(reply);
            }
            catch (KeyNotFoundException) // проброшено из сервиса, если схемы нет
            {
                return NotFound();
            }
        }
    }
}
