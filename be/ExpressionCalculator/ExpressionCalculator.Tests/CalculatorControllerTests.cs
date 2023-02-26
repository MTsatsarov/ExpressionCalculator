using ExpressionCalculator.Web.Controllers;
using ExpressionCalculator.Web.Models;
using ExpressionCalculator.Web.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace ExpressionCalculator.Tests
{
	[TestFixture]
	public class CalculatorControllerTests
	{
		private CalculatorController controller;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var validationService = new ValidationService();
			var calculatorService = new CalculatorService();
			this.controller = new CalculatorController(calculatorService, validationService);
		}

		[Test]
		[TestCase("@ + 1")]
		[TestCase("$+ 1")]
		[TestCase("%+ 1")]
		[TestCase("test+ 1")]
		[TestCase("\\ + 1")]
		public void WhenReceiveInvalidExpression_ReturnBadRequest(string expression)
		{
			//Arrange
			var expressionModel = new ExpressionInputModel();
			expressionModel.Expression = expression;

			var expectedResult = "Invalid expression.";

			//Act
			var response = this.controller.Evaluate(expressionModel);

			//Assert
			Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
			var objResult = response as BadRequestObjectResult;

			Assert.That(objResult.Value, Is.EqualTo(expectedResult));
		}

		[Test]
		[TestCase("*",2)]
		[TestCase("+",3)]
		[TestCase("-",-1)]
		[TestCase("/",0.5)]
		public void ShouldCalculateExpression_WithAllDelimiters(string delimiter, double result)
		{
			//Arrange
			var expression = $"(1 {delimiter} ( 1 + 1 ))";
			var expressionModel = new ExpressionInputModel();
			expressionModel.Expression = expression;
			//Act
			var response = this.controller.Evaluate(expressionModel);

			//Assert
			Assert.That(response, Is.InstanceOf<OkObjectResult>());
			var objResult = response as OkObjectResult;

			Assert.That(objResult.Value, Is.EqualTo(result));
		}

		[Test]
		public void ShouldCalculateExpressionWithNestedParenthses()
		{
			//Arrange
			var expression = "(1+(1+1))";
			var expressionModel = new ExpressionInputModel();
			expressionModel.Expression = expression;
			var result = 3;

			//Act
			var response = this.controller.Evaluate(expressionModel);

			//Assert
			Assert.That(response, Is.InstanceOf<OkObjectResult>());
			var objResult = response as OkObjectResult;

			Assert.That(objResult.Value, Is.EqualTo(result));
		}
	}
}
