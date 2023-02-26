using ExpressionCalculator.Web.Services.Interfaces;

namespace ExpressionCalculator.Web.Services
{
	public class CalculatorService : ICalculatorService
	{
		public double Evaluate(string expression)
		{
			expression = expression.Replace(" ",string.Empty);
			char[] arr = expression.ToCharArray();

			Stack<double> values = new Stack<double>();
			Stack<char> operators = new Stack<char>();

			operators.Push('('); // Implicit opening parenthesis

			int pos = 0;
			while (pos <= arr.Length)
			{
				if (pos == arr.Length || arr[pos] == ')')
				{
					ProcessClosingParenthesis(values, operators);
					pos++;
				}
				else if (arr[pos] >= '0' && arr[pos] <= '9')
				{
					pos = ProcessInputNumber(arr, pos, values);
				}
				else
				{
					ProcessInputOperator(arr[pos], values, operators);
					pos++;
				}

			}

			return values.Pop();
		}

		#region Private
		private static void ProcessClosingParenthesis(Stack<double> vStack,
								Stack<char> opStack)
		{

			while (opStack.Peek() != '(')
				ExecuteOperation(vStack, opStack);

			opStack.Pop(); // Remove the opening parenthesis

		}

		private static int ProcessInputNumber(char[] exp, int pos,
						Stack<double> vStack)
		{

			int value = 0;
			while (pos < exp.Length &&
					exp[pos] >= '0' && exp[pos] <= '9')
				value = 10 * value + (int)(exp[pos++] - '0');

			vStack.Push(value);

			return pos;

		}
		private static void ProcessInputOperator(char op, Stack<double> vStack,
							Stack<char> opStack)
		{

			while (opStack.Count > 0 &&
					OperatorCausesEvaluation(op, opStack.Peek()))
				ExecuteOperation(vStack, opStack);

			opStack.Push(op);

		}

		private static bool OperatorCausesEvaluation(char op, char prevOp)
		{

			bool evaluate = false;

			switch (op)
			{
				case '+':
				case '-':
					evaluate = (prevOp != '(');
					break;
				case '*':
				case '/':
					evaluate = prevOp == '*' || prevOp == '/';
					break;
				case ')':
					evaluate = true;
					break;
			}

			return evaluate;

		}

		public static void ExecuteOperation(Stack<double> vStack,
								Stack<char> opStack)
		{

			double rightOperand = vStack.Pop();
			double leftOperand = vStack.Pop();
			char currentOperator = opStack.Pop();

			double result = 0;
			switch (currentOperator)
			{
				case '+':
					result = leftOperand + rightOperand;
					break;
				case '-':
					result = leftOperand - rightOperand;
					break;
				case '*':
					result = leftOperand * rightOperand;
					break;
				case '/':
					result = leftOperand / rightOperand;
					break;
			}

			vStack.Push(result);
		}
		#endregion
	}
}
