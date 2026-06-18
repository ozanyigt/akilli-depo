using Application.Common.Constants;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Common.Rules;

public class MultiTenantBusinessRules : BaseBusinessRules
{
    private readonly ILocalizationService _localizationService;

    public MultiTenantBusinessRules(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public async Task CompanyIdMustBeProvided(Guid companyId)
    {
        if (companyId == Guid.Empty)
        {
            string message = await _localizationService.GetLocalizedAsync(
                CommonBusinessMessages.CompanyIdRequired,
                CommonBusinessMessages.SectionName
            );
            throw new BusinessException(message);
        }
    }

    public async Task CompanyMustMatchWhenSelected(Guid entityCompanyId, Guid requestCompanyId)
    {
        await CompanyIdMustBeProvided(requestCompanyId);

        if (entityCompanyId != requestCompanyId)
        {
            string message = await _localizationService.GetLocalizedAsync(
                CommonBusinessMessages.UnauthorizedCompanyAccess,
                CommonBusinessMessages.SectionName
            );
            throw new AuthorizationException(message);
        }
    }
}
