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
                var sytaxValidationResults = ValidateBracketsRules(testList);
                foreach (var BracketsResult in sytaxValidationResults)
                {
                    Console.ForegroundColor = BracketsResult == "YES" ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(BracketsResult);
                }
            } while (Console.ReadLine() != null);
        }


        static List<string> ValidateBracketsRules(List<string> sytaxLines)
        {
            var results = new List<string>();

            foreach (var line in sytaxLines)
            {
                results.Add(ValidateSytaxLine(line) ? "YES" : "NO");
            }

            return results;
        }


        static bool ValidateSytaxLine(string sytaxLine)
        {
            if (string.IsNullOrEmpty(sytaxLine))
                return false;

            const string openBrackets = "{[(";
            const string closeBrackets = "}])";

            var BracketsSet = new Stack<int>();

            foreach (char Brackets in sytaxLine)
            {
                if (openBrackets.Contains(Brackets))
                {
                    var BracketsFound = openBrackets.IndexOf(Brackets);
                    BracketsSet.Push(BracketsFound);
                }
                else if (closeBrackets.Contains(Brackets) && BracketsSet.Count > 0)
                {
                    if (!BracketsSet.Pop().Equals(closeBrackets.IndexOf(Brackets)))
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


