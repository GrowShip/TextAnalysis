using System;
using System.Collections.Generic;
using System.IO;
using NUnitLite;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Запуск автоматических тестов. Ниже список тестовых наборов, который нужно запустить.
            // Закомментируйте тесты на те задачи, к которым ещё не приступали, чтобы они не мешались в консоли.
            // Все непрошедшие тесты 
            var testsToRun = new string[]
            {
                // "TextAnalysis.SentencesParser_Tests",
                // "TextAnalysis.FrequencyAnalysis_Tests",
                // "TextAnalysis.TextGenerator_Tests",
            };
            new AutoRun().Execute(new[]
            {
                // "--stoponerror", // Останавливать после первого же непрошедшего теста. Закомментируйте, чтобы увидеть все падающие тесты
                "--noresult",
                "--test=" + string.Join(",", testsToRun)
            });

            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParserTask.ParseSentences(text);
            var frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);
            //Расскомментируйте этот блок, если хотите выполнить последнюю задачу до первых двух.
            
            frequency = new Dictionary<string, string>
            {
                {"harry", "potter"},
                {"potter", "boy" },
                {"boy", "who" },
                {"who", "likes" },
                {"boy who", "survived" },
                {"survived", "attack" },
                {"he", "likes" },
                {"likes", "harry" },
                {"ron", "likes" },
                {"wizard", "harry" },
            };

            frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(
                SentencesParserTask.ParseSentences(
                    File.ReadAllText(@"/Users/ilya/Downloads/TextAnalysis.csproj/HarryPotterText.txt")));
            
            while (true)
            {
                Console.WriteLine("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                Console.WriteLine("Введите кол-во слов после (например, 6): ");
                int wordsCount = 0;
                int.TryParse(Console.ReadLine(),out wordsCount);
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGeneratorTask.ContinuePhrase(frequency, beginning.ToLower(), wordsCount);
                Console.WriteLine(phrase);
            }
        }
    }
}