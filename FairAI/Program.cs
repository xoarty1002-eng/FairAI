using FairAI;
var cd = new CoreDepth(8);
var lp = new LanguagePool();
var dp = new DepthPool(32);
while (true)
{
    var request = Console.ReadLine();
    var response = "answer: "+ request;
    Console.WriteLine(response);
}