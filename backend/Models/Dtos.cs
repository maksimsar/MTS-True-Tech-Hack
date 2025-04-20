namespace MTSTrueTechHack.Backend.Models.Dtos;


/// <summary>DTO, поступающий в бизнес‑логику для создания схемы.</summary>
public sealed record CreateSchemaDto(
    int    UserId,
    string Name,
    string Description,
    string JsonSchema = ""
);

/// <summary>DTO, который сервис отдаёт контроллеру.</summary>
public sealed record SchemaDto(
    int      Id,
    string   Name,
    string   Description,
    string   JsonSchema,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

/// <summary>DTO одного сообщения в истории диалога.</summary>
public sealed record ChatMessageDto(
    string   Text,
    bool     IsFromUser,
    DateTime Timestamp
);
/// <summary>Внутренний DTO для GPT‑клиента — диалог против схемы.</summary>
public sealed record ChatDto(
    string Message,
    IEnumerable<ChatMessageDto> History,
    string JsonSchema = ""
);
