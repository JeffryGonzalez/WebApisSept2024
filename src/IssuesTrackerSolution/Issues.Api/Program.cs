using HtTemplate.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomFeatureManagement();

builder.Services.AddCustomServices();
builder.Services.AddCustomOasGeneration();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();