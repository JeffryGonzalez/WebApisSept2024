using Issues.Api.Catalog;

namespace Issues.Api.Vendors;

public class VendorData(TimeProvider timeProvider) : ILookupVendors
{
    private Dictionary<string, VendorInformationResponse> fakeVendors = new()
    {

    };
    public async Task<Dictionary<string, VendorInformationResponse>> GetVendorInformationAsync(CancellationToken token)
    {
        // Todo - make this more real.
        return fakeVendors;
    }



    public async Task<VendorInformationResponse> AddVendorAsync(VendorCreateRequest request)
    {
        var entity = new VendorItemEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Added = timeProvider.GetUtcNow(),
            AddedBy = "sub of person additing it", // Tomorrow
            Slug = SlugGenerator.GenerateSlugFor(request.Name),
        };
        // Save it to the database.

        // Todo: Figure out what we need / want to return to them.
        var response = new VendorInformationResponse(entity.Slug, entity.Name);
        return response;
    }

    async Task<bool> ILookupVendors.IsCurrentVendorAsync(string vendor)
    {
        return fakeVendors.ContainsKey(vendor.ToLowerInvariant());
    }
}