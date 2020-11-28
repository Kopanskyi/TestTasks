using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rail_Fence_Cipher
{
    static class RailFenceCipher
    {
        public static string Encode(string stringToEncode, int railsNumber)
        {
            if (string.IsNullOrEmpty(stringToEncode))
            {
                return string.Empty;
            }

            if (railsNumber < 2)
            {
                return stringToEncode;
            }

            var symbolsCollection = new List<char>[railsNumber];

            for (int i = 0; i < symbolsCollection.Length; i++)
            {
                symbolsCollection[i] = new List<char>();
            }

            var rail = 0;
            var increaseRailNumber = true;

            foreach (var symbol in stringToEncode)
            {
                symbolsCollection[rail].Add(symbol);

                if (rail == railsNumber - 1)
                {
                    increaseRailNumber = false;
                }

                if (rail == 0 && !increaseRailNumber)
                {
                    increaseRailNumber = true;
                }

                if (increaseRailNumber && rail < railsNumber - 1)
                {
                    rail++;
                }

                if (!increaseRailNumber && rail > 0)
                {
                    rail--;
                }
            }

            return EncodedToString(symbolsCollection);
        }

        public static string Decode(string stringToDecode, int railsNumber)
        {
            if (string.IsNullOrEmpty(stringToDecode))
            {
                return string.Empty;
            }

            if (railsNumber < 2)
            {
                return stringToDecode;
            }

            var symbolsPerLineCount = new int[railsNumber];

            for (int i = 0; i < symbolsPerLineCount.Length; i++)
            {
                symbolsPerLineCount[i] = 0;
            }

            var rail = 0;
            var increaseRailNumber = true;

            foreach (var symbol in stringToDecode)
            {
                symbolsPerLineCount[rail]++;

                if (rail == railsNumber - 1)
                {
                    increaseRailNumber = false;
                }

                if (rail == 0 && !increaseRailNumber)
                {
                    increaseRailNumber = true;
                }

                if (increaseRailNumber && rail < railsNumber - 1)
                {
                    rail++;
                }

                if (!increaseRailNumber && rail > 0)
                {
                    rail--;
                }
            }

            var symbolsCollection = new List<char>[railsNumber];

            for (int startIndex = 0, i = 0; i < symbolsCollection.Length; i++)
            {
                symbolsCollection[i] = new List<char>();
                symbolsCollection[i].AddRange(stringToDecode.Substring(startIndex, symbolsPerLineCount[i]));
                startIndex += symbolsPerLineCount[i];
            }

            return DecodedToString(symbolsCollection, symbolsPerLineCount);
        }

        private static string EncodedToString(IEnumerable<char>[] symbolsCollection)
        {
            var text = new StringBuilder();

            foreach (var symbolList in symbolsCollection)
            {
                foreach (var symbol in symbolList)
                {
                    text.Append(symbol);
                }
            }

            return text.ToString();
        }

        private static string DecodedToString(IList<char>[] symbolsCollection, IList<int> symbolsPerLineCount)
        {
            var totalSymbolsCount = 0;

            for (var i = 0; i < symbolsPerLineCount.Count(); i++)
            {
                totalSymbolsCount += symbolsPerLineCount[i];
                symbolsPerLineCount[i] = 0;
            }

            var text = new StringBuilder();
            var railsNumber = symbolsCollection.Length;
            var increaseRailNumber = true;

            for (int rail = 0; text.Length < totalSymbolsCount; )
            {
                text.Append(symbolsCollection[rail][symbolsPerLineCount[rail]]);
                symbolsPerLineCount[rail]++;

                if (rail == railsNumber - 1)
                {
                    increaseRailNumber = false;
                }

                if (rail == 0 && !increaseRailNumber)
                {
                    increaseRailNumber = true;
                }

                if (increaseRailNumber && rail < railsNumber - 1)
                {
                    rail++;
                }

                if (!increaseRailNumber && rail > 0)
                {
                    rail--;
                }
            }

            return text.ToString();
        }
    }
}
