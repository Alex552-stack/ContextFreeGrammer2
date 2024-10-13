namespace ConsoleApp1.Classes;

public class Utils
{
    public static (char,List<string>) ParseRulesFrom(string line)
    {
        var leftPart = line.Split("->")[0].Trim();
        var rightPart = line.Split("->")[1].Trim();
        
        List<string> expressions = rightPart.Split("|").ToList();
        expressions.ForEach(s => s = s.Trim());
        return (leftPart[0], expressions);
    }
}