using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = GetFinalDict(text);
            return result;;
        }
        
        static Dictionary<string, string> GetFinalDict(List<List<string>> initialList)
        {
            var dictWithCountOfBigramsAndThreegrams = GetThreegramsWithCount(initialList);
            return GetLastDict(dictWithCountOfBigramsAndThreegrams);
        }

        private static Dictionary<string,Dictionary<string,int>> GetThreegramsWithCount(List<List<string>> initialList)
        {
            var dictWithCountOfBigrams = new Dictionary<string, Dictionary<string, int>>();
            for (int amountOfNgrams = 1; amountOfNgrams < 3; amountOfNgrams++)
            {
                for (int i = 0; i < initialList.Count; i++)
                {
                    for (int j = 0; j < initialList[i].Count - amountOfNgrams; j++)
                    {
                        string nGramKey = "";
                        if (amountOfNgrams == 1) nGramKey = initialList[i][j];
                        else if(amountOfNgrams == 2) nGramKey = $"{initialList[i][j]} {initialList[i][j + 1]}";
                        int howManyGramsInSentense = 1;
                        if (dictWithCountOfBigrams.ContainsKey(nGramKey))
                        {
                            if (dictWithCountOfBigrams[nGramKey].ContainsKey(initialList[i][j + amountOfNgrams]))
                            {
                                dictWithCountOfBigrams[nGramKey].TryGetValue(initialList[i][j + amountOfNgrams], out howManyGramsInSentense);
                                howManyGramsInSentense++;
                                dictWithCountOfBigrams[nGramKey][initialList[i][j + amountOfNgrams]] = howManyGramsInSentense;
                            }
                            else
                            {
                                dictWithCountOfBigrams[nGramKey].Add(initialList[i][j + amountOfNgrams], howManyGramsInSentense);
                            }
                        }
                        else
                        {
                            var result2 = new Dictionary<string, int>();
                            result2.Add(initialList[i][j + amountOfNgrams], howManyGramsInSentense);
                            dictWithCountOfBigrams.Add(nGramKey, result2);
                        }
                    }
                }
            }
            return dictWithCountOfBigrams;
        }
        
        static Dictionary<string,string> GetLastDict(Dictionary<string,Dictionary<string,int>> dictWithThreeAndDigrams)
        {
            var finalDict = new Dictionary<string, string>();
            var arrOfKeys = new string [dictWithThreeAndDigrams.Count];
            dictWithThreeAndDigrams.Keys.CopyTo(arrOfKeys,0);
            for (int i = 0; i < arrOfKeys.Length; i++)
            {
                var arrOfKeys2 = new string [dictWithThreeAndDigrams[arrOfKeys[i]].Count];
                dictWithThreeAndDigrams[arrOfKeys[i]].Keys.CopyTo(arrOfKeys2,0);
                if (arrOfKeys2.Length == 1)
                {
                    finalDict.Add(arrOfKeys[i],arrOfKeys2[0]);
                }
                else if (CheckAll(dictWithThreeAndDigrams, arrOfKeys[i],arrOfKeys2))
                {
                    string value = arrOfKeys2[0];
                    for (int j = 0; j < arrOfKeys2.Length - 1; j++)
                    {
                        if (string.CompareOrdinal(value, arrOfKeys2[j + 1]) > 0)
                            value = arrOfKeys2[j + 1];
                    }
                    finalDict.Add(arrOfKeys[i],value);
                }
                else if(!CheckAll(dictWithThreeAndDigrams, arrOfKeys[i],arrOfKeys2))
                {
                    int numberOfKey = 0;
                    for (int j = 0; j < arrOfKeys2.Length-1; j++)
                    {
                        if (dictWithThreeAndDigrams[arrOfKeys[i]][arrOfKeys2[numberOfKey]] < dictWithThreeAndDigrams[arrOfKeys[i]][arrOfKeys2[j+1]])
                        {
                            numberOfKey = j+1;
                        }
                        else if (dictWithThreeAndDigrams[arrOfKeys[i]][arrOfKeys2[numberOfKey]] ==
                                 dictWithThreeAndDigrams[arrOfKeys[i]][arrOfKeys2[j+1]])
                        {
                            if (string.CompareOrdinal(arrOfKeys2[numberOfKey], arrOfKeys2[j + 1]) > 0)
                                numberOfKey = j + 1;
                        }
                    }
                    finalDict.Add(arrOfKeys[i],arrOfKeys2[numberOfKey]);
                }
            }
            return finalDict;
        }

        private static bool CheckAll(Dictionary<string, Dictionary<string, int>> dictWithThreeAndDigrams, string firstKeyDict, string[] arrOfKeys2)
        {
            bool checkout = true;
            for (int i = 0; i < arrOfKeys2.Length; i++)
            {
                if (dictWithThreeAndDigrams[firstKeyDict][arrOfKeys2[i]] != 1) checkout = false;
            }
            return checkout;
        }
    }
}