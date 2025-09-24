using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Nop.Data;
using NopStation.Plugin.Misc.Core.Domains;
using NopStation.Plugin.Misc.Core.Infrastructure;

namespace NopStation.Plugin.Misc.Core.Services;

public class LicenseService : ILicenseService
{
    #region Fields

    private readonly IRepository<License> _licenseRepository;

    #endregion

    #region Ctor

    public LicenseService(IRepository<License> licenseRepository)
    {
        _licenseRepository = licenseRepository;
    }

    #endregion

    #region Methods

    public async Task InsertLicenseAsync(License license)
    {
        await _licenseRepository.InsertAsync(license);
    }

    public async Task UpdateLicenseAsync(License license)
    {
        await _licenseRepository.UpdateAsync(license);
    }

    public async Task DeleteLicenseAsync(License license)
    {
        await _licenseRepository.DeleteAsync(license);
    }

    public async Task<IList<License>> GetLicensesAsync()
    {
        return await _licenseRepository.Table.ToListAsync();
    }

    public KeyVerificationResult VerifyProductKey(string key, bool checkFileName = false, string fileName = "")
    {
        return KeyVerificationResult.InvalidProductKey;
    }

    public Task<bool> IsLicensedAsync(Assembly assembly)
    {
        return Task.FromResult(true);
    }

    #endregion
}
