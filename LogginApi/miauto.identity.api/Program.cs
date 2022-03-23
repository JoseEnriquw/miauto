using miauto.identity.api.Config;
using miauto.identity.api.Models.Common;
using miauto.identity.api.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
NHibernateUtil.Start();

//Configuracion Posgre
var postgreSQLConnectionConfiguration = new PostgreSQLConfiguration(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
builder.Services.AddSingleton(postgreSQLConnectionConfiguration);

//Inyeccion de dependencias
builder.Services.AddScoped<IUserRepositories,UserRepositories>();
builder.Services.AddControllers();

//
var appSettingsSetion = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSetion);

//JWT
var appSettings= appSettingsSetion.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(d =>
{
    d.RequireHttpsMetadata= false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(llave),
        ValidateIssuer = false,
        ValidateAudience = false

    };

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
