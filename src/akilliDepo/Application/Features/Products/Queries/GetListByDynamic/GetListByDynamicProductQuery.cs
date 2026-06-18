using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductQuery : IRequest<GetListResponse<GetListByDynamicProductListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListByDynamicProductQueryHandler : IRequestHandler<GetListByDynamicProductQuery, GetListResponse<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListByDynamicProductQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(
            GetListByDynamicProductQuery request,
            CancellationToken cancellationToken
        )
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Product> products = await _productRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                predicate: p => p.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicProductListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(products);
            return response;
        }
    }
}
