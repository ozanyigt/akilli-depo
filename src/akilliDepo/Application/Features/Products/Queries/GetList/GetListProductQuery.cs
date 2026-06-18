using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<GetListResponse<GetListProductListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CompanyId { get; set; }
    public string? SearchTerm { get; set; }

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, GetListResponse<GetListProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListProductQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            string? search = string.IsNullOrWhiteSpace(request.SearchTerm) ? null : request.SearchTerm.Trim();

            IPaginate<Product> products = await _productRepository.GetListAsync(
                predicate: p =>
                    p.CompanyId == request.CompanyId
                    && (
                        search == null
                        || p.Name.Contains(search)
                        || p.Code.Contains(search)
                        || p.Description.Contains(search)
                    ),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductListItemDto> response = _mapper.Map<GetListResponse<GetListProductListItemDto>>(products);
            return response;
        }
    }
}
