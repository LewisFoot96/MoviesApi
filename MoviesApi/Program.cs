using MoviesApi;
using MoviesApi.Application.Repository;
using MoviesApi.Domain;
using MoviesApi.Endpoints;
using MoviesApi.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ApiCommonClass).Assembly));

builder.AddSqlServerClient("myConnection");

builder.Services.AddScoped<IRepository<Movie>, MoviesRepository>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapMoviesEndpoints();

app.UseHttpsRedirection();

app.Run();
