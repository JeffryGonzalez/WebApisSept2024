namespace Issues.Api.Vendors;

public class Api(VendorData vendor, TimeProvider timeProvider) : ControllerBase
{

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetVendorsAsync(CancellationToken token)
    {

        // var vendorLookup = new VendorData(); // the new keyword cannot be used on ANYTHING that is touching a backing service.
        var vendors = await vendor.GetVendorInformationAsync(token);
        return Ok(vendors);
    }

    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync([FromBody] VendorCreateRequest request)
    {
        // Todo: Validate this sucker.
        // Todo: Make sure this is being posted by a SoftwareCenter Admin, if not, 401 or 403 (Tomorrow)
        // Todo: If it looks good, create a VendorItemEntity, save that to the database
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
        return Ok(response);
    }
}


public record VendorCreateRequest
{
    public string Name { get; set; } = string.Empty;
}
public record VendorInformationResponse(string id, string Name);
