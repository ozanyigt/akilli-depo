using Application.Common.Rules;
using Application.Features.Companies.Rules;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<CreatedProductResponse>, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Unit { get; set; }
    public required string Description { get; set; }
    public required Guid CompanyId { get; set; }
    public required decimal MinStockLevel { get; set; }
    public required bool IsActive { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public CreateProductCommandHandler(
            IMapper mapper,
            IProductRepository productRepository,
            ProductBusinessRules productBusinessRules,
            CompanyBusinessRules companyBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _companyBusinessRules = companyBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<CreatedProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);
            await _companyBusinessRules.CompanyIdShouldExistWhenSelected(request.CompanyId, cancellationToken);

            Product product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);

            CreatedProductResponse response = _mapper.Map<CreatedProductResponse>(product);
            return response;
        }
    }
}
