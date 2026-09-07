using FairAI;
using System;

var cd = new CoreService(8);
var lp = new LanguageServicel();
var dp = new ArtificialService(32);

Console.WriteLine("FairAI Pipeline Framework Active. Enter prompts:");

while (true)
{
    Console.Write("\nUser > ");
    var request = Console.ReadLine();
    LanguageModel processingState = lp.Calculate(request);
    TermModel lowerNode = dp.Down(processingState);
    TermModel verifiedNode = cd.Check(lowerNode);
    processingState = dp.Up(verifiedNode);
    var aiResult = lp.Generate(processingState);
    Console.WriteLine($"AI > {aiResult}");
}