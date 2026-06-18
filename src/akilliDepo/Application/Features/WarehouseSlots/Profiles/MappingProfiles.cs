using Application.Features.WarehouseSlots.Commands.Create;
using Application.Features.WarehouseSlots.Commands.Delete;
using Application.Features.WarehouseSlots.Commands.Update;
using Application.Features.WarehouseSlots.Queries.GetById;
using Application.Features.WarehouseSlots.Queries.GetList;
using Application.Features.WarehouseSlots.Queries.GetListByDynamic;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.WarehouseSlots.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateWarehouseSlotCommand, WarehouseSlot>();
        CreateMap<WarehouseSlot, CreatedWarehouseSlotResponse>();

        CreateMap<UpdateWarehouseSlotCommand, WarehouseSlot>();
        CreateMap<WarehouseSlot, UpdatedWarehouseSlotResponse>();

        CreateMap<DeleteWarehouseSlotCommand, WarehouseSlot>();
        CreateMap<WarehouseSlot, DeletedWarehouseSlotResponse>();

        CreateMap<WarehouseSlot, GetByIdWarehouseSlotResponse>();

        CreateMap<WarehouseSlot, GetListWarehouseSlotListItemDto>();
        CreateMap<IPaginate<WarehouseSlot>, GetListResponse<GetListWarehouseSlotListItemDto>>();

        CreateMap<WarehouseSlot, GetListByDynamicWarehouseSlotListItemDto>();
        CreateMap<IPaginate<WarehouseSlot>, GetListResponse<GetListByDynamicWarehouseSlotListItemDto>>();
    }
}