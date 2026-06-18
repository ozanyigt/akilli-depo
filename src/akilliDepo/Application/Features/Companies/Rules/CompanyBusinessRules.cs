using Application.Features.Companies.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Companies.Rules;

public class CompanyBusinessRules : BaseBusinessRules
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ILocalizationService _localizationService;

    public CompanyBusinessRules(ICompanyRepository companyRepository, ILocalizationService localizationService)
    {
        _companyRepository = companyRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CompaniesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CompanyShouldExistWhenSelected(Company? company)
    {
        if (company == null)
            await throwBusinessException(CompaniesBusinessMessages.CompanyNotExists);
    }

    public async Task CompanyIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Company? company = await _companyRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CompanyShouldExistWhenSelected(company);
    }
}