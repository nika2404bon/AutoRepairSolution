using AutoRepairService.Data;
using AutoRepairService.Data.Repositories;
using AutoRepairService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure JSON file paths
var clientsFilePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "clients.json");
var servicesFilePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "services.json");
var appointmentsFilePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "appointments.json");

// 1. Регистрация репозиториев (Singleton, так как они работают с файлами)
builder.Services.AddSingleton<IClientRepository>(provider => 
    new ClientRepository(clientsFilePath));
builder.Services.AddSingleton<IServiceRepository>(provider => 
    new ServiceRepository(servicesFilePath));

// Регистрация AppointmentRepository с зависимостями
builder.Services.AddSingleton<IAppointmentRepository>(provider => 
    new AppointmentRepository(
        appointmentsFilePath,
        provider.GetRequiredService<IClientRepository>(),
        provider.GetRequiredService<IServiceRepository>()));

// 2. Регистрация DataContext (если он используется)
builder.Services.AddSingleton<DataContext>();

// 3. Регистрация сервисов (Scoped для HTTP-запроса)
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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