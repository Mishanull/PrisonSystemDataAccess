using DAOInterfaces;
using EfcData.Context;
using EfcData.DAO;
using Entities;
using FileContext.Alerts;
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

builder.Services.AddScoped<IUserService, UserDAO>();
builder.Services.AddScoped<IGuardService, GuardDAO>(); ;
builder.Services.AddScoped<IPrisonerService, PrisonerDAO>();
builder.Services.AddScoped<IWorkShiftService, WorkShiftDAO>();
builder.Services.AddScoped<IVisitService, VisitDAO>();
builder.Services.AddScoped<IAlertService, AlertFileDAO>();
builder.Services.AddScoped<AlertFileContext>();

builder.Services.AddDbContext<PrisonSystemContext>();

/*
builder.Services.AddScoped<IUserService, UserDAO>();
builder.Services.AddScoped<UserFileContext>();
builder.Services.AddScoped<IGuardService, GuardDAO>();
builder.Services.AddScoped< GuardFileContext>();
builder.Services.AddScoped<IPrisonerService, PrisonerDAO>();
builder.Services.AddScoped<PrisonerFileContext>();
*/


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