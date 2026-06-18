using Application.Features.Warehouses.Commands.Create;
using Application.Features.Warehouses.Commands.Delete;
using Application.Features.Warehouses.Commands.Update;
using Application.Features.Warehouses.Queries.GetById;
using Application.Features.Warehouses.Queries.GetList;
using Application.Features.Warehouses.Queries.GetListByDynamic;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Warehouses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateWarehouseCommand, Warehouse>();
        CreateMap<Warehouse, CreatedWarehouseResponse>();

        CreateMap<UpdateWarehouseCommand, Warehouse>();
        CreateMap<Warehouse, UpdatedWarehouseResponse>();

        CreateMap<DeleteWarehouseCommand, Warehouse>();
        CreateMap<Warehouse, DeletedWarehouseResponse>();

        CreateMap<Warehouse, GetByIdWarehouseResponse>();

        CreateMap<Warehouse, GetListWarehouseListItemDto>();
        CreateMap<IPaginate<Warehouse>, GetListResponse<GetListWarehouseListItemDto>>();

        CreateMap<Warehouse, GetListByDynamicWarehouseListItemDto>();
        CreateMap<IPaginate<Warehouse>, GetListResponse<GetListByDynamicWarehouseListItemDto>>();
    }
}