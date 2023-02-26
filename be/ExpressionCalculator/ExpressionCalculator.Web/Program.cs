using ExpressionCalculator.Web.Services;
using ExpressionCalculator.Web.Services.Interfaces;

namespace ExpressionCalculator.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<ICalculatorService, CalculatorService>();
			builder.Services.AddTransient<IValidationService, ValidationService>();

			builder.Services.AddCors(c =>
			{
				c.AddPolicy(name: "AllowOrigins",
				 options =>
				 {
					 options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				 });

			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}