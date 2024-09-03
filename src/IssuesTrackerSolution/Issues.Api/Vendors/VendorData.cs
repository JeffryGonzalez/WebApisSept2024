using Issues.Api.Catalog;

namespace Issues.Api.Vendors;

public class VendorData : ILookupVendors
{
    private Dictionary<string, VendorInformationResponse> fakeVendors = new()
        {
            {"microsoft", new VendorInformationResponse("Microsoft") },
            { "jetbrains", new VendorInformationResponse("Jetbrains") }
        };
    public async Task<Dictionary<string, VendorInformationResponse>> GetVendorInformationAsync(CancellationToken token)
    {
        // Todo - make this more real.
        return fakeVendors;
    }

    public async Task<bool> IsCurrentVendorAsync(string vendor)
    {
        return fakeVendors.ContainsKey(vendor.ToLowerInvariant());
    }
}