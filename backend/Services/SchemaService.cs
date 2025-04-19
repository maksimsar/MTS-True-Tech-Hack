// File: Services/SchemaService.cs
using AutoMapper;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Backend.Models.Dtos;

namespace MTSTrueTechHack.Backend.Services;

public class SchemaService : ISchemaService
{
    private readonly IMapper _mapper;
    private readonly GptClient _gpt;
    private readonly ISchemaRepository _repo;

    public SchemaService(IMapper mapper, GptClient gpt, ISchemaRepository repo)
    {
        _mapper = mapper;
        _gpt    = gpt;
        _repo   = repo;
    }

    public async Task<SchemaDto> CreateAsync(CreateSchemaRequest req)
    {
        // map request → CreateSchemaDto
        var dto = _mapper.Map<CreateSchemaDto>(req);

        // генерируем
        var json = await _gpt.GenerateSchemaAsync(dto);

        var entity = new Schema {
            UserID     = req.UserId,
            Name       = req.Name,
            Description= req.Description,
            JSONSchema = json,
            CreatedAt  = DateTime.UtcNow,
            UpdatedAt  = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);
        await _repo.SaveAsync();

        // map to SchemaDto (тот же класс, что вернёт контроллер)
        return _mapper.Map<SchemaDto>(entity);
    }

    public async Task<ChatMessageResponse> ChatAsync(int schemaId, ChatRequest req)
    {
        var schema = await _repo.GetAsync(schemaId)
                     ?? throw new KeyNotFoundException($"Schema {schemaId} not found");

        var chatDto = new ChatDto { // ChatDto — внутренний DTO для GptClient
            JsonSchema = schema.JSONSchema,
            Message    = req.Message,
            History    = req.History
        };

        var replyText = await _gpt.GenerateChatAsync(chatDto);
        return new ChatMessageResponse(replyText, false, DateTime.UtcNow);
    }
}
