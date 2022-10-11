using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var finalSentense = new StringBuilder();
            finalSentense.Append(phraseBeginning);
            int counter = 0;
            string phrase = phraseBeginning;
            string[] keysArr = new string[2];
            
            while (counter != wordsCount)
            {
                if(phrase == "breakMF") break;
                keysArr = GetOneOrTwoWords(phrase, keysArr);
                phrase = CheckKey(keysArr, nextWords, ref finalSentense);
                counter++;
            }

            return finalSentense.ToString();
        }

        static string[] GetOneOrTwoWords(string phrase, string[] keysArr)
        {
            if (phrase.Contains(" "))
            {
                int indexLength = phrase.Split(' ').Length;
                keysArr[0] = phrase.Split(' ')[indexLength - 2];
                keysArr[1] = phrase.Split(' ')[indexLength - 1];
            }
            else
            {
                keysArr[0] = keysArr[1];
                keysArr[1] = phrase;
            }
            return keysArr;
        }

        static string CheckKey(string[] key, Dictionary<string, string> dict, ref StringBuilder finalSentence)
        {
            if (dict.ContainsKey($"{key[0]} {key[1]}"))
            {
                finalSentence.Append(" " + dict[$"{key[0]} {key[1]}"]);
                return dict[$"{key[0]} {key[1]}"];
            }
            else if (dict.ContainsKey($"{key[1]}"))
            {
                finalSentence.Append(" " + dict[$"{key[1]}"]);
                return dict[$"{key[1]}"];
            }
            else return "breakMF";
        }
    }
}