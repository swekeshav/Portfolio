using Portfolio.Web.Services;

namespace Portfolio.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        // Add services to the container.
        services.AddControllersWithViews();

        services.AddHttpClient("opentdb",
            httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://opentdb.com/api.php?");
            });

        services.AddScoped<ITriviaClientService, TriviaClientService>();
        services.AddScoped<ITriviaService, TriviaService>();

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
    }
}