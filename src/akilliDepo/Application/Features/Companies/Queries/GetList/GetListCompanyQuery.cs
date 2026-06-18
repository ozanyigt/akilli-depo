using Application.Common.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Companies.Queries.GetList;

public class GetListCompanyQuery : IRequest<GetListResponse<GetListCompanyListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, GetListResponse<GetListCompanyListItemDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListCompanyQueryHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListCompanyListItemDto>> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Company> companies = await _companyRepository.GetListAsync(
                predicate: c => c.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListCompanyListItemDto>>(companies);
            return response;
        }
    }
}
