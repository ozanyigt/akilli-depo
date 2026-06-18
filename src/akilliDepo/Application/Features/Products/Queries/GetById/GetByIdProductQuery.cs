using Application.Common.Rules;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductQuery : IRequest<GetByIdProductResponse>
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetByIdProductQueryHandler(
            IMapper mapper,
            IProductRepository productRepository,
            ProductBusinessRules productBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(product!.CompanyId, request.CompanyId);

            GetByIdProductResponse response = _mapper.Map<GetByIdProductResponse>(product);
            return response;
        }
    }
}
