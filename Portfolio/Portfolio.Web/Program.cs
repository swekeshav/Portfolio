using Portfolio.Web.Data;
using Portfolio.Web.Models;
using Portfolio.Web.Services;
using Serilog;

namespace Portfolio.Web;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Warning()
				.WriteTo.Console()
				.WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

		builder.Host.UseSerilog();

		var services = builder.Services;

		// Add services to the container.
		services.AddControllersWithViews()
			.AddRazorRuntimeCompilation();

		services.AddHttpClient("opentdb",
			httpClient =>
			httpClient.BaseAddress = new Uri("https://opentdb.com/api.php?"));

		services.AddScoped<ITriviaClientService, TriviaClientService>();
		services.AddScoped<ITriviaService, TriviaService>();
		services.AddScoped<ITodosService, TodosService>();
		services.AddScoped<IRepository<TodoViewModel>, TodosRepository>();

		services.AddSingleton<IExceptionPolicy, ExceptionPolicy>();
		services.AddExceptionHandler<RazorExceptionHandler>();
		services.AddExceptionHandler<APIExceptionHandler>();
		services.AddProblemDetails();

		var app = builder.Build();

		app.UseExceptionHandler();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			//app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		//pattern: "{controller=Trivia}/{action=ShowTrivia}");

		app.Run();
	}
}