using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlovakVirtualKeyboard
{
    class ParseTxt
    {
        public const string FilePath = "sk.txt";
        public static string[] SlovakWords;
        public static bool exitFile = false;

        // Read the file "sk.txt" and put the words into a static array "SlovakWords".
        public static void ReadSlovakWords()
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                var linesCount = lines.Length;
                SlovakWords = new string[linesCount];

                for (int i = 0; i < linesCount; i++)
                {
                    var word = lines[i].Split(' ')[0];
                    SlovakWords[i] = word;
                }
                exitFile = true;
            }           
        }

        public static string[] SearchStartWith(string beginning)
        {
            return SlovakWords.Where(r => r.StartsWith(beginning)).Take(4).ToArray();
        }
    }
}
