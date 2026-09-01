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
        // 1. Initialize a temporary blank state context for this input text iteration
        StateModel processingState = lp.Calculate(request);

        // 2. Use your 32-neuron DepthPool (dp) instance to step down the data track
        NodeModel lowerNode = dp.Down(processingState);

        // 3. Extract the first generated core from your 8-core CoreDepth (cd) instance to run validation
        NodeModel verifiedNode = cd.Check(lowerNode);

        // 4. Return the validation metrics up through the DepthPool structural pipeline
        processingState = dp.Up(verifiedNode);

        // 5. Use LanguagePool (lp) to generate a dynamic response from the request and governance tracking state
        // (Adjust the method name below to match what is declared inside your ILanguage.cs / LanguagePool.cs)
        var aiResult = lp.Generate(processingState);

        // 6. Output secure processed response
        Console.WriteLine($"AI > {aiResult}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Pipeline Error: {ex.Message}");
    }
}