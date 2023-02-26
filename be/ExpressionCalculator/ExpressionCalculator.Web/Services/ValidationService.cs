using ExpressionCalculator.Web.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Web.Services
{

    public class ValidationService : IValidationService
    {
        public bool Validate(string expression)
        {
            var regex = new Regex(@"^[^A-z!@#$%^&\\]*$");

            bool isValid = regex.Match(expression).Success;
            return isValid;
		}
    }
}
