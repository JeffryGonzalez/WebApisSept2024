using Issues.Api.Vendors.Models;
using Issues.Api.Vendors.Services;
using Microsoft.AspNetCore.Authorization;



namespace Issues.Api.Vendors.Api;
[Authorize] // don't accept any requests without a valid Authorization header
public class VendorQueriesController(VendorData vendor) : ControllerBase
{

    [HttpGet("/vendors")]
    public async Task<ActionResult<Dictionary<string, VendorInformationResponse>>> GetVendorsAsync(CancellationToken token)
    {


        // var vendorLookup = new VendorData(); // the new keyword cannot be used on ANYTHING that is touching a backing service.
        var vendors = await vendor.GetVendorInformationAsync(token);
        return Ok(vendors);
    }


}
