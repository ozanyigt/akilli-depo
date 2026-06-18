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

namespace Application.Features.Companies.Queries.GetListByDynamic;

public class GetListByDynamicCompanyQuery : IRequest<GetListResponse<GetListByDynamicCompanyListItemDto>>, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery Dynamic { get; set; }
    public Guid CompanyId { get; set; }

    public class GetListByDynamicCompanyQueryHandler : IRequestHandler<GetListByDynamicCompanyQuery, GetListResponse<GetListByDynamicCompanyListItemDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly MultiTenantBusinessRules _multiTenantBusinessRules;

        public GetListByDynamicCompanyQueryHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            MultiTenantBusinessRules multiTenantBusinessRules
        )
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _multiTenantBusinessRules = multiTenantBusinessRules;
        }

        public async Task<GetListResponse<GetListByDynamicCompanyListItemDto>> Handle(
            GetListByDynamicCompanyQuery request,
            CancellationToken cancellationToken
        )
        {
            await _multiTenantBusinessRules.CompanyIdMustBeProvided(request.CompanyId);

            IPaginate<Company> companies = await _companyRepository.GetListByDynamicAsync(
                dynamic: request.Dynamic,
                predicate: c => c.CompanyId == request.CompanyId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicCompanyListItemDto>>(companies);
            return response;
        }
    }
}
