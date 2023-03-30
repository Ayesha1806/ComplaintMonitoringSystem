using BusinessLogicLayer.Repository.Contracts;
using BusinessLogicLayer.Repository.Services;
using DataAccessLayer.DbContext;
using DataAccessLayer.Repository.Contracts;
using DataAccessLayer.Repository.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<ComplaintMonitoringSystemContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));
builder.Services.AddScoped<IAuthenticationDataAccessLayer, AuthenticationServicesDataAccessLayer>();
builder.Services.AddScoped<IAuthenticationBusinessLayer, AuthenticationServicesBusinessLayer>();
builder.Services.AddScoped<IComplaintDataAccessLayer,ComplaintServicesDataLayer>();
builder.Services.AddScoped<IComplaintBusinessLayercs, ComplaintServicesBusinessLayer>();
builder.Services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ComplaintMonitoringSystemContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project_Monolithic", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
 {
 new OpenApiSecurityScheme
 {
 Reference=new OpenApiReference
 {
 Type=ReferenceType.SecurityScheme,
 Id="Bearer"
 }
 },
 new string[]{}
 }
 });
});
builder.Services.AddCors(p=>p.AddPolicy("corspolicy",build =>
{
    build.WithOrigins("https://localhost:7145").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComplaintMonitoringSystem");
        c.RoutePrefix = string.Empty;
    }
   );
}
app.UseCors("corspolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
