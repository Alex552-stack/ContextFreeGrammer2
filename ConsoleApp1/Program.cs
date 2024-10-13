using System.Text.Json;
using ConsoleApp1.Classes;

var fileStream = new FileStream("input.json", FileMode.Open);
var reader = new StreamReader(fileStream);
var context = new CFG();

var text = reader.ReadToEnd();
context = JsonSerializer.Deserialize<CFG>(text);
// Random random = new();
//
//
// while (!reader.EndOfStream)
// {
//     var line = reader.ReadLine();
//     if(line == null) continue;
//     
//     var parsedInput = Utils.ParseRulesFrom(line);
//     context.Rules.Add($"{parsedInput.Item1}", parsedInput.Item2);
// }

context.Derivate();

