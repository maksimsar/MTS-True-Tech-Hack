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

        // генерируем (метод остался GenerateSchemaAsync)
        var json = await _gpt.GenerateSchemaAsync(dto);

        var entity = new Schema
        {
            UserID      = req.UserId,
            Name        = req.Name,
            Description = req.Description,
            JSONSchema  = json,
            CreatedAt   = DateTime.UtcNow,
            UpdatedAt   = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);
        await _repo.SaveAsync();

        // map to SchemaDto и возвращаем
        return _mapper.Map<SchemaDto>(entity);
    }

    public async Task<ChatMessageResponse> ChatAsync(int schemaId, ChatRequest req)
    {
        var schema = await _repo.GetAsync(schemaId)
                     ?? throw new KeyNotFoundException($"Schema {schemaId} not found");

        // Конструируем DTO через конструктор, передаём все три параметра
        var chatDto = new ChatDto(
            schema.JSONSchema,  // JsonSchema
            req.Message,        // Message
            req.History         // History
        );

        // Вызываем именно ContinueChatAsync, а не GenerateChatAsync
        var replyText = await _gpt.ContinueChatAsync(chatDto);

        // Используем позиционные аргументы, чтобы не ошибаться с регистром имен
        return new ChatMessageResponse(replyText, false, DateTime.UtcNow);
    }
}