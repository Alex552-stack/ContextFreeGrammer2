using System.Diagnostics.Tracing;

namespace ConsoleApp1.Classes;
//Context free grammer
public class CFG
{
    private readonly int HardLimitOnIteration = 50; 
    private readonly bool IsLeftRecursive = false;
    private readonly Random randon = new();
    public Dictionary<string, List<string>> Rules { get; set; } = new();
    
    public char StartSymbol { get; set; }
    public HashSet<string> Terminals { get; set; }
    public HashSet<string> NonTerminals { get; set; }
    
    public void DetermineTAndNonT()
    {
        Terminals = new HashSet<string>();
        NonTerminals = new HashSet<string>();
        foreach (var rule in Rules)
        {
            NonTerminals.Add(rule.Key);
            foreach (var expression in rule.Value)
            {
                foreach (var c in expression)
                {
                    if (NonTerminals.Contains($"{c}") || c == 'ε' || c == '|') continue;
                    Terminals.Add($"{c}");
                }
            }
        }
    }

    public void Derivate()
    {
        string currentString = StartSymbol.ToString();
        int iteration = 0;
        Console.Write(currentString);
        bool hasEndedSuccesfully = false;
        while (iteration < HardLimitOnIteration)
        {
            iteration++;
            var index = GetIndexOfNonTerminalToDerivate(currentString);
            if (index == -1)
            {
                hasEndedSuccesfully = true;
                return;
            };

            var chosenRule = ChooseRuleForNonTerminal($"{currentString[index]}");
            
            if (currentString.Length == 1)
                currentString = chosenRule;
            else
                currentString = $"{currentString.Substring(0, index)}{chosenRule}{currentString.Substring(index + 1)}";
            
            Console.Write("->");
            index = GetIndexOfNonTerminalToDerivate(currentString);
            PrintWithHighlight(currentString, index);
        }

        if (!hasEndedSuccesfully)
        {
            Console.Clear();
            Derivate();
        }
    }

    private void PrintWithHighlight(string output, int startIndexOfHighlight)
    {
        int buffer = 0;
        int lastPrint = -1;
        for(int i = 0; i < output.Length; i++)
        {
            if (i == startIndexOfHighlight)
            {
                Console.Write(output.Substring(lastPrint + 1, buffer));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(output[i]);
                Console.ResetColor();
                buffer = 0;
                lastPrint = i;
            }
            else
            {
                buffer++;
            }
        }
        if(buffer != 0)
            Console.Write(output.Substring(lastPrint + 1, buffer));
    }

    private string ChooseRuleForNonTerminal(string nonTerminal)
    {
        if (Rules.Keys.All(cheie => cheie != nonTerminal))
            throw new Exception($"No rule found for the 'nonTerminal' {nonTerminal}");

        int nrOfRules = Rules[nonTerminal].Count;
        return Rules[nonTerminal][randon.Next(nrOfRules)];
    }

    private int GetIndexOfNonTerminalToDerivate(string currentString)
    {
        if (!IsLeftRecursive)
            currentString = new string(currentString.Reverse().ToArray());

        for (int i = 0; i < currentString.Length; i++)
        {
            if (NonTerminals.Contains($"{currentString[i]}"))
            {
                return IsLeftRecursive ? i : currentString.Length - i - 1;
            }
        }
        return -1;
    }
    
    
}