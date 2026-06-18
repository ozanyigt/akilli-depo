using Application.Features.StockMovements.Commands.Create;
using Application.Features.StockMovements.Commands.Delete;
using Application.Features.StockMovements.Commands.Update;
using Application.Features.StockMovements.Queries.GetById;
using Application.Features.StockMovements.Queries.GetList;
using Application.Features.StockMovements.Queries.GetListByDynamic;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.StockMovements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateStockMovementCommand, StockMovement>();
        CreateMap<StockMovement, CreatedStockMovementResponse>();

        CreateMap<UpdateStockMovementCommand, StockMovement>();
        CreateMap<StockMovement, UpdatedStockMovementResponse>();

        CreateMap<DeleteStockMovementCommand, StockMovement>();
        CreateMap<StockMovement, DeletedStockMovementResponse>();

        CreateMap<StockMovement, GetByIdStockMovementResponse>();

        CreateMap<StockMovement, GetListStockMovementListItemDto>();
        CreateMap<IPaginate<StockMovement>, GetListResponse<GetListStockMovementListItemDto>>();

        CreateMap<StockMovement, GetListByDynamicStockMovementListItemDto>();
        CreateMap<IPaginate<StockMovement>, GetListResponse<GetListByDynamicStockMovementListItemDto>>();
    }
}