
namespace Hangfire.WebAPI
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var configuration = builder.Configuration;

			var connectionString = configuration["ConnectionString"];

			// Add services to the container.
			builder.Services.AddHangfire(a =>
			a.UseSqlServerStorage(configuration["ConnectionString"]));
			builder.Services.AddHangfireServer();


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseHangfireDashboard();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
