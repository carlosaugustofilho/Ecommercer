using AutoMapper;
using Ecommercer.Communictaion.Requests;
using Ecommercer.Domain.Entites;

namespace Ecommercer.Aplication.Services.AutoMapper
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<RequestRegistrarUsuarioJson, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id if it's not set by request
                .ForMember(dest => dest.Ativo, opt => opt.Ignore()) // Set default value in Usuario entity
                .ForMember(dest => dest.Cadastrado, opt => opt.Ignore()); // Set default value in Usuario entity
        }
    }
}
