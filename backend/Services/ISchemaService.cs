/*
// File: Services/ISchemaService.cs
using MTSTrueTechHack.Backend.Models.Dtos;
using MTSTrueTechHack.Backend.Models.Responses;

namespace MTSTrueTechHack.Backend.Services;

public interface ISchemaService
{
    /// <summary>
    /// Creates a new JSON schema via GPT, persists it, and returns response DTO
    /// </summary>
    Task<SchemaResponse> CreateAsync(CreateSchemaRequest req);

    /// <summary>
    /// Continues a chat against an existing schema
    /// </summary>
    Task<ChatMessageResponse> ChatAsync(int schemaId, ChatRequest req);
} */

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
