namespace Issues.Api.Catalog;

public class Api : ControllerBase
{

    [HttpPost("/catalog/{vendor}/software")]
    public async Task<ActionResult> AddSoftwareToCatalogAsync([FromBody] CreateSoftwareCatalogItemRequest request, string vendor)
    {
        if (string.IsNullOrEmpty(request.Name)) // and description
        {
            return BadRequest();
        }
        if (vendor != "microsoft")
        {
            return NotFound();
        }
        return Ok(request);
    }
}


public record CreateSoftwareCatalogItemRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

}