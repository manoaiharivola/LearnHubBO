using LearnHubBackOffice.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));

builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        var formateurId = context.Session.GetString("FormateurId");
        var formateurNom = context.Session.GetString("FormateurNom");
        if (!string.IsNullOrEmpty(formateurNom) && !string.IsNullOrEmpty(formateurId))
        {
            context.Response.Redirect("/Courses");
        }
        else
        {
            context.Response.Redirect("/Formateurs/Login");
        }
    }
    else
    {
        await next();
    }
});

app.MapRazorPages();

app.Run();
