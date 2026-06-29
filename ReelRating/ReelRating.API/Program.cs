using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using ReelRating.Infrastructure.Authentication;
using ReelRating.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("authentication", new OpenApiInfo
    {
        Title = "Authentication API",
        Version = "v1"
    });

    c.SwaggerDoc("cine", new OpenApiInfo
    {
        Title = "Cine API",
        Version = "v1"
    });

    c.SwaggerDoc("filters", new OpenApiInfo
    {
        Title = "Filters API",
        Version = "v1"
    });

    c.DocInclusionPredicate((documentName, apiDescription) =>
        string.Equals(apiDescription.GroupName, documentName, StringComparison.OrdinalIgnoreCase)
    );

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token JWT}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/authentication/swagger.json", "Authentication API");
        c.SwaggerEndpoint("/swagger/cine/swagger.json", "Cine API");
        c.SwaggerEndpoint("/swagger/filters/swagger.json", "Filters API");

        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();