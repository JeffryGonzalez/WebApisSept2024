﻿using FluentValidation;
using Issues.Api.Vendors.Models;
using Issues.Api.Vendors.Services;
using Microsoft.AspNetCore.Authorization;



namespace Issues.Api.Vendors.Api;

[Authorize(Policy = "IsSoftwareCenterAdmin")]
public class VendorCommandsController
    (VendorData vendor,
    IValidator<VendorCreateRequest> validator
    )
    : ControllerBase
{
    [HttpPost("/vendors")] // nobody should be able to do this unless they meet the security policy
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateRequest request
       )
    {

        var validations = await validator.ValidateAsync(request);


        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }

        VendorInformationResponse response = await vendor.AddVendorAsync(request);

        return Ok(response);
    }
}