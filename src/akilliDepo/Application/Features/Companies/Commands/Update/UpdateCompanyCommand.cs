using Application.Common.Rules;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Companies.Commands.Update;

public class UpdateCompanyCommand : IRequest<UpdatedCompanyResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required bool IsActive { get; set; }
    public required Guid CompanyId { get; set; }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, UpdatedCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyBusinessRules _companyBusinessRules;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public UpdateCompanyCommandHandler(
            IMapper mapper,
            ICompanyRepository companyRepository,
            CompanyBusinessRules companyBusinessRules,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _companyBusinessRules = companyBusinessRules;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<UpdatedCompanyResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            Company? company = await _companyRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _companyBusinessRules.CompanyShouldExistWhenSelected(company);
            await _multiTenantBusinessRules.CompanyMustMatchWhenSelected(company!.CompanyId, request.CompanyId);

            company = _mapper.Map(request, company);
            company!.CompanyId = request.CompanyId;

            await _companyRepository.UpdateAsync(company);

            UpdatedCompanyResponse response = _mapper.Map<UpdatedCompanyResponse>(company);
            return response;
        }
    }
}
