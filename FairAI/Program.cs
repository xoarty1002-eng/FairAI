using FairAI;
using System;
using System.Text.Json;

var cd = new CoreService(8);
if (LoadCore() != null)
{
    cd.Cores = LoadCore();
}
var lp = new LanguageService();
if (LoadLanguage() != null)
{
    lp.Data = LoadLanguage();
}
var dp = new ArtificialService(32);
if (LoadAI() != null)
{
    dp.Pool = LoadAI();
}

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
    SaveText(lp.Data);
    SaveCore(cd.Cores);
    SaveAI(dp.Pool);
}
void SaveText(List<TextModel> data)
{
    string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

    // Write the text string directly to a file path
    File.WriteAllText("LanguageService.json", jsonString);
}
void SaveCore(List<CoreModel> data)
{
    string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

    // Write the text string directly to a file path
    File.WriteAllText("CoreService.json", jsonString);
}
void SaveAI(List<NeuronModel> data)
{
    string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

    // Write the text string directly to a file path
    File.WriteAllText("ArtificialService.json", jsonString);
}
List<TextModel>? LoadLanguage()
{
    string filePath = "LanguageService.json";

    if (File.Exists(filePath))
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<TextModel>>(jsonString) ?? null;
    }
    return null;
}
List<CoreModel>? LoadCore()
{
    string filePath = "CoreService.json";

    if (File.Exists(filePath))
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<CoreModel>>(jsonString) ?? null;
    }
    return null;
}
List<NeuronModel>? LoadAI()
{
    string filePath = "ArtificialService.json";

    if (File.Exists(filePath))
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<NeuronModel>>(jsonString) ?? null;
    }
    return null;
}