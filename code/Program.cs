// This sample requires C# 7.1 or later for async/await.

using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// Install Newtonsoft.Json with NuGet
using Newtonsoft.Json;

namespace TranslateConsoleApp
{
    /// <summary>
    /// The C# classes that represents the JSON returned by the Translator Text API.
    /// </summary>
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public TextResult SourceText { get; set; }
        public Translation[] Translations { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }
        public float Score { get; set; }
    }

    public class TextResult
    {
        public string Text { get; set; }
        public string Script { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public TextResult Transliteration { get; set; }
        public string To { get; set; }
        public Alignment Alignment { get; set; }
        public SentenceLength SentLen { get; set; }
    }

    public class Alignment
    {
        public string Proj { get; set; }
    }

    public class SentenceLength
    {
        public int[] SrcSentLen { get; set; }
        public int[] TransSentLen { get; set; }
    }

    class Program
    {
        static string subscriptionKey = "";
        static string endpoint = "";
        static string region = "";
        static string inputFile = "";
        static string outputFile = "";

        private static string GetSetting(string settingName)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[settingName].ToString();
        }

        static Program()
        {
            if (null == subscriptionKey || 
                null == endpoint || 
                null == region || 
                null == inputFile || 
                null == outputFile)
                throw new Exception("Please specify all the Application Configuration settings.");
        }

        static public async Task TranslateText(string route, string[] inputs)
        {
            Console.WriteLine("Translating...");
            List <string> urduTranslations = new List<string>();

            foreach (string input in inputs)
            {
                // Build a body for the Http request
                object[] body = new object[] { new { Text = input } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the Http request
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", region);

                    // Get service response for the request
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    // Read Http response message as string
                    string serviceResult = await response.Content.ReadAsStringAsync();
                    TranslationResult[] translationResults = JsonConvert.DeserializeObject<TranslationResult[]>(serviceResult);
                    
                    foreach (TranslationResult translationResult in translationResults)
                        foreach (Translation translation in translationResult.Translations)
                            urduTranslations.Add(translation.Text);
                }
            }
            File.WriteAllLines(outputFile, urduTranslations.ToArray());
            Console.WriteLine("Urdu translation complete.");
            Console.WriteLine("Press any key to continue...");
        }

        static async Task Main(string[] args)
        {
            try
            {
                subscriptionKey = GetSetting("SubscriptionKey");
                endpoint = GetSetting("Endpoint");
                region = GetSetting("Region");
                inputFile = GetSetting("InputFile");
                outputFile = GetSetting("OutputFile");

                string route = "/translate?api-version=3.0&to=ur";
                Console.WriteLine("Starting translation...");
                
                string[] inputs = File.ReadAllLines(@inputFile);
                Console.WriteLine("Reading input file.");
                await TranslateText(route, inputs);
            }
            catch (Exception objEx)
            {
                Console.WriteLine("Error: " + objEx.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
