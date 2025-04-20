// File: MappingProfile.cs
using AutoMapper;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Backend.Models.Dtos;

namespace MTSTrueTechHack.Backend
{
    public class MappingProfile : Profile
    {
    public MappingProfile()
    {
        CreateMap<CreateSchemaRequest, CreateSchemaDto>();
        CreateMap<Schema, SchemaDto>()
            .ForMember(d => d.Id,         o => o.MapFrom(s => s.ID))
            .ForMember(d => d.JsonSchema, o => o.MapFrom(s => s.JSONSchema));
        CreateMap<ChatRequest, ChatDto>();
    }
    }
}
