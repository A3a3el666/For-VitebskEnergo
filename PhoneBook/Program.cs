using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Phonebook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DepartmentRepository>();
builder.Services.AddSingleton<EmployeeRepository>();// Register DepartmentRepository as a singleton
builder.Services.AddControllersWithViews(); // Add controller and view services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
