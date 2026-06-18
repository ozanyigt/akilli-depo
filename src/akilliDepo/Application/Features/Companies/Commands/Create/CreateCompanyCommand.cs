using Application.Common.Rules;
using Application.Features.Companies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Companies.Commands.Create;

public class CreateCompanyCommand : IRequest<CreatedCompanyResponse>, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required bool IsActive { get; set; }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreatedCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly CompanyBusinessRules _companyBusinessRules;

        public CreateCompanyCommandHandler(
            IMapper mapper,
            ICompanyRepository companyRepository,
            CompanyBusinessRules companyBusinessRules
        )
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _companyBusinessRules = companyBusinessRules;
        }

        public async Task<CreatedCompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company = _mapper.Map<Company>(request);

            await _companyRepository.AddAsync(company);

            company.CompanyId = company.Id;
            await _companyRepository.UpdateAsync(company);

            CreatedCompanyResponse response = _mapper.Map<CreatedCompanyResponse>(company);
            return response;
        }
    }
}
