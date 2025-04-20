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
        // 1. Формируем DTO для GPT
        var dto = new CreateSchemaDto(req.UserId, req.Name, req.Description, string.Empty);

        // 2. Генерируем JSON‑схему
        var jsonSchema = await _gpt.GenerateSchemaAsync(dto);

        // 3. Создаём и сохраняем Entity
        var entity = new Schema {
            UserID     = req.UserId,
            Name       = req.Name,
            Description= req.Description,
            JSONSchema = jsonSchema,
            CreatedAt  = DateTime.UtcNow,
            UpdatedAt  = DateTime.UtcNow
        };
        await _repo.AddAsync(entity);
        await _repo.SaveAsync();

        // 4. Маппим Entity → внутренний DTO
        return _mapper.Map<SchemaDto>(entity);
    }

    public async Task<ChatMessageResponse> ChatAsync(int schemaId, ChatRequest req)
    {
        var schema = await _repo.GetAsync(schemaId)
                     ?? throw new KeyNotFoundException($"Schema {schemaId} not found");

        // Сохраняем запрос пользователя
        schema.Messages.Add(new Message {
            SchemaID   = schemaId,
            Text       = req.Message,
            IsFromUser = true,
            Timestamp  = DateTime.UtcNow
        });
        await _repo.SaveAsync();

        // Собираем историю и вызываем GPT
        var historyDtos = schema.Messages
            .Select(m => new ChatMessageDto(m.Text, m.IsFromUser, m.Timestamp))
            .ToList();

        // Правильный порядок: сначала Message, потом History, потом JsonSchema
        var chatDto = new ChatDto(
            Message:    req.Message,
            History:    historyDtos,
            JsonSchema: schema.JSONSchema
        );
        var replyText = await _gpt.ContinueChatAsync(chatDto);

        // Сохраняем ответ ассистента
        schema.Messages.Add(new Message {
            SchemaID   = schemaId,
            Text       = replyText,
            IsFromUser = false,
            Timestamp  = DateTime.UtcNow
        });
        await _repo.SaveAsync();

        // Возвращаем HTTP‑контракт
        return new ChatMessageResponse(replyText, false, DateTime.UtcNow);
    }

    public async Task<SchemaDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetAsync(id);
        return entity is null ? null : _mapper.Map<SchemaDto>(entity);
    }
}