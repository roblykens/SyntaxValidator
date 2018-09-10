using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValidateSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            //test data for checking syntax
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
                var sytaxValidationResults = ValidateSyntaxRules(testList);
                foreach (var syntaxResult in sytaxValidationResults)
                {
                    Console.ForegroundColor = syntaxResult == "YES" ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(syntaxResult);
                }
            } while (Console.ReadLine() != null);
        }


        static List<string> ValidateSyntaxRules(List<string> sytaxLines)
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

            const string openSyntax = "{[(";
            const string closeSyntax = "}])";

            var syntaxSet = new Stack<int>();

            foreach (char syntax in sytaxLine)
            {
                if (openSyntax.Contains(syntax))
                {
                    var syntaxFound = openSyntax.IndexOf(syntax);
                    syntaxSet.Push(syntaxFound);
                }
                else if (closeSyntax.Contains(syntax) && syntaxSet.Count > 0)
                {
                    if (!syntaxSet.Pop().Equals(closeSyntax.IndexOf(syntax)))
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


