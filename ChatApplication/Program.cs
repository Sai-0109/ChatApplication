using ChatApplication.AuthService;
using ChatApplication.ChatHubs;
using ChatApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSignalR();
builder.Services.AddSession();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());
           
});

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
app.UseCors("AllowAll");
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/ChatApplication"); // Map the ChatHub
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=login}/{action=Index}/{id?}");
 
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=User}/{action=Register}/{id?}");

app.Run();
