using Auction.API;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureRepositories();
builder.Services.ConfigureCosmosDbClient(builder.Configuration);
builder.Services.ConfigureMediatr();
builder.Services.ConfigureAutomapper();
builder.Services.ConfigureCarter();
builder.Services.ConfigureCors();
builder.Services.ConfigureJwtAuth(builder.Configuration);

var app = builder.Build();

app.UseCors();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(e => e.MapCarter());

app.Run();