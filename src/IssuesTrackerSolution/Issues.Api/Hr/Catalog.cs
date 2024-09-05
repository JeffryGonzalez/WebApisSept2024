using Issues.Api.Catalog;
using Marten;

namespace Issues.Api.Hr;

public class Catalog(IDocumentSession session) : ControllerBase
{

    [HttpGet("/hr/software-catalog")]
    public async Task<ActionResult> GetFullSoftwareCatalogAsync(CancellationToken cancellationToken)
    {
        var response = await session.Query<CatalogItemEntity>()
            .Select(item => new HrCatalogItem() { Id = item.Slug, Title = item.Name, Vendor = item.Vendor })
            .ToListAsync(cancellationToken);

        return Ok(new { catalog = response });
    }
}

public record HrCatalogItem
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
}