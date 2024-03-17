using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Phonebook.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DepartmentRepository>();
builder.Services.AddSingleton<EmployeeRepository>();
builder.Services.AddControllersWithViews(); 

var app = builder.Build();

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
