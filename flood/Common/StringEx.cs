using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public static class StringEx
    {
        /// <summary>
        /// Adds a new line after maxperLine has been reached. Will only add new line if end of word is reached
        /// </summary>
        /// <param name="s">String to mutate</param>
        /// <param name="maxPerLine">The chracter target before adding a new line</param>
        /// <returns>a string with line breaks</returns>
        public static string SplitStringLine(this string s, int maxPerLine)
        {
            var resultString = string.Empty;
            int count = 0;

            foreach (char c in s)
            {
                if (count >= maxPerLine && c == ' ')
                {
                    resultString += Environment.NewLine;
                    count = 0;
                    continue;
                }

                resultString += c;
                count++;
            }

            return resultString;
        }

        public static string[] ToWordArray(this string s)
        {
            return s.Split(new [] {' '});
        }

        public static string FirstCharToCaps(this string s)
        {
            return string.Join(" ", s.Split(' ')
                .Select(x => x[0].ToString().ToUpper() + x.Substring(1, x.Length -1)));
        }

        public static string EveryOtherCharacterToCaps(this string s)
        {
            var result = string.Empty;
            s = s.ToLower();

            for(int i =0; i < s.Length; i++)
            {
                if(i % 2 == 0)
                {
                    result += s[i].ToString().ToUpper();
                    continue;
                }

                result += s[i];

            }

            return result;
        }

        public static IDictionary<char, int> LetterCountsOfString(this string s)
        {
            Dictionary<char, int> results = new Dictionary<char, int>();

            foreach(var letter in s)
            {
                if(!results.ContainsKey(letter))
                {
                    results.Add(letter, 0);
                }

                results[letter]++;
            }

            return results;
        }

        public static IEnumerable<IGrouping<bool, char>> Messing(this string s)
        {
            var bbbb = s.GroupBy(x => (byte)x % 2 == 0);
            return bbbb;
            
        }

    }
}
