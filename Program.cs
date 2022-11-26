using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FirebaseConnector.Controllers;

//Doctor dr = new Doctor("Dr. Ozz");
//await dr.givePrescriptions();
//await dr.viewPatientInformation();
//await dr.viewAppointments();
//await dr.readComments();
//await dr.writeComments("Amsich", "Hello");


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
