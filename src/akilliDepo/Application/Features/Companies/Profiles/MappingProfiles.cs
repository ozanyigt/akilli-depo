using Application.Features.Companies.Commands.Create;
using Application.Features.Companies.Commands.Delete;
using Application.Features.Companies.Commands.Update;
using Application.Features.Companies.Queries.GetById;
using Application.Features.Companies.Queries.GetList;
using Application.Features.Companies.Queries.GetListByDynamic;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Companies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCompanyCommand, Company>();
        CreateMap<Company, CreatedCompanyResponse>();

        CreateMap<UpdateCompanyCommand, Company>();
        CreateMap<Company, UpdatedCompanyResponse>();

        CreateMap<DeleteCompanyCommand, Company>();
        CreateMap<Company, DeletedCompanyResponse>();

        CreateMap<Company, GetByIdCompanyResponse>();

        CreateMap<Company, GetListCompanyListItemDto>();
        CreateMap<IPaginate<Company>, GetListResponse<GetListCompanyListItemDto>>();

        CreateMap<Company, GetListByDynamicCompanyListItemDto>();
        CreateMap<IPaginate<Company>, GetListResponse<GetListByDynamicCompanyListItemDto>>();
    }
}