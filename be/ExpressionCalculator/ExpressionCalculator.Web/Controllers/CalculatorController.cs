using ExpressionCalculator.Web.Models;
using ExpressionCalculator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpressionCalculator.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CalculatorController : ControllerBase
	{
		private readonly ICalculatorService calculatorService;
		private readonly IValidationService validationService;

		public CalculatorController(ICalculatorService calculatorService, IValidationService validationService)
		{
			this.calculatorService = calculatorService;
			this.validationService = validationService;
		}

		[HttpPost]
		public IActionResult Evaluate([FromBody] ExpressionInputModel model)
		{
			if (!validationService.Validate(model.Expression))
			{
				return this.BadRequest("Invalid expression.");
			}

			var result = this.calculatorService.Evaluate(model.Expression);
			return this.Ok(result);
		}
	}
}
