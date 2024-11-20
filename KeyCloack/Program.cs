using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.OpenApi.Models;

//credits: https://dev.to/gloinho/autenticacao-e-autorizacao-de-uma-asp-net-web-api-com-keycloak-loe#cria%C3%A7%C3%A3o-de-grupos-e-roles-e-associa%C3%A7%C3%A3o-de-usu%C3%A1rios-e-grupos

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(cf =>
{
    cf.SwaggerDoc("v1", new OpenApiInfo { Title = "Keycloack Demo", Version = "v1" });

    // Configuração do esquema de segurança JWT para o Swagger
    cf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Altere para Http para indicar que é um esquema de autenticação HTTP com Bearer
        Scheme = "bearer", // Especifique "bearer" para indicar o formato JWT
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Por favor, insira o token JWT no campo 'Authorization' usando o prefixo 'Bearer '"
    });

    cf.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var authenticationOptions = builder
                            .Configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

builder.Services.AddKeycloakAuthentication(authenticationOptions!);


var authorizationOptions = builder
                            .Configuration
                            .GetSection(KeycloakProtectionClientOptions.Section)
                            .Get<KeycloakProtectionClientOptions>();

builder.Services.AddKeycloakAuthorization(authorizationOptions!);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
