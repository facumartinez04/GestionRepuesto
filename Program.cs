using GestionRepuestoAPI.RepuestosMapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestionRepuestoAPI.Repository.Interfaces;
using GestionRepuestoAPI.Repository;
using Microsoft.Extensions.DependencyInjection;

using GestionRepuestoAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConectionSQL")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRepuestoRepository, RepuestoRepository>();
builder.Services.AddScoped<IUsuarioPermisoRepository, UsuarioPermisoRepository>();
builder.Services.AddScoped<IUsuarioRolRepository, UsuarioRolRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IRolPermisoRepository, RolPermisoRepository>();

builder.Services.AddAutoMapper(typeof(RtoMapper));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.WithOrigins("http://localhost:7268", "http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsAdministrador", policy =>
     policy.RequireRole("Administrador"));

    options.AddPolicy("CrearRepuesto", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "repuesto.crear")));

    options.AddPolicy("EditarRepuesto", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "repuesto.editar")));

    options.AddPolicy("ListarRepuesto", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "repuesto.listar")));

    options.AddPolicy("EliminarRepuesto", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "repuesto.eliminar")));

    options.AddPolicy("ListarUsuario", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "usuario.listar")));

    options.AddPolicy("CrearUsuario", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "usuario.crear")));

    options.AddPolicy("EditarUsuario", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "usuario.editar")));

    options.AddPolicy("EliminarUsuario", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Administrador") ||
            context.User.HasClaim("Permiso", "usuario.eliminar")));

    options.AddPolicy("ModuloUsuarios", policy =>
     policy.RequireAssertion(context =>
         context.User.IsInRole("Administrador") ||
         context.User.HasClaim("Permiso", "usuario.modulo")));

    options.AddPolicy("ModuloRoles", policy =>
 policy.RequireAssertion(context =>
     context.User.IsInRole("Administrador") ||
     context.User.HasClaim("Permiso", "usuario.roles")));
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
