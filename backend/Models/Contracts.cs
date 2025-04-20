using System;
using System.Collections.Generic;
using MTSTrueTechHack.Backend.Models.Dtos; 

namespace MTSTrueTechHack.Backend.Models
{
    public sealed record CreateSchemaRequest(
        int    UserId,
        string Name,
        string Description
    );

    public sealed record ChatRequest(
        string                   Message,
        IEnumerable<ChatMessageDto> History
    );
    
    public sealed record SchemaResponse(
        
        int Id,
        string Name,
        string JsonSchema
    );

    public sealed record ChatMessageResponse(
        string   Text,
        bool     IsFromUser,
        DateTime Timestamp
    );
}
