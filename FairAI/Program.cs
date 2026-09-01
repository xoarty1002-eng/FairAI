using FairAI;
using System;

var cd = new CoreDepth(8);
var lp = new LanguagePool();
var dp = new DepthPool(32);

Console.WriteLine("FairAI Pipeline Framework Active. Enter prompts:");

while (true)
{
    Console.Write("\nUser > ");
    var request = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(request)) continue;
    if (request.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

    try
    {
        StateModel processingState = lp.Calculate(request);
        NodeModel lowerNode = dp.Down(processingState);
        NodeModel verifiedNode = cd.Check(lowerNode);
        processingState = dp.Up(verifiedNode);
        var aiResult = lp.Generate(processingState);
        Console.WriteLine($"AI > {aiResult}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Pipeline Error: {ex.Message}");
    }
}