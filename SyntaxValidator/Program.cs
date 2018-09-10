using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValidateBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            //test data for checking Brackets
            var testList = new List<string>();
            testList.Add("[]{}()"); //YES
            testList.Add("[{]}"); //NO
            testList.Add("[{}[]]"); //YES
            testList.Add("{[{[]}]]}({})"); //NO
            testList.Add("[{{}[}](}[])"); //NO
            testList.Add("(}){"); //NO
            testList.Add("({)}"); //NO
            testList.Add("{]}["); //NO
            testList.Add("{{()}}{}[]"); //YES
            testList.Add("{}"); //YES
            testList.Add(""); //NO
            testList.Add("(..u9)"); //NO
            testList.Add(")[]"); //NO
            testList.Add("()[]}"); //NO

            do
            {
                var bracketsValidationResults = ValidateBracketsRules(testList);
                foreach (var bracketsResult in bracketsValidationResults)
                {
                    Console.ForegroundColor = bracketsResult == "YES" ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(bracketsResult);
                }
            } while (Console.ReadLine() != null);
        }


        static List<string> ValidateBracketsRules(List<string> bracketsLines)
        {
            var results = new List<string>();

            foreach (var line in bracketsLines)
            {
                results.Add(ValidateBracketsLine(line) ? "YES" : "NO");
            }

            return results;
        }


        static bool ValidateBracketsLine(string bracketsLine)
        {
            if (string.IsNullOrEmpty(bracketsLine))
                return false;

            const string openBrackets = "{[(";
            const string closeBrackets = "}])";

            var bracketsSet = new Stack<int>();

            foreach (char brackets in bracketsLine)
            {
                if (openBrackets.Contains(brackets))
                {
                    var BracketsFound = openBrackets.IndexOf(brackets);
                    bracketsSet.Push(BracketsFound);
                }
                else if (closeBrackets.Contains(brackets) && bracketsSet.Count > 0)
                {
                    if (!bracketsSet.Pop().Equals(closeBrackets.IndexOf(brackets)))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}


