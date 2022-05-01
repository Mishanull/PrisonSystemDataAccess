using DAOInterfaces;
using FileContext.Guards;
using FileContext.Prisoners;
using FileContext.Users;
using Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserFileDAO>();
builder.Services.AddScoped<UserFileContext>();
builder.Services.AddScoped<IGuardService, GuardFileDAO>();
builder.Services.AddScoped< GuardFileContext>();
builder.Services.AddScoped<IPrisonerService, PrisonerFileDAO>();
builder.Services.AddScoped<PrisonerFileContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();