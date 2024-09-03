namespace Issues.Api.Vendors;

public class Api(VendorData vendor) : ControllerBase
{

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetVendorsAsync(CancellationToken token)
    {

        // var vendorLookup = new VendorData(); // the new keyword cannot be used on ANYTHING that is touching a backing service.
        var vendors = await vendor.GetVendorInformationAsync(token);
        return Ok(vendors);
    }
}

public record VendorInformationResponse(string Name);
