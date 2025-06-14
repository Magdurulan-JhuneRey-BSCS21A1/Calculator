using System;
using System.Linq.Expressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Expression:");
        string expression = Console.ReadLine();
        Console.WriteLine(Calculate(ShuntingYard(Tokenize(expression))));
        
    }

    static List<string> Tokenize(string expression)
    {
        List<string> token = new List<string>();
        string number = "";
        foreach (char ch in expression)
        {
            if (char.IsDigit(ch))
            {
                number += ch;
            }
            else
            {
                token.Add(number);
                number = "";
                token.Add(ch.ToString());
            }
        }
        token.Add(number);
        // Inside Tokenize
        Console.WriteLine("Tokens from Tokenize:");
        foreach (var t in token) Console.Write($"[{t}] ");

        return token;
    }

    static List<string> ShuntingYard(List<string> token)
    {
        // Inside ShuntingYard
        Console.WriteLine("Tokens in ShuntingYard:");
        foreach (var t in token) Console.Write($"[{t}] ");

        List<string> output = new List<string>();
        List<string> stack = new List<string>();
        string[] precedence = new string[] { "-", "+", "/", "*" };

        foreach (string str in token)
        {
            if (precedence.Contains(str))
            {
                if (stack.Count == 0)
                {
                    stack.Add(str);
                }
                else
                {
                    for (int i = stack.Count; i > 0; i--)
                    {
                        if (Array.IndexOf(precedence, stack[i - 1]) < Array.IndexOf(precedence, str))
                        {
                            stack.Add(str);
                            break;
                        }
                        else
                        {
                            output.Add(stack[i - 1]);
                            stack.RemoveAt(i - 1);
                            if (stack.Count == 0)
                            {
                                stack.Add(str);
                            }
                        }
                    }
                }
            }
            else
            {
                output.Add(str);
            }
        }
        while (stack.Count != 0)
        {
            output.Add(stack[stack.Count - 1]);
            stack.RemoveAt(stack.Count - 1);
        }
        Console.WriteLine("Output:");
        foreach (var o in output) Console.Write($"[{o}] ");
        return output;
    }

    static double Calculate(List<string> output)
    {
        /*foreach (string ss in output)
        {
            Console.Write(ss);
        }*/
        Stack<double> result = new Stack<double>();
        foreach (string str in output)
        {
            if (double.TryParse(str, out double number))
            {
                result.Push(number);
            }
            else
            {
                double num1 = result.Pop();
                double num2 = result.Pop();

                switch (str)
                {
                    case "+":
                        result.Push(num2 + num1);
                        break;
                    case "-":
                        result.Push(num2 - num1);
                        break;
                    case "/":
                        result.Push(num2 / num1);
                        break;
                    case "*":
                        result.Push(num2 * num1);
                        break;
                }
            }
        }
        return result.Pop();
    }
}