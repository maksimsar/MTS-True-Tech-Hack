using AutoMapper;
using MTSTrueTechHack.Backend.Models;
using MTSTrueTechHack.Backend.Models.Dtos;

namespace MTSTrueTechHack.Backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // HTTP request models → internal DTOs
            CreateMap<CreateSchemaRequest, CreateSchemaDto>()
                .ForMember(dest => dest.JsonSchema, opt => opt.Ignore());

            CreateMap<ChatRequest, ChatDto>();

            // Entities → internal DTOs
            CreateMap<Schema, SchemaDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.JsonSchema, opt => opt.MapFrom(src => src.JSONSchema));

            CreateMap<Message, ChatMessageDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.IsFromUser, opt => opt.MapFrom(src => src.IsFromUser))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp));

            // Internal DTOs → HTTP response models
            CreateMap<SchemaDto, SchemaResponse>();
            CreateMap<ChatMessageDto, ChatMessageResponse>();
        }
    }
}