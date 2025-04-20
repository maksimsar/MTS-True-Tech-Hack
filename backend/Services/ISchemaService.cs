
// File: Services/ISchemaService.cs
using MTSTrueTechHack.Backend.Models;       // для CreateSchemaRequest, ChatMessageResponse
using MTSTrueTechHack.Backend.Models.Dtos; // для CreateSchemaDto, SchemaDto, ChatMessageDto

namespace MTSTrueTechHack.Backend.Services;

public interface ISchemaService
{
    // возвращаем SchemaDto, а не несуществующий SchemaResponse
    Task<SchemaDto> CreateAsync(CreateSchemaRequest req);
    
    // ChatAsync возвращает ChatMessageResponse (он определён в Contracts.cs)
    Task<ChatMessageResponse> ChatAsync(int schemaId, ChatRequest req);
}
