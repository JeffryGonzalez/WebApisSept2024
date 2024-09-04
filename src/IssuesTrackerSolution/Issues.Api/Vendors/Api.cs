using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Authorization;


namespace Issues.Api.Vendors;
[Authorize] // don't accept any requests without a valid Authorization header
public class Api(VendorData vendor) : ControllerBase
{

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetVendorsAsync(CancellationToken token)
    {

        // var vendorLookup = new VendorData(); // the new keyword cannot be used on ANYTHING that is touching a backing service.
        var vendors = await vendor.GetVendorInformationAsync(token);
        return Ok(vendors);
    }

    [HttpPost("/vendors")] // nobody should be able to do this unless they meet the security policy
    [Authorize(Policy = "IsSoftwareCenterAdmin")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateRequest request,
        [FromServices] IValidator<VendorCreateRequest> validator)
    {
        // Todo: Validate this sucker.
        var validations = await validator.ValidateAsync(request);


        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }
        // - Property Validation - is the incoming request in accordance with the business rules?
        // - Domain Validation (Entity Validation) - 
        // - might not be "pure functions"
        // Todo: Make sure this is being posted by a SoftwareCenter Admin, if not, 401 or 403 (Tomorrow)
        // Todo: If it looks good, create a VendorItemEntity, save that to the database


        VendorInformationResponse response = await vendor.AddVendorAsync(request);

        return Ok(response);
    }
}


public record VendorCreateRequest
{

    public string Name { get; set; } = string.Empty;
}

public class VendorCreateRequestValidator : AbstractValidator<VendorCreateRequest>
{
    public VendorCreateRequestValidator(IDocumentSession session)
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(v => v.Name).MustAsync(async (name, cancellation) =>
        {
            var slug = SlugGenerator.GenerateSlugFor(name);
            var exists = await session.Query<VendorItemEntity>().AnyAsync(v => v.Slug == slug);
            return !exists;
        }).WithMessage("That Vendor Already Exists");
    }
}
public record VendorInformationResponse(string id, string Name);
