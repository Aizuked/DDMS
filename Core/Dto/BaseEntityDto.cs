using AutoMapper;
using Core.Models;

namespace Core.Dto;

public class BaseEntityDto : BaseEntity;

public partial class BaseEntityDtoProfile : Profile
{
    public BaseEntityDtoProfile()
    {
        CreateMap<BaseEntityDto, BaseEntityDto>();
    }
}
