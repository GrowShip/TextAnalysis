using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var arrOfSentences = DecodeIntoArrOfSentences(text);
            foreach (var sentence in arrOfSentences)
            {
                if(!string.IsNullOrEmpty(sentence)) sentencesList.Add(ConverteIntoListOfWords(sentence));
            }
            sentencesList.RemoveAll(x => x == null || x.Count == 0);
            return sentencesList;
        }

        static string[] DecodeIntoArrOfSentences(string text)
        {
            var arrOfSentences = text.Split(new char[] {'.',';', '!', '?', ':', '(', ')'});
            return arrOfSentences;
        }
        
        static List<string> ConverteIntoListOfWords(string sentence)
        {
            var listOfWords = new List<string>();
            var words = new StringBuilder();
            int i = 0;
            foreach (var letter in sentence)
            {
                if (i == 0 && letter == ' ')
                    continue;
                else if (char.IsLetter(letter) || letter == '\'')
                    words.Append(letter);
                else
                    words.Append(' ');
                i++;
            }

            foreach (var word in words.ToString().Split(' '))
            {   
                if(string.IsNullOrEmpty(word.ToLower())) continue;
                listOfWords.Add(word.ToLower());
            }
            return listOfWords;
        }
    }
}