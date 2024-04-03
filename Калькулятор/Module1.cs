using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Калькулятор
{
    public class Module1
    {
        public static double Calc(string postfixStr)
        {
            Stack<double> locals = new Stack<double>();
            double number = 0;
            bool flag = false;
            for (int i = 0; i < postfixStr.Length; i++)
            {
                char c = postfixStr[i];

                if (Char.IsDigit(c))
                {
                    if (i != 0 && postfixStr[i - 1] == '-')
                        flag = true;
                    string dop = GetStringNumber(ref postfixStr, ref i);
                    number = Convert.ToDouble(dop);
                    if (flag)
                    {
                        locals.Push(-number);
                        flag = false;
                    }
                    else
                        locals.Push(number);
                }
                else if (operationPriority.ContainsKey(c) && (postfixStr[i] != '-' || (i == postfixStr.Length - 1 || !Char.IsDigit(postfixStr[i + 1]))))
                {

                    double second = locals.Count > 0 ? locals.Pop() : 0;
                    double first = 0;
                    if (c != '!')
                        first = locals.Count > 0 ? locals.Pop() : 0;
                    locals.Push(Execute(c, first, second));
                }
            }
            if (locals.Count > 0)
                return locals.Pop();
            else
            {
                Form1.IsError = true;
                return 0;
            }
        }

        private static double Execute(char op, double first, double second)
        {
            switch (op)
            {
                case '+':
                    return (first + second);
                case '-':
                    return (first - second);
                case '*':
                    return (first * second);
                case '/':
                    return (first / second);
                case '^':
                    return (Math.Pow(first, second));
                case '!':
                    return (Factorial(second));
                default:
                    return 0;
            };
        }

        public static Dictionary<char, int> operationPriority = new Dictionary<char, int>() {
        {'(', 0},
        {'+', 1},
        {'-', 1},
        {'*', 2},
        {'/', 2},
        {'^', 3},
        {'!', 3},
        };

        public static string GetStringNumber(ref string str, ref int pos)
        {
            string strNumber = "";

            for (; pos < str.Length; pos++)
            {
                char num = str[pos];
                if (Char.IsDigit(num) || str[pos] == ',')
                    strNumber += num;
                else
                {
                    pos--;
                    break;
                }
            }
            return strNumber;
        }
        public static string ToPostfix(string infixStr)
        {
            string postfixStr = "";
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < infixStr.Length; i++)
            {
                char c = infixStr[i];

                if (Char.IsDigit(c))
                {
                    if (i != 0 && infixStr[i - 1] == '-')
                    {
                        postfixStr += "-";
                        StringBuilder sb = new StringBuilder(infixStr);
                        sb[i - 1] = ' ';
                        infixStr = sb.ToString();
                    }
                    postfixStr += GetStringNumber(ref infixStr, ref i) + " ";
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        postfixStr += stack.Pop() + " ";
                    if (stack.Count > 0)
                        stack.Pop();
                    else
                    {
                        Form1.IsError = true;
                        return "";
                    }
                }
                else if (operationPriority.ContainsKey(c) && (infixStr[i] != '-' || i+1 != infixStr.Length && !Char.IsDigit(infixStr[i + 1])))
                {
                    char op = c;

                    while (stack.Count > 0 && (operationPriority[stack.Peek()] >= operationPriority[op]))
                        postfixStr += stack.Pop() + " ";
                    stack.Push(op);
                }
            }
            foreach (char op in stack)
                postfixStr += op;
            return postfixStr;
        }
        public static double Factorial(double number)
        {   if (number % 1 == 0 && number >= 0)
            {
                if (number == 0)
                    return 1;
                else
                    return number * Factorial(number - 1);
            }
            else
            {
                Form1.IsError = true;
                return 0;
            }
        }
    }
}
